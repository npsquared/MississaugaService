using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace MississaugaDbService
{
    public class Address
    {
        public int Id { get; set; }
        public int StreetNumber { get; set; }
        public string StreetName { get; set; }

        [NotMapped]
        public string FullAddress {
            get { return StreetNumber + " " + StreetName; }
        }

 //       public int OwnerId { get; set; }
 //       public Owner Owner { get; set; }
    }
}