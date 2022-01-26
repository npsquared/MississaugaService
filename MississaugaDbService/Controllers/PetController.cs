using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace MississaugaDbService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ControllerBase
    {
        private readonly ILogger<PetController> _logger;
        private readonly PetLicensingContext _context;

        public PetController(ILogger<PetController> logger, PetLicensingContext context)
        {

            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllPets()
        {
            Console.WriteLine("Getting all pets");
            // Eager loading
            // Immediate Mode of Query Execution. 
            IEnumerable<Pet> pets = _context.Pets.Include(x => x.Breed).Include( x => x.Owner).ToList();
            return Ok(pets);
        }

        [HttpGet]
        [Route("seed-pets")]
        public IActionResult SeedSomePets()
        {
            // Check to see the database is created before running.
            Console.WriteLine($"Database path: {_context.DbPath}.");
            Console.WriteLine("Inserting a new pet");
            var b = _context.Breeds as IEnumerable<Breed>;
            var o = _context.Owners as IEnumerable<Owner>;

            List<Pet> pets = new List<Pet>() {
                new Pet { Name="NewPetDog", BreedId = b.FirstOrDefault(s => s.Name == "HUSKY").Id, OwnerId = o.FirstOrDefault(s => s.Id == 1).Id},
                new Pet { Name="AnotherPetDog", BreedId = b.FirstOrDefault(s => s.Name == "HUSKY").Id, OwnerId = o.FirstOrDefault(s => s.Id == 1).Id}
            };
            _context.AddRange(pets);
            _context.SaveChanges();
            return Ok(pets);
        }

        [HttpGet]
        [Route("get")]
        public IActionResult SelectPets(string breedname) //, string name)
        {
            Console.WriteLine("Getting breeds");
 //           var name_encoded = HttpUtility.HtmlEncode(name); 
            var breedname_encoded = HttpUtility.HtmlEncode(breedname);

            // Eager loading
            IEnumerable<Pet> petList = _context.Pets.Include(s=>s.Breed).Include(s => s.Owner).Where(x=>x.Breed.Name==breedname_encoded).ToList();
            return Ok(petList);
        }

        [HttpGet]
        [Route("getbyOwnerId")]
        public IActionResult SelectPetsbyOwnerId(int O_Id) //, string name)
        {
            Console.WriteLine("Getting breeds");
            //           var name_encoded = HttpUtility.HtmlEncode(name); 
            var O_Id_encoded = HttpUtility.HtmlEncode(O_Id);

            // Eager loading
            IEnumerable<Pet> petList = _context.Pets.Include(s => s.Breed).Include(s => s.Owner).Where(x => x.Owner.Id == Convert.ToInt32(O_Id_encoded)).ToList();
            return Ok(petList);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeletePetsById(int Id)
        {
            Console.WriteLine("Deleting Pet by ID");
            var Id_encoded = HttpUtility.HtmlEncode(Id);

            Pet pets = _context.Pets.Find(Convert.ToInt32(Id_encoded));
            _context.Remove<Pet>(pets);
            _context.SaveChanges();

            return Ok(pets);
        }

    }
}