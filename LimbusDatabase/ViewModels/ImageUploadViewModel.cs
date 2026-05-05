using System.ComponentModel.DataAnnotations;
using LimbusDatabase.Models;

namespace LimbusDatabase.ViewModels
{
    public class ImageUploadViewModel
    {
        [Required(ErrorMessage ="Un fichier est requis")]
        public IFormFile? FormFile { get; set; } = null!;

        [Required(ErrorMessage = "Il faut spécifier un nom à l'image")]
        public string NomImage { get; set; } = null!;

        public int PersonnageId { get; set; }
    }
}
