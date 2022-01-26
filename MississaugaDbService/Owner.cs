using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations.Schema;
namespace MississaugaDbService
{  
   public class Owner
    {
        public int Id { get; set; } // Id used to be type string. Changed to int. 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //   public string Address { get; set; }
        public Address Address { get; set; }
 //       public string Pets { get; set; }
        public string OwnershipDate { get; set; }

        public List<Pet> Pets { get; set; }

    }
    
}
