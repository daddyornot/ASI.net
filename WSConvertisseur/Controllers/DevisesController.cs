using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;

namespace WSConvertisseur.Controllers
{
    [Route("api/devises")]
    [ApiController]
    public class DevisesController : Controller
    {
        private List<Devise> devises = new List<Devise>();

        public DevisesController()
        {
            devises.Add(new Devise(1, "Dollar", 1.08));
            devises.Add(new Devise(2, "Franc Suisse", 1.07));
            devises.Add(new Devise(3, "Yen", 120));
        }

        // GET: DevisesController
        [HttpGet]
        public IEnumerable<Devise> GetAll()
        {
            return devises;
        }

        // GET: DevisesController/Details/5
        [HttpGet("{id}", Name = "GetDevise")]
        public ActionResult<Devise> GetById(int id)
        {
            Devise? devise = devises.FirstOrDefault(d => d.Id == id);
            return devise != null ? Ok(devise) : NotFound();
        }

        // POST: DevisesController
        [HttpPost()]
        public ActionResult<Devise> Post([FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            devises.Add(devise);
            return CreatedAtRoute("GetDevise", new { id = devise.Id }, devise);
        }

        // POST: DevisesController/Create
        [HttpPost("/create/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DevisesController/Edit/5
        [HttpGet("/edit/{id}")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DevisesController/Edit/5
        [HttpPost("/edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DevisesController/Delete/5
        [HttpDelete("{id}", Name = "DeleteDevise")]
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

        // POST: DevisesController/Delete/5
        [HttpPost("/delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
