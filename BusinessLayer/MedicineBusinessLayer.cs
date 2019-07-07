using ApteanClinic.Database;
using ApteanClinic.Models;
using Database;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class MedicineBusinessLayer
    {
        private MedicineDataLayer medicineDataLayer;
        public MedicineBusinessLayer()
        {
            medicineDataLayer = new MedicineDataLayer();
        }
        public object GetAllMedicines()
        {
            try
            {
                return medicineDataLayer.GetAllMedicines();
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public void AddMedicine(Medicine medicine)
        {
            try
            {
                medicineDataLayer.AddMedicine(medicine);
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
                return medicineDataLayer.GetMedicineById(id);
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
                medicineDataLayer.UpdateMedicine(medicine);
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
                medicineDataLayer.DeleteMedicineById(medicine);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }


    }
}
