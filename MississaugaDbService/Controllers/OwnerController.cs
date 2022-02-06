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
    public class OwnerController : Controller
    {
        private readonly ILogger<OwnerController> _logger;
        private readonly PetLicensingContext _context;

        public OwnerController(ILogger<OwnerController> logger, PetLicensingContext context)
        {

            _logger = logger;
            _context = context;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateOwner(string FN, string LN, int St_num, string St_name)
        {
            // Check to see the database is created before running.
            Console.WriteLine($"Database path: {_context.DbPath}.");

            Console.WriteLine("Inserting a new Owner");

            var addr = _context.Addresses as IEnumerable<Address>;

            // Creating a new Address
            Address addresses = new Address { StreetNumber = St_num, StreetName = St_name };
            _context.Add(addresses);
            _context.SaveChanges();

            // Creating a new Owner
            Owner owners = new Owner { FirstName = FN, LastName = LN, Address = addr.FirstOrDefault(x => x.FullAddress == St_num + " " + St_name), OwnershipDate = DateTime.Now.ToString() };

            _context.Add(owners);
            _context.SaveChanges();
            return Ok(owners);
        }

        [HttpPost]
        [Route("addOwnerPet")]
        public IActionResult AddPetToOwner(int o_id, int p_id)
        {
            // Check to see the database is created before running.
            Console.WriteLine($"Database path: {_context.DbPath}.");

            Console.WriteLine("Updating Owner record with Pet");
            IEnumerable<Pet> pets = _context.Pets.Where(x => x.Id == p_id).ToList();

            Owner owner = _context.Owners.Find(o_id);
            owner.Pets.AddRange(pets);

//            _context.Owners.Update(pets);
            _context.SaveChanges();
            return Ok(owner);
        }

        [HttpGet]
        [Route("list")]
        public IActionResult GetOwners()
        {
            Console.WriteLine("Getting all owners");
            var owners = _context.Owners.Include(x => x.Address);
            return Ok(owners);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteOwnersById(int Id)
        {
            Console.WriteLine("Deleting Owner by ID");
            var Id_encoded = HttpUtility.HtmlEncode(Id);

            Owner owners = _context.Owners.Find(Convert.ToInt32(Id_encoded));
            _context.Remove<Owner>(owners);
            _context.SaveChanges();

            return Ok(owners);
        }


    }
}