using ApteanClinic.Database;
using ApteanClinic.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class MedicineDataLayer
    {
        public void AddMedicine(Medicine medicine)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    context.Medicines.Add(medicine);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public object GetAllMedicines()
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.Medicines.ToList();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public Medicine GetMedicineById(int? id)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.Medicines.Where(m => m.Id == id).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public void UpdateMedicine(Medicine medicine)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    context.Entry(medicine).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public void DeleteMedicineById(Medicine medicine)
        {
          
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    context.Entry(medicine).State = EntityState.Deleted;
                    context.Medicines.Remove(medicine);
                    context.SaveChanges();

                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }


    }
}
