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
    public class AddressController : ControllerBase
    {
        private readonly ILogger<AddressController> _logger;
        private readonly PetLicensingContext _context;

        public AddressController(ILogger<AddressController> logger, PetLicensingContext context)
        {

            _logger = logger;
            _context = context;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateAddress(int St_num, String St_name)
        {
            // Check to see the database is created before running.
            Console.WriteLine($"Database path: {_context.DbPath}.");

            Console.WriteLine("Inserting a new Address");
            Address addresses = new Address { StreetNumber = St_num, StreetName = St_name };

            _context.AddRange(addresses);
            _context.SaveChanges();
            return Ok(addresses);
        }

        [HttpGet]
        [Route("seed-addresses")]
        public IActionResult SeedSomeAddresses()
        {
            // Check to see the database is created before running.
            Console.WriteLine($"Database path: {_context.DbPath}.");
            Console.WriteLine("Inserting a new address");
            List<Address> addresses = new List<Address>() {
                new Address { StreetNumber = 456, StreetName = "PleasantView Av."},
                new Address { StreetNumber = 789, StreetName = "HappyPlace St."},
                new Address { StreetNumber = 101, StreetName = "Binary Street"}
            };
            _context.AddRange(addresses);
            _context.SaveChanges();
            return Ok(addresses);
        }

        [HttpGet]
        [Route("list")]
        public IActionResult GetAddress()
        {
            Console.WriteLine("Getting all Addresses");
            // Eager loading
            // Immediate Mode of Query Execution. 
            IEnumerable<Address> addresses = _context.Addresses.ToList();
            return Ok(addresses);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteAddressesById(int Id)
        {
            Console.WriteLine("Deleting Address by ID");
            var Id_encoded = HttpUtility.HtmlEncode(Id);

            Address addresses = _context.Addresses.Find(Convert.ToInt32(Id_encoded));
            _context.Remove<Address>(addresses);
            _context.SaveChanges();

            return Ok(addresses);
        }

    }
}