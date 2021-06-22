using Microsoft.AspNetCore.Mvc;
using PeopleStoreAppDataCoontracts;
using PeopleStoreAppWeb.DataBase;
using System;
using System.Linq;

namespace PeopleStoreAppWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private LocalDataStorage db;

        public PeopleController(LocalDataStorage db)
        {
            this.db = db;
        }
        [HttpGet]
        public IActionResult GetPeople()
        {
            return Ok(db.People);
        }

        [HttpPost]
        public IActionResult AddPerson([FromBody]Person person)
        {
            db.AddPerson(person);
            return Ok();
        }
        [HttpGet("{id}/photo")]
        public IActionResult GetPhoto([FromRoute] int id)
        {
            var p = db.People.First(w => w.ID == id);
            return base.File(Convert.FromBase64String(p.PictureBase64), "image/jpeg");
        }
    }
}
