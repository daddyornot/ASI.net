using TP2Console.Models.EntityFramework;

namespace TP2Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new PostgresContext())
            {
                Film titanic = ctx.Films.First(f => f.Nom.Contains("Titanic"));

                titanic.Description = "Un bateau qui coule. Date : " + DateTime.Now;

                int nbchanges = ctx.SaveChanges();

                Console.WriteLine("Nombre d'enregistrements modifiés ou ajoutés : " + nbchanges);

            }
        }
    }
}
