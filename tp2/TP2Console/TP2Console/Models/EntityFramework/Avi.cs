using System;
using System.Collections.Generic;

namespace TP2Console.Models.EntityFramework;

public partial class Avi
{
    public int Idfilm { get; set; }

    public int Idutilisateur { get; set; }

    public string? Commentaire { get; set; }

    public decimal Note { get; set; }

    public virtual Film IdfilmNavigation { get; set; } = null!;

    public virtual Utilisateur IdutilisateurNavigation { get; set; } = null!;
}
