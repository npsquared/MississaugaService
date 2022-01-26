using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace MississaugaDbService
{
    public class Listservice
    {
        public List<Breed> CreateListBreeds()
        {

            // Creating a List of Breed objects
            List<Breed> breeds = new List<Breed>() {
                //new Breed { Name="BURMESE", AnimalType="CAT" },
                //new Breed { Name="CHARTREUX", AnimalType="CAT" },
                //new Breed { Name="COLORPOINT", AnimalType="CAT" },
                //new Breed { Name="CORNISH REX", AnimalType="CAT" },
                //new Breed { Name="CYMRIC", AnimalType="CAT" },
                //new Breed { Name="PAPILLON", AnimalType="DOG" },
                //new Breed { Name="PEKINGESE", AnimalType="DOG" },
                //new Breed { Name="PHARAOH HOUND", AnimalType="DOG" },
                //new Breed { Name="PICARDY SHEEPDOG", AnimalType="DOG" },
                //new Breed { Name="PIT BULL", AnimalType="DOG" }
                new Breed { Name="HUSKY", AnimalType="DOG" }
            };
            return breeds;

        }

        //public List<Owner> CreateListOwners(string FN, string LN, string StNum, string StName, string P)
        //{
        //    var address = _context.Addresses as IEnumerable<Address>;

        //    Creating a List of Owner objects
        //    List<Owner> owners = new List<Owner>() {
        //        new Owner {FirstName = FN, LastName = LN, Address = address.FirstOrDefault(x => x.FullAddress == StNum + " " + StName), Pets = P , OwnershipDate = DateTime.Now.ToString() }
        //    };
        //    return owners;

        //}

        //public List<Pet> CreateListPets()
        //{

        //    // Creating a List of Pet objects
        //    List<Pet> pets = new List<Pet>() {
        //        new Pet { Name="NewHUSKYPet", BreedId = b.Single(s => s.Name == "HUSKY").Id} 
        //    };
        //    return pets;

        //}

    }
}
