using System.ComponentModel.DataAnnotations;

namespace LimbusDatabase.ViewModels
{
    public class ConnexionViewModel
    {
        [Required(ErrorMessage = "Veuillez préciser un nom d'utilisateur")]
        public string Pseudonyme { get; set; } = null!;

        [Required(ErrorMessage = "Veuillez entrer un mot de passe.")]
        public string Password { get; set; } = null!;
    }
}
