using System;
using System.ComponentModel.DataAnnotations;

namespace ApteanClinic.Models
{
    public class Medicine
    {
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public int Cost { get; set; }

    }
}