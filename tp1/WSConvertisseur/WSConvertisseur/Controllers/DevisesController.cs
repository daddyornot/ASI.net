﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;

namespace WSConvertisseur.Controllers
{
    /// <summary>
    /// Contrôleur pour gérer les opérations sur les devises.
    /// </summary>
    [Route("api/devises")]
    [ApiController]
    public class DevisesController : Controller
    {
        private List<Devise> devises = new List<Devise>();

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="DevisesController"/>.
        /// </summary>
        public DevisesController()
        {
            devises.Add(new Devise(1, "Dollar", 1.08));
            devises.Add(new Devise(2, "Franc Suisse", 1.07));
            devises.Add(new Devise(3, "Yen", 120));
        }

        /// <summary>
        /// Retourne toutes les devises
        /// </summary>
        /// <returns>Liste de toutes les devises</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IEnumerable<Devise> GetAll()
        {
            return devises;
        }

        /// <summary>
        /// Retourne la devise dont l'id est spécifié dans l'url. 
        /// </summary>
        /// <param name="id">ID de la devise</param>
        /// <returns>Http Response</returns>
        /// <response code="200">Devise trouvé</response>
        /// <response code="404">Devise non trouvée</response>
        [HttpGet("{id}", Name = "GetDevise")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Devise> GetById(int id)
        {
            Devise? devise = devises.FirstOrDefault(d => d.Id == id);
            return devise != null ? Ok(devise) : NotFound();
        }

        /// <summary>
        /// Crée une nouvelle devise
        /// </summary>
        /// <param name="devise">Un objet Devise</param>
        /// <returns>Http Response</returns>
        /// <response code="201">Devise créée</response>
        /// <response code="400">Mauvais format de requete</response>
        [HttpPost()]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<Devise> Post([FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            devises.Add(devise);
            return CreatedAtRoute("GetDevise", new { id = devise.Id }, devise);
        }

        /// <summary>
        /// Modifie une devise existante
        /// </summary>
        /// <param name="id">L'id de la devise à modifier</param>
        /// <param name="devise">Un objet devise modifié</param>
        /// <returns>Http Response</returns>
        /// <response code="204">Devise modifiée</response>
        /// <response code="400">Mauvais format de requete</response>
        /// <response code="404">Devise non trouvée</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult Put(int id, [FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != devise.Id)
            {
                return BadRequest();
            }
            int index = devises.FindIndex((d) => d.Id == id);
            if (index < 0)
            {
                return NotFound();
            }
            devises[index] = devise;
            return NoContent();
        }

        /// <summary>
        /// Supprime une devise
        /// </summary>
        /// <param name="id">L'id de la devise à supprimer</param>
        /// <returns>Http Response</returns>
        /// <response code="200">Devise supprimée</response>
        /// <response code="404">Devise non trouvée</response>
        [HttpDelete("{id}", Name = "DeleteDevise")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult Delete(int id)
        {
            Devise? devise =
                (from d in devises
                 where d.Id == id
                 select d).FirstOrDefault();
            if (devise == null)
            {
                return NotFound();
            }
            devises.Remove(devise);
            return Ok("La devise " + devise.ToString() + " à été supprimée.");
        }
    }
}
