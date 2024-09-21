namespace TP2Console.Models.EntityFramework;

public partial class Avi
{
    public override string ToString()
    {
        return $"IdFilm: {Idfilm}, IdUtilisateur: {Idutilisateur}, Commentaire: {Commentaire}, Note: {Note}";
    }
}