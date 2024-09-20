using System;
using System.Collections.Generic;

namespace TP2Console.Models.EntityFramework;

public partial class Utilisateur
{
    public int Idutilisateur { get; set; }

    public string Login { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Pwd { get; set; } = null!;

    public virtual ICollection<Avi> Avis { get; set; } = new List<Avi>();
}
