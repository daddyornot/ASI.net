using Microsoft.EntityFrameworkCore;
using Npgsql;
using TP2Console.Exercices;
using TP2Console.Models.EntityFramework;

namespace TP2Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Exo2.Q1();
            Exo2.Q2();
            Exo2.Q3();
            Exo2.Q4();
            Exo2.Q5();
            Exo2.Q6();
            Exo2.Q7();
            Exo2.Q8();
            Exo2.Q9();
        }
    }
}


// using (var ctx = new PostgresContext())
// {
// ----------------------- Modificztion dans la BDD ----------------------
// // // desactivation du tracking : aucun changement dans la base de données ne sera effectué
// // ctx.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
//
// Film titanic = ctx.Films.First(f => f.Nom.Contains("Titanic"));
// // meme requete, mais sans tracking
// // Film titanic = ctx.Films.AsNoTracking().First(f => f.Nom.Contains("Titanic"));
//
// // modification de la description du film dans le contexte SEULEMENT
// titanic.Description = "Un bateau échoué. Date : " + DateTime.Now;
//
// // sauvegarde du contexte : les changements sont sauvegardés dans la base de données
// int nbchanges = ctx.SaveChanges();
//
// Console.WriteLine("Nombre d'enregistrements modifiés ou ajoutés : " + nbchanges);

// ----------------------- Chargement Explicite ----------------------
// Chargement de la catégorie Action
// Categorie categorieAction = ctx.Categories.First(c => c.Nom == "Action");
// Console.WriteLine("Catégorie : " + categorieAction.Nom);
//
// // Chargement des films de la catégorie Action
// // Collection() pour many to many // Reference() pour one to one
// ctx.Entry(categorieAction).Collection(c => c.Films).Load();
// Console.WriteLine("Films : ");
// foreach (var film in categorieAction.Films)
// {
//     Console.WriteLine(film.Nom);
// }

// ----------------------- Chargement Hatif ----------------------
// // Chargement de la catégorie Action et des films de cette catégorie
// Categorie categorieAction = ctx.Categories
//     .Include(c => c.Films)
//     .ThenInclude(f => f.Avis)
//     .First(c => c.Nom == "Action");
// Console.WriteLine("Catégorie : " + categorieAction.Nom);
// Console.WriteLine("Films : ");
// foreach (var film in categorieAction.Films)
// {
//     Console.WriteLine(film.Nom);
// }

// ----------------------- Chargement Paresseux (Lazy Laoding) ----------------------
// Categorie categorieAction = ctx.Categories.First(c => c.Nom == "Action");
// Console.WriteLine("Catégorie : " + categorieAction.Nom);
// Console.WriteLine("Films : ");
// foreach (var film in categorieAction.Films) // Lazy Loading
// {
//     Console.WriteLine(film.Nom);
// }
// }