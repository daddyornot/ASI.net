using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tp3Partie1.Models.EntityFramework;
using Tp3Partie1.Models.Repository;

namespace Tp3Partie1.Models.DataManager;

public class UtilisateurManager : IDataRepository<Utilisateur>
{
    readonly SeriesDbContext? seriesDbContext;

    public UtilisateurManager()
    {
    }

    public UtilisateurManager(SeriesDbContext context)
    {
        seriesDbContext = context;
    }

    public async Task<ActionResult<IEnumerable<Utilisateur>>> GetAllAsync()
    {
        return await seriesDbContext.Utilisateurs.ToListAsync();
    }

    public async Task<ActionResult<Utilisateur>> GetByIdAsync(int id)
    {
        return await seriesDbContext.Utilisateurs.FirstOrDefaultAsync(u => u.UtilisateurId == id);
    }

    public async Task<ActionResult<Utilisateur>> GetByStringAsync(string mail)
    {
        return await seriesDbContext.Utilisateurs.FirstOrDefaultAsync(u => u.Mail.ToUpper() == mail.ToUpper());
    }

    public async Task AddAsync(Utilisateur entity)
    {
        await seriesDbContext.Utilisateurs.AddAsync(entity);
        await seriesDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Utilisateur utilisateur, Utilisateur entity)
    {
        seriesDbContext.Entry(utilisateur).State = EntityState.Modified;
        utilisateur.UtilisateurId = entity.UtilisateurId;
        utilisateur.Nom = entity.Nom;
        utilisateur.Prenom = entity.Prenom;
        utilisateur.Mail = entity.Mail;
        utilisateur.Rue = entity.Rue;
        utilisateur.Cp = entity.Cp;
        utilisateur.Ville = entity.Ville;
        utilisateur.Pays = entity.Pays;
        utilisateur.Lat = entity.Lat;
        utilisateur.Long = entity.Long;
        utilisateur.Pwd = entity.Pwd;
        utilisateur.Mobile = entity.Mobile;
        utilisateur.NotesUtilisateur = entity.NotesUtilisateur;
        await seriesDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Utilisateur utilisateur)
    {
        seriesDbContext.Utilisateurs.Remove(utilisateur);
        await seriesDbContext.SaveChangesAsync();
    }
}