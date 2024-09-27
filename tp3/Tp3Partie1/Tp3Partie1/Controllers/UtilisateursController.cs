using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tp3Partie1.Models.EntityFramework;

namespace Tp3Partie1.Controllers
{
    /// <summary>
    ///  Controller pour les utilisateurs
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateursController : ControllerBase
    {
        private readonly SeriesDbContext _context;

        public UtilisateursController(SeriesDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retourne tous les utilisateurs
        /// </summary>
        /// <returns> Liste de tous les utilisateurs</returns>
        /// <response code="200">Utilisateurs trouvés</response>
        [HttpGet]
        [ProducesResponseType<List<Utilisateur>>(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
        {
            return await _context.Utilisateurs.ToListAsync();
        }

        /// <summary>
        ///  Retourne l'utilisateur dont l'id est spécifié dans l'url
        /// </summary>
        /// <param name="id">L'id de l'utilisateur</param>
        /// <returns>Http Response</returns>
        /// <response code="200">Utilisateur trouvé</response>
        /// <response code="404">Utilisateur non trouvé</response>
        [HttpGet]
        [ProducesResponseType<Utilisateur>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("[action]/{id:int}")]
        [ActionName("GetById")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateurById(int id)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(id);

            if (utilisateur == null)
            {
                return NotFound("Utilisateur non trouvé");
            }

            return utilisateur;
        }

        /// <summary>
        /// Retourne l'utilisateur dont l'email est spécifié dans l'url
        /// </summary>
        /// <param name="email">L'email de l'utilisateur</param>
        /// <returns>Http Response</returns>
        /// <response code="200">Utilisateur trouvé</response>
        /// <response code="404">Utilisateur non trouvé</response>
        /// <response code="400">Mauvais format de requete</response>
        [HttpGet]
        [ProducesResponseType<Utilisateur>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("[action]/{email}")]
        [ActionName("GetByEmail")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateurByEmail(string email)
        {
            var utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(u => u.Mail.ToLower() == email.ToLower());

            if (utilisateur == null)
            {
                return NotFound("Utilisateur non trouvé");
            }

            return utilisateur;
        }

        /// <summary>
        ///  Modifie un utilisateur existant
        /// </summary>
        /// <param name="id"> L'id de l'utilisateur à modifier</param>
        /// <param name="utilisateur"> Un objet utilisateur modifié</param>
        /// <returns>Http Response</returns>
        /// <response code="204">Utilisateur modifié</response>
        /// <response code="404">Utilisateur non trouvé</response>
        /// <response code="400">Mauvais format de requete</response>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutUtilisateur(int id, Utilisateur utilisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != utilisateur.UtilisateurId)
            {
                return BadRequest("L'id de l'utilisateur ne correspond pas");
            }

            _context.Entry(utilisateur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurExists(id))
                {
                    return NotFound("Utilisateur non trouvé");
                }

                throw;
            }

            return NoContent();
        }

        /// <summary>
        ///  Crée un nouvel utilisateur
        /// </summary>
        /// <param name="utilisateur"> Un objet utilisateur</param>
        /// <returns> Http Response</returns>
        /// <response code="201">Utilisateur créé</response>
        /// <response code="400">Mauvais format de requete</response>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<Utilisateur>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUtilisateur", new { id = utilisateur.UtilisateurId }, utilisateur);
        }

        // DELETE: api/Utilisateurs/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteUtilisateur(int id)
        // {
        //     var utilisateur = await _context.Utilisateurs.FindAsync(id);
        //     if (utilisateur == null)
        //     {
        //         return NotFound("Utilisateur non trouvé");
        //     }
        //
        //     _context.Utilisateurs.Remove(utilisateur);
        //     await _context.SaveChangesAsync();
        //
        //     return NoContent();
        // }

        private bool UtilisateurExists(int id)
        {
            return _context.Utilisateurs.Any(e => e.UtilisateurId == id);
        }
    }
}