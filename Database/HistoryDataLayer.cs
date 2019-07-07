using ApteanClinic.Database;
using ApteanClinic.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class HistoryDataLayer
    {
        public MedicalHistory AddMedicalHistory(MedicalHistory medicalHistory)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    context.MedicalHistories.Add(medicalHistory);
                    context.SaveChanges();
                    return medicalHistory;
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public List<MedicalHistory> GetMedicalHistories(int? id)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.MedicalHistories.Where(a => a.PatientId == (int)id).ToList();
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public List<MedicalHistory> GetAllMedicalHistory()
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.MedicalHistories.ToList();
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public void Dispose()
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    context.Dispose();
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
    }
}
