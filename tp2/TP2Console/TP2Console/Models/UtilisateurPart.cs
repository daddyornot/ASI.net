namespace TP2Console.Models.EntityFramework;

// partial class Utilisateur to complete the generated class
public partial class Utilisateur
{
    
    public override string ToString()
    {
        return $"Id: {Idutilisateur}, Login: {Login}, Email: {Email}, Pwd: {Pwd}";
    }
}