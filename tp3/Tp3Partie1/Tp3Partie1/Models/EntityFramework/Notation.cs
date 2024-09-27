using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tp3Partie1.Models.EntityFramework;

[Table("t_j_notation_not")]
public partial class Notation
{
    [Column("utl_id")]
    public int UtilisateurId { get; set; }
    
    [Column("ser_id")]
    public int SerieId { get; set; }
    
    [Column("not_note")]
    [Range(0, 5)]
    public int Note { get; set; }
    
    [ForeignKey(nameof(UtilisateurId))]
    [InverseProperty(nameof(UtilisateurNotant.NotesUtilisateur))]
    public virtual Utilisateur UtilisateurNotant { get; set; } = null!;
    
    [ForeignKey(nameof(SerieId))]
    [InverseProperty(nameof(Serie.NotesSerie))]
    public virtual Serie SerieNotee { get; set; } = null!;
    
}