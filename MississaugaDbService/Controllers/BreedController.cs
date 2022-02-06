using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Web;

namespace MississaugaDbService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BreedController : Controller
    {
        private readonly ILogger<BreedController> _logger;
        private readonly PetLicensingContext _context;

        public BreedController(ILogger<BreedController> logger, PetLicensingContext context)
        {

            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllBreeds()
        {
            Console.WriteLine("Getting all breeds");
            var breeds = _context.Breeds;  // I think there was a typo here. The line used to be  var breeds = _context.Owners;
            return Ok(breeds);
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetBreeds(string animal, string breedtype)
        {
            Console.WriteLine("Getting breeds");
            var breeds = _context.Breeds as IEnumerable<Breed>;
            var animal_encoded = HttpUtility.HtmlEncode(animal); // Html encoding the user input for animal, to prevent XSS attack. 
            var breedtype_encoded = HttpUtility.HtmlEncode(breedtype); // Html encoding the user input for breedtype, to prevent XSS attack.

            if (!string.IsNullOrEmpty(animal_encoded) && string.IsNullOrEmpty(breedtype_encoded)) // filter by animal only
            {
              var result = breeds.Where(x => x.AnimalType == animal_encoded);
                if (result.Any())
                {
                    return Ok(result);
                }
                // The Todo did not indicate what to do when the resulting list is empty. So I've chosen to handle it using a list of object type Breed, which contains
                // one record with blank values. 
                // If the result set is empty, a default record of type Breed with empty values is returned, i.e. Id = 0, Name = "", AnimalType = ""
                else
                {
                    List<Breed> bds = new List<Breed>() {
                new Breed { Name="", AnimalType="" }
                };
                    return Ok(bds);
                }
            } else if(string.IsNullOrEmpty(animal_encoded) && !string.IsNullOrEmpty(breedtype_encoded)) // filter by breedtype only
            {
                var result = breeds.Where(x => x.Name == breedtype_encoded);
                if (result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    List<Breed> bds = new List<Breed>() {
                new Breed { Name="", AnimalType="" }
                };
                    return Ok(bds);
                }
            } else if(!string.IsNullOrEmpty(animal_encoded) && !string.IsNullOrEmpty(breedtype_encoded)) // filter by animal and breedtype
            {
                var result = breeds.Where(x => x.AnimalType == animal_encoded)
                                    .Where(x => x.Name == breedtype_encoded);
                if (result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    List<Breed> bds = new List<Breed>() {
                new Breed { Name="", AnimalType="" }
                };
                    return Ok(bds);
                }
            }
            else // no filters passed. animal and breedtype are empty or null. 
            {
                var result = breeds.Where(x => x.AnimalType == "")
                                    .Where(x => x.Name == "");
                if (result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    List<Breed> bds = new List<Breed>() {
                new Breed { Name="", AnimalType="" }
                };
                    return Ok(bds);
                }
            }
            
            
        }

        [HttpGet]
        [Route("seed-breeds")]
        public IActionResult SeedSomeBreeds()
        {
            // Check to see the database is created before running.
            Console.WriteLine($"Database path: {_context.DbPath}.");
            Console.WriteLine("Inserting a new breed");
            Listservice ls = new Listservice();
            List<Breed> breeds = ls.CreateListBreeds(); // list of breeds is retrived from listservice.cs which seeds a list of breeds

            _context.AddRange(breeds);
            _context.SaveChanges();
            return Ok(breeds);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteBreedsById(int Id)
        {
            Console.WriteLine("Deleting breed by Id");
            var Id_encoded = HttpUtility.HtmlEncode(Id);

            Breed breeds = _context.Breeds.Find(Convert.ToInt32(Id_encoded));
            _context.Remove<Breed>(breeds);
            _context.SaveChanges();

            return Ok(breeds);
        }

    }
}