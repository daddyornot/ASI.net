using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Models;

public partial class Utilisateur
{
    public int UtilisateurId { get; set; }

    public string? Nom { get; set; }

    public string? Prenom { get; set; }

    public string? Mobile { get; set; }

    [StringLength(100, MinimumLength = 6,
        ErrorMessage = "La longueur de l'adresse mail doit être comprise entre 6 et 100 caractères")]
    [EmailAddress(ErrorMessage = "L'adresse mail n'est pas valide")]
    [Required]
    public string Mail { get; set; } = null!;

    [StringLength(64, ErrorMessage = "Le mot de passe doit être inférieur à 64 caractères")]
    // 6 character password with at least one uppercase letter, one lowercase letter, one number 
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d\S]{6,}$",
        ErrorMessage =
            "Le mot de passe doit contenir au moins 6 caractères, une lettre majuscule, une lettre minuscule, un chiffre et peut inclure des caractères spéciaux")]
    [Required]

    public string? Pwd { get; set; }

    [StringLength(200, ErrorMessage = "Le champ doit contenir moins de 200 caractères")]
    public string? Rue { get; set; }

    public string? Cp { get; set; }

    [StringLength(50, ErrorMessage = "Le champ doit contenir moins de 50 caractères")] 
    public string? Ville { get; set; }

    [StringLength(50, ErrorMessage = "Le champ doit contenir moins de 50 caractères")] 
    public string? Pays { get; set; }

    public float? Lat { get; set; }

    public float? Long { get; set; }

    [Required] public DateTime DateCreation { get; set; }

    // public virtual ICollection<Notation> NotesUtilisateur { get; set; } = new List<Notation>();
}