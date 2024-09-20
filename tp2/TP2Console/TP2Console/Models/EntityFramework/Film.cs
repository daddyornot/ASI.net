using System;
using System.Collections.Generic;

namespace TP2Console.Models.EntityFramework;

public partial class Film
{
    public int Idfilm { get; set; }

    public string Nom { get; set; } = null!;

    public string? Description { get; set; }

    public int Idcategorie { get; set; }

    public virtual ICollection<Avi> Avis { get; set; } = new List<Avi>();

    public virtual Categorie IdcategorieNavigation { get; set; } = null!;
}
