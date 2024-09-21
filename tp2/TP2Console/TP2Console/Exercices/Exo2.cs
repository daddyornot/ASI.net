using Microsoft.EntityFrameworkCore;
using TP2Console.Models.EntityFramework;

namespace TP2Console.Exercices;

public static class Exo2
{
    /// <summary>
    /// Afficher le nombre de films
    /// </summary>
    public static void Q1()
    {
        var ctx = new PostgresContext();
        foreach (var film in ctx.Films)
        {
            Console.WriteLine("Nombre de films : " + ctx.Films.Count());
            // Console.WriteLine(film.ToString());
        }
    }

    /// <summary>
    /// Afficher les emails des utilisateurs
    /// </summary>
    public static void Q2()
    {
        var ctx = new PostgresContext();
        foreach (var utilisateur in ctx.Utilisateurs)
        {
            Console.WriteLine(utilisateur.Email);
        }
    }

    /// <summary>
    /// Afficher les utilisateurs triés par login croissant
    /// </summary>
    public static void Q3()
    {
        var ctx = new PostgresContext();
        foreach (var utilisateur in ctx.Utilisateurs.OrderBy(u => u.Login))
        {
            Console.WriteLine(utilisateur.ToString());
        }
    }

    /// <summary>
    /// Afficher les noms et id des films de la catégorie Action
    /// </summary>
    public static void Q4()
    {
        var ctx = new PostgresContext();
        // Categorie categorieAction = ctx.Categories
        // .Include(c => c.Films)
        // .ThenInclude(f => f.Avis)
        // .First(c => c.Nom == "Action");

        // sans include
        Categorie categorieAction = ctx.Categories.First(c => c.Nom == "Action");
        ctx.Entry(categorieAction).Collection(c => c.Films).Load();


        Console.WriteLine("Dans la catégorie " + categorieAction.Nom + " il y a les films suivants : ");
        foreach (var film in categorieAction.Films)
        {
            Console.WriteLine("Nom du film : " + film.Nom + " - Id du film : " + film.Idfilm);
        }
    }

    /// <summary>
    /// Afficher le nombre de catégories
    /// </summary>
    public static void Q5()
    {
        var ctx = new PostgresContext();
        Console.WriteLine("Nombre de catégories : " + ctx.Categories.Count());
    }

    /// <summary>
    /// Afficher la note la plus basse
    /// </summary>
    public static void Q6()
    {
        var ctx = new PostgresContext();
        Console.WriteLine("Note la plus basse : " + ctx.Avis.Min(a => a.Note));
    }

    /// <summary>
    /// Afficher les films commençant par "le" non sensibles à la casse
    /// </summary>
    public static void Q7()
    {
        var ctx = new PostgresContext();
        // List<Film> films = ctx.Films
        //     .Where(f => f.Nom.ToLower().StartsWith("le".ToLower()))
        //     .ToList();

        List<Film> films = ctx.Films
            .Where(f => EF.Functions.Like(f.Nom.ToLower(), "le%".ToLower()))
            .ToList();

        Console.WriteLine($"Films commençant par 'le' ({films.Count} résultats) : ");
        foreach (var film in films)
        {
            Console.WriteLine(film.Nom);
        }
    }

    /// <summary>
    /// Afficher la note moyenne de Pulp Fiction insensible à la casse
    /// </summary>
    public static void Q8()
    {
        var ctx = new PostgresContext();

        // decimal noteMoyenne = ctx.Films
        //     .Include(f => f.Avis)
        //     .Where(f => f.Nom.ToLower() == "pulp fiction")
        //     .SelectMany(f => f.Avis)
        //     .Average(a => a.Note);

        // sans include
        Film pulpFiction = ctx.Films.First(f => f.Nom.ToLower() == "pulp fiction".ToLower());
        ctx.Entry(pulpFiction).Collection(f => f.Avis).Load();
        decimal noteMoyenne = pulpFiction.Avis.Average(a => a.Note);

        Console.WriteLine("Note moyenne de Pulp Fiction : " + noteMoyenne);
    }

    /// <summary>
    /// Afficher l'utiliseateur ayant mis la note la plus haute
    /// </summary>
    public static void Q9()
    {
        var ctx = new PostgresContext();

        Utilisateur utilisateur = ctx.Utilisateurs
            .OrderByDescending(u => u.Avis.Max(a => a.Note))
            .First();

        Console.WriteLine("Utilisateur ayant mis la note la plus haute : " + utilisateur);
    }
}