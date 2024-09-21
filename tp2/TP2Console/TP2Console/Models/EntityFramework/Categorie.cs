using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TP2Console.Models.EntityFramework;

[Table("categorie")]
public partial class Categorie
{
    // private ILazyLoader _lazyLoader;
    // public Categorie(ILazyLoader lazyLoader)
    // {
    //     _lazyLoader = lazyLoader;
    // }
    
    [Key]
    [Column("idcategorie")]
    public int Idcategorie { get; set; }

    [Column("nom")]
    [StringLength(50)]
    public string Nom { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [InverseProperty(nameof(Film.IdcategorieNavigation))]
    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
    
    // // Lazy Loading
    // private ICollection<Film> films;
    // public virtual ICollection<Film> Films
    // {
    //     get => _lazyLoader.Load(this, ref films);
    //     set => films = value;
    // }
}
