using System.Security.Claims;
using System.Security.Principal;
using LimbusDatabase.Data;
using LimbusDatabase.Models;
using LimbusDatabase.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace LimbusDatabase.Controllers
{
    public class UtilisateursController : Controller
    {
        readonly LimbusDatabaseContext _context;

        public UtilisateursController(LimbusDatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Inscription()
        {
            return View();
        }

        public IActionResult Connexion()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Inscription(InscriptionViewModel ivm)
        {
            bool already = await _context.Utilisateurs.AnyAsync(x => x.Pseudonyme == ivm.Pseudonyme);
            if (already)
            {
                ModelState.AddModelError("Pseudonyme", "Ce pseudonyme est déjà pris");
                return View(ivm);
            }
            string query = "EXEC Utilisateurs.USP_CreerUtilisateur @Pseudonyme, @MotDePasse, @Email";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter{ParameterName="@Pseudonyme",Value=ivm.Pseudonyme},
                new SqlParameter {ParameterName="@MotDePasse",Value=ivm.Password},
                new SqlParameter{ParameterName="@Email",Value=ivm.Email}

            };
            try
            {
                await _context.Database.ExecuteSqlRawAsync(query, parameters.ToArray());
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Une erreur est survenue");
                return View(ivm);
            }
            return RedirectToAction("Connexion", "Utilisateurs");
        }

        [HttpPost]

        public async Task<IActionResult> Connexion(ConnexionViewModel cvm)
        {
            string query = "EXEC Utilisateurs.AuthUtilisateur @Pseudo, @MotDePasse";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter{ParameterName="@Pseudo",Value=cvm.Pseudonyme},
                new SqlParameter{ParameterName="@MotDePasse",Value=cvm.Password}

            };

            Utilisateur? utilisateur=(await _context.Utilisateurs.FromSqlRaw(query,parameters.ToArray()).ToListAsync()).FirstOrDefault();
            if (utilisateur == null)
            {
                ModelState.AddModelError("", "Nom d'utilisateur ou mot de passe invalide");
                return View(cvm);
            }

            List<Claim> claims= new List<Claim> { 
                
                new Claim(ClaimTypes.NameIdentifier,utilisateur.UtilisateurId.ToString()),
                new Claim(ClaimTypes.Name,utilisateur.Pseudonyme)
            
            };

            ClaimsIdentity identite = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identite);
            await HttpContext.SignInAsync(principal);

            return RedirectToAction("Index", "Utilisateurs");
        }

        [HttpGet]
        public async Task<IActionResult> Deconnexion()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Utilisateurs");
        }

        public async Task<IActionResult> Index()
        {
            ViewData["utilisateur"] = "Visiteur";
            IIdentity? identite = HttpContext.User.Identity;
            if (identite != null && identite.IsAuthenticated)
            {
                string pseudo = HttpContext.User.FindFirstValue(ClaimTypes.Name);
                Utilisateur? user = await _context.Utilisateurs.FirstOrDefaultAsync(x => x.Pseudonyme == pseudo);
                if (user != null)
                {
                    ViewData["utilisateur"] = user.Pseudonyme;
                }
            }
            return View();
        }
    }
}
