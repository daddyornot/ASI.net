using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tp3Partie1.Models.EntityFramework;

[Table("t_e_utilisateur_utl")]
public partial class Utilisateur
{
    [Key] [Column("utl_id")] public int UtilisateurId { get; set; }

    [Column("utl_nom")] [StringLength(50)] public string? Nom { get; set; }

    [Column("utl_prenom")]
    [StringLength(50)]
    public string? Prenom { get; set; }

    [Column("utl_mobile")] public string? Mobile { get; set; }

    [Column("utl_mail")]
    [StringLength(100)]
    [Required]
    public string Mail { get; set; } = null!;

    [Column("utl_pwd")]
    [StringLength(64)]
    [Required]
    public string? Pwd { get; set; }

    [Column("utl_rue")]
    [StringLength(200)]
    public string? Rue { get; set; }

    [Column("utl_cp")] [StringLength(5)] public string? Cp { get; set; }

    [Column("utl_ville")]
    [StringLength(50)]
    public string? Ville { get; set; }

    [Column("utl_pays")]
    [StringLength(50)]
    public string? Pays { get; set; }

    [Column("utl_latitude")] public float? Lat { get; set; }

    [Column("utl_longitude")] public float? Long { get; set; }

    [Column("utl_datecreation")]
    [Required]
    public DateTime DateCreation { get; set; }
    
    [InverseProperty(nameof(Notation.UtilisateurNotant))]
    public virtual ICollection<Notation> NotesUtilisateur { get; set; } = new List<Notation>();

}