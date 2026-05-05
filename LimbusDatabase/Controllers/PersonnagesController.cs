using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LimbusDatabase.Data;
using LimbusDatabase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using LimbusDatabase.ViewModels;

namespace LimbusDatabase.Controllers
{
    public class PersonnagesController : Controller
    {
        private readonly LimbusDatabaseContext _context;

        public PersonnagesController(LimbusDatabaseContext context)
        {
            _context = context;
        }

        // GET: Personnages
        public async Task<IActionResult> Index()
        {
            var limbusDatabaseContext = _context.Personnages.Include(x => x.Faction);
            return View(await limbusDatabaseContext.ToListAsync());
        }

        // GET: Personnages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personnage = await _context.Personnages
                .Include(p => p.Faction)
                .FirstOrDefaultAsync(m => m.PersonnageId == id);
            if (personnage == null)
            {
                return NotFound();
            }
            string Image = null;
            if (personnage.Photo != null)
            {
                Image = $"data:image/png;base64, {Convert.ToBase64String (personnage.Photo)}";
            }
            DetailViewModel dvm= new DetailViewModel { ImageUrl = Image ,Personnage=personnage};
            return View(dvm);
        }

        // GET: Personnages/Create
        public IActionResult Create()
        {
            ViewData["FactionId"] = new SelectList(_context.Factions, "FactionId", "FactionId");
            return View();
        }

        // POST: Personnages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonnageId,Nom,Prenom,FactionId,District,EnVie")] Personnage personnage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personnage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FactionId"] = new SelectList(_context.Factions, "FactionId", "FactionId", personnage.FactionId);
            return View(personnage);
        }

        // GET: Personnages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personnage = await _context.Personnages.FindAsync(id);
            if (personnage == null)
            {
                return NotFound();
            }
            ViewData["FactionId"] = new SelectList(_context.Factions, "FactionId", "FactionId", personnage.FactionId);
            return View(personnage);
        }

        // POST: Personnages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonnageId,Nom,Prenom,FactionId,District,EnVie")] Personnage personnage)
        {
            if (id != personnage.PersonnageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personnage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonnageExists(personnage.PersonnageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FactionId"] = new SelectList(_context.Factions, "FactionId", "FactionId", personnage.FactionId);
            return View(personnage);
        }

        // GET: Personnages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personnage = await _context.Personnages
                .Include(p => p.Faction)
                .FirstOrDefaultAsync(m => m.PersonnageId == id);
            if (personnage == null)
            {
                return NotFound();
            }

            return View(personnage);
        }

        // POST: Personnages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personnage = await _context.Personnages.FindAsync(id);
            if (personnage != null)
            {
                _context.Personnages.Remove(personnage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonnageExists(int id)
        {
            return _context.Personnages.Any(e => e.PersonnageId == id);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> MortDeFactionDistrict(MortDeFactionDistrictViewModel vm)
        {
            string query = "EXEC Personnages.usp_MortDeFactionDistrict @FactionId, @District";
            List<SqlParameter> parameters = new List<SqlParameter> {
            new SqlParameter{ParameterName="@FactionId",Value=vm.FactionId},
            new SqlParameter{ParameterName="@District",Value=vm.District}
            };
            await _context.Database.ExecuteSqlRawAsync(query, parameters.ToArray());
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]

        public async Task<IActionResult> MortDeFactionDistrict()
        {
            return View();
        }
        [Route("Personnages/AjouterImage/{PersonnageId}")]
        [HttpPost]
        public async Task<IActionResult> AjouterImage(ImageUploadViewModel iuvm,int PersonnageId)
        {
            Personnage? personnage = await _context.Personnages.FirstOrDefaultAsync(x => x.PersonnageId == PersonnageId);
            if (ModelState.IsValid&& personnage!=null)
            {
                
                if (iuvm.FormFile!=null&&iuvm.FormFile.Length>=0)
                {
                    MemoryStream stream = new MemoryStream();
                    await iuvm.FormFile.CopyToAsync(stream);
                    byte[] fichierImage= stream.ToArray();
                    personnage.Photo = fichierImage;
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        
        public async Task<IActionResult> AjouterImage(int PersonnageId)
        {
           
            return View(new ImageUploadViewModel() { PersonnageId=PersonnageId});
        }

        public async Task<IActionResult> PersonnagesNombreIdentite()
        {
            return View (await _context.VwIdentitesPersonnages.ToListAsync());
        }

        public async Task<IActionResult> PersonnagesAnormalitesBatailles()
        {
            List<VwPersonnagesBataille> personnages= await _context.VwPersonnagesBatailles.AsQueryable().ToListAsync();
            List<VwAnormaliteBataille> anormalites = await _context.VwAnormaliteBatailles.AsQueryable().ToListAsync();
            PersonnagesAnormalitesBataillesViewModel  pabvm = new PersonnagesAnormalitesBataillesViewModel() { PersonnagesBatailles=personnages,AnormaliteBatailles=anormalites};
            return View(pabvm);
        }
    }
}
