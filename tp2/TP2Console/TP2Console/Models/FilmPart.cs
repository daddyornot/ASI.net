namespace TP2Console.Models.EntityFramework;

public partial class Film
{
    public override string ToString()
    {
        return $"Id: {Idfilm}, Titre: {Nom}, Description: {Description}";
    }
}