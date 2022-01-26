using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace MississaugaDbService
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
   //     public string Breed { get; set; }
        
        public int BreedId { get; set; }

        public Breed Breed { get; set; }
        public int? OwnerId { get; set; }
        public Owner Owner { get; set; }

    }
}