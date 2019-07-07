using ApteanClinic.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApteanClinic.Database
{
    public class ApteanClinicContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<DoctorTime> DoctorTimes { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<MedicinesQuantity> Medicine_Quantity{get; set;}
        public DbSet<MedicalHistory> MedicalHistories { get; set; }

        //public System.Data.Entity.DbSet<ApteanClinic.Models.InvoiceViewModel> InvoiceViewModels { get; set; }

        //public System.Data.Entity.DbSet<ApteanClinic.Models.AppointmentListViewModel> AppointmentListViewModels { get; set; }
    }
}