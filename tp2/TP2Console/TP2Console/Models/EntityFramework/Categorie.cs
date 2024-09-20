using System;
using System.Collections.Generic;

namespace TP2Console.Models.EntityFramework;

public partial class Categorie
{
    public int Idcategorie { get; set; }

    public string Nom { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
