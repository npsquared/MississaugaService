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
    public class TagController : ControllerBase
    {
        private readonly ILogger<TagController> _logger;
        private readonly PetLicensingContext _context;

        public TagController(ILogger<TagController> logger, PetLicensingContext context)
        {

            _logger = logger;
            _context = context;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateTag(int O_Id, int P_Id)
        {
            // Check to see the database is created before running.
            Console.WriteLine($"Database path: {_context.DbPath}.");

            Console.WriteLine("Inserting a new Tag");
            var owner = _context.Owners as IEnumerable<Owner>;
            var pet = _context.Pets.Include(x => x.Breed) as IEnumerable<Pet>;

            List<Tag> tags = new List<Tag>()
            {
                new Tag { OwnerID = owner.FirstOrDefault(s => s.Id == O_Id).Id, PetId = pet.FirstOrDefault(s => s.Id == P_Id).Id}
            }; 
            
            _context.AddRange(tags);
            _context.SaveChanges();
            return Ok(tags);
        }

        [HttpGet]
        [Route("seed-tags")]
        public IActionResult SeedSomeTags()
        {
            // Check to see the database is created before running.
            Console.WriteLine($"Database path: {_context.DbPath}.");
            Console.WriteLine("Inserting a new tag");
            var owner = _context.Owners as IEnumerable<Owner>;
            var pet = _context.Pets.Include(x => x.Breed) as IEnumerable<Pet>;

            List<Tag> tags = new List<Tag>() {
                new Tag { OwnerID = owner.FirstOrDefault(s => s.FirstName == "Nitin").Id, PetId = pet.FirstOrDefault(s => s.Name == "SOPHIE").Id}
            };
            _context.AddRange(tags);
            _context.SaveChanges();
            return Ok(tags);
        }

        [HttpGet]
        [Route("list")]
        public IActionResult GetTags()
        {
            Console.WriteLine("Getting all tags");
            // Eager loading
            // Immediate Mode of Query Execution. 
            IEnumerable<Tag> tags = _context.Tags.Include(x => x.Owner).Include(x => x.Pet).Include(x => x.Pet.Breed).ToList();
            return Ok(tags);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteTagsById(int Id)
        {
            Console.WriteLine("Deleting Tag by ID");
            var Id_encoded = HttpUtility.HtmlEncode(Id);

            Tag tags = _context.Tags.Find(Convert.ToInt32(Id_encoded));
            _context.Remove<Tag>(tags);
            _context.SaveChanges();

            return Ok(tags);
        }

    }
}