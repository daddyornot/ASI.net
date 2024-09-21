using Microsoft.EntityFrameworkCore;
using Npgsql;
using TP2Console.Models.EntityFramework;

namespace TP2Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Afficher les films
            // Exo2Q1();
            // Afficher les emails des utilisateurs
            // Exo2Q2();
            // Afficher les utilisateurs triés par login croissant
            // Exo2Q3();
            // Afficher les noms et id des films de la catégorie Action
            // Exo2Q4();
            // Afficher le nombre de catégories
            // Exo2Q5();
            // Afficher la note la plus basse
            // Exo2Q6();
            // Afficher les films commençant par "le" non sensibles à la casse
            // Exo2Q7();
            // Afficher la note moyenne de Pulp Fiction insensible à la casse
            // Exo2Q8();
            //
            Exo2Q9();
        }

        public static void Exo2Q1()
        {
            var ctx = new PostgresContext();
            foreach (var film in ctx.Films)
            {
                Console.WriteLine(film.ToString());
            }
        }

        // Afficher les emails des utilisateurs
        public static void Exo2Q2()
        {
            var ctx = new PostgresContext();
            foreach (var utilisateur in ctx.Utilisateurs)
            {
                Console.WriteLine(utilisateur.Email);
            }
        }


        // Afficher les utilisateurs triés par login croissant
        public static void Exo2Q3()
        {
            var ctx = new PostgresContext();
            foreach (var utilisateur in ctx.Utilisateurs.OrderBy(u => u.Login))
            {
                Console.WriteLine(utilisateur.ToString());
            }
        }

        // Afficher les noms et id des films de la catégorie Action
        public static void Exo2Q4()
        {
            var ctx = new PostgresContext();
            Categorie categorieAction = ctx.Categories
                .Include(c => c.Films)
                .ThenInclude(f => f.Avis)
                .First(c => c.Nom == "Action");
            Console.WriteLine("Dans la catégorie " + categorieAction.Nom + " il y a les films suivants : ");
            foreach (var film in categorieAction.Films)
            {
                Console.WriteLine("Nom du film : " + film.Nom + " - Id du film : " + film.Idfilm);
            }
        }

        // Afficher le nombre de catégories
        public static void Exo2Q5()
        {
            var ctx = new PostgresContext();
            Console.WriteLine("Nombre de catégories : " + ctx.Categories.Count());
        }

        // Afficher la note la plus basse
        public static void Exo2Q6()
        {
            var ctx = new PostgresContext();
            Console.WriteLine("Note la plus basse : " + ctx.Avis.Min(a => a.Note));
        }

        // Afficher les films commençant par "le" non sensibles à la casse
        public static void Exo2Q7()
        {
            var ctx = new PostgresContext();
            // List<Film> films = ctx.Films
            //     .Where(f => f.Nom.ToLower().StartsWith("le"))
            //     .ToList();
            
            List<Film> films = ctx.Films
                .Where(f => EF.Functions.Like(f.Nom.ToLower(), "le%"))
                .ToList();
            
            Console.WriteLine($"Films commençant par 'le' ({films.Count} résultats) : ");
            foreach (var film in films)
            {
                Console.WriteLine(film.Nom);
            }
        }

        // Afficher la note moyenne de Pulp Fiction insensible à la casse
        public static void Exo2Q8()
        {
            var ctx = new PostgresContext();
            
            decimal noteMoyenne = ctx.Films
                .Include(f => f.Avis)
                .Where(f => f.Nom.ToLower() == "pulp fiction")
                .SelectMany(f => f.Avis)
                .Average(a => a.Note);
    
            Console.WriteLine("Note moyenne de Pulp Fiction : " + noteMoyenne);
        }

        // Afficher l'utiliseateur ayant mis la note la plus haute
        public static void Exo2Q9()
        {
            var ctx = new PostgresContext();
            Utilisateur utilisateur = ctx.Utilisateurs
                .Include(u => u.Avis)
                .OrderByDescending(u => u.Avis.Max(a => a.Note))
                .First();

            Console.WriteLine("Utilisateur ayant mis la note la plus haute : " + utilisateur.Login);
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