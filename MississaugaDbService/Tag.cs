using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MississaugaDbService
{
    public class Tag
    {
        public int Id { get; set; }
        public int OwnerID { get; set; }
        public int PetId { get; set; }
        public Owner Owner { get; set; }
        public Pet Pet { get; set; } 
    }
}