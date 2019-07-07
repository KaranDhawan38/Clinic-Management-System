namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DoctorId = c.Int(nullable: false),
                        NurseId = c.Int(nullable: false),
                        PatientId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Time = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Speciality = c.Int(nullable: false),
                        Fees = c.Double(nullable: false),
                        DoctorUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.DoctorUser_Id)
                .Index(t => t.DoctorUser_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 80),
                        Gender = c.Int(nullable: false),
                        BloodGroup = c.Int(nullable: false),
                        Contact = c.String(nullable: false, maxLength: 10),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 16),
                        ConfirmPassword = c.String(nullable: false),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DoctorTimes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeId = c.Int(nullable: false),
                        Doctor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.Doctor_Id)
                .Index(t => t.Doctor_Id);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Appointment_Id = c.Int(nullable: false),
                        Total = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MedicalHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppointmentId = c.Int(nullable: false),
                        PatientId = c.Int(nullable: false),
                        Dignosis = c.String(),
                        Medicine = c.String(),
                        ClinicRemark = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MedicinesQuantities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Appointment_Id = c.Int(nullable: false),
                        Medicine_Id = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                        MedicineRate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Medicines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Cost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Nurses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Availability = c.Boolean(nullable: false),
                        NurseUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.NurseUser_Id)
                .Index(t => t.NurseUser_Id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Height = c.Single(nullable: false),
                        Weight = c.Single(nullable: false),
                        Address = c.String(),
                        EmergengyContactName = c.String(nullable: false),
                        EmergencyContactNumber = c.String(nullable: false, maxLength: 10),
                        PatientUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.PatientUser_Id)
                .Index(t => t.PatientUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "PatientUser_Id", "dbo.Users");
            DropForeignKey("dbo.Nurses", "NurseUser_Id", "dbo.Users");
            DropForeignKey("dbo.DoctorTimes", "Doctor_Id", "dbo.Doctors");
            DropForeignKey("dbo.Doctors", "DoctorUser_Id", "dbo.Users");
            DropIndex("dbo.Patients", new[] { "PatientUser_Id" });
            DropIndex("dbo.Nurses", new[] { "NurseUser_Id" });
            DropIndex("dbo.DoctorTimes", new[] { "Doctor_Id" });
            DropIndex("dbo.Doctors", new[] { "DoctorUser_Id" });
            DropTable("dbo.Patients");
            DropTable("dbo.Nurses");
            DropTable("dbo.Medicines");
            DropTable("dbo.MedicinesQuantities");
            DropTable("dbo.MedicalHistories");
            DropTable("dbo.Invoices");
            DropTable("dbo.DoctorTimes");
            DropTable("dbo.Users");
            DropTable("dbo.Doctors");
            DropTable("dbo.Appointments");
        }
    }
}
