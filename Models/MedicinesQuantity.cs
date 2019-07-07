using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ApteanClinic.Models
{
   public class MedicinesQuantity
    {
        public int Id { get; set; }
        public int Appointment_Id { get; set; }
        public int Medicine_Id { get; set; }
        public int quantity { get; set; }
        public int MedicineRate { get; set; }
        
    }
}
