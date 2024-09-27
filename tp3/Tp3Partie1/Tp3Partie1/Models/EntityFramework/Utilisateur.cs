using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tp3Partie1.Models.EntityFramework;

[Table("t_e_utilisateur_utl")]
public partial class Utilisateur
{
    [Key] [Column("utl_id")] 
    public int UtilisateurId { get; set; }

    [Column("utl_nom")] [StringLength(50)] 
    public string? Nom { get; set; }

    [Column("utl_prenom")]
    [StringLength(50)]
    public string? Prenom { get; set; }

    [Column("utl_mobile")] 
    [RegularExpression(@"^0[0-9]{9}$", ErrorMessage="Le numéro de téléphone doit être composé de 10 chiffres et commencer par 0")]
    public string? Mobile { get; set; }

    [Column("utl_mail")]
    [StringLength(100, MinimumLength = 6)]
    [EmailAddress(ErrorMessage = "La longueur de l'adresse mail doit être comprise entre 6 et 100 caractères")]
    [Required]
    public string Mail { get; set; } = null!;

    [Column("utl_pwd")]
    [StringLength(64)]
    // 6 character password with at least one uppercase letter, one lowercase letter, one number 
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$", ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères, une lettre majuscule, une lettre minuscule et un chiffre")]
    [Required]
    public string? Pwd { get; set; }

    [Column("utl_rue")]
    [StringLength(200)]
    public string? Rue { get; set; }

    [Column("utl_cp")] [StringLength(5, ErrorMessage = "Le code postal doit être composé de 5 chiffres")] 
    public string? Cp { get; set; }

    [Column("utl_ville")]
    [StringLength(50)]
    public string? Ville { get; set; }

    [Column("utl_pays")]
    [StringLength(50)]
    public string? Pays { get; set; }

    [Column("utl_latitude")] 
    public float? Lat { get; set; }

    [Column("utl_longitude")] 
    public float? Long { get; set; }

    [Column("utl_datecreation")]
    [Required]
    public DateTime DateCreation { get; set; }
    
    [InverseProperty(nameof(Notation.UtilisateurNotant))]
    public virtual ICollection<Notation> NotesUtilisateur { get; set; } = new List<Notation>();

}