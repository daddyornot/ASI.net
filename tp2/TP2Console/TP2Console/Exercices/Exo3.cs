using Microsoft.EntityFrameworkCore;
using TP2Console.Models.EntityFramework;

namespace TP2Console.Exercices;

public static class Exo3
{
    /// <summary>
    ///  Ajouter un utilisateur
    /// </summary>
    public static void Q1()
    {
        var ctx = new PostgresContext();
        var newUser = new Utilisateur()
        {
            Email = "damien@mail.com",
            Idutilisateur = 9,
            Login = "damien",
            Pwd = "damien"
        };

        ctx.Utilisateurs.Add(newUser);
        ctx.SaveChanges();
    }

    /// <summary>
    ///  Modifier la description du film "L'armée des douze singes" et changer sa catégorie pour "Drame"
    /// </summary>
    public static void Q2()
    {
        var ctx = new PostgresContext();
        var film = ctx.Films.First(f => f.Nom.ToLower().Contains("armee des douze singes"));
        film.Description = "Documentaire sur les singes";
        film.Idcategorie = ctx.Categories.First(c => c.Nom == "Drame").Idcategorie;
        Console.WriteLine(film);
        ctx.SaveChanges();
    }

    /// <summary>
    ///  Supprimer le film "L'armée des douze singes" et tous les avis associés
    /// </summary>
    public static void Q3()
    {
        var ctx = new PostgresContext();
        var film = ctx.Films.First(f => f.Nom.ToLower().Contains("armee des douze singes"));
        ctx.Avis.Where(a => a.Idfilm == film.Idfilm).ToList().ForEach(a => ctx.Avis.Remove(a));
        ctx.Films.Remove(film);
        ctx.SaveChanges();
    }

    /// <summary>
    /// Ajouter un avis
    /// </summary>
    public static void Q4()
    {
        var ctx = new PostgresContext();
        var avis = new Avi()
        {
            Idfilm = ctx.Films.First(f => f.Nom.ToLower().Contains("titanic")).Idfilm,
            Idutilisateur = ctx.Utilisateurs.First(u => u.Login == "damien").Idutilisateur,
            Note = new decimal(0.01),
            Commentaire = "Nul, null et re-nul"
        };
        ctx.Avis.Add(avis);
        ctx.SaveChanges();
    }

    /// <summary>
    /// Ajouter deux films dans la catégorie "Drame" avec AddRange
    /// </summary>
    public static void Q5()
    {
        var ctx = new PostgresContext();
        var drameCategory = ctx.Categories.First(c => c.Nom == "Drame").Idcategorie;
        var films = new List<Film>
        {
            new Film
            {
                Nom = "Film1DeDrame",
                Description = "Description1 dramatique",
                Idcategorie = drameCategory
            },
            new Film
            {
                Nom = "Film2DeEncorePlusDeDrame",
                Description = "Description2 encore plus dramatique",
                Idcategorie = drameCategory
            }
        };
        ctx.Films.AddRange(films);
        ctx.SaveChanges();
    }
}