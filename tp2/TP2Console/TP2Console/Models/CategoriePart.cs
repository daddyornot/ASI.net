namespace TP2Console.Models.EntityFramework;

public partial class Categorie
{
    public string ToString()
    {
        return $"Id: {Idcategorie}, Nom: {Nom}, Description: {Description}";
    }

}