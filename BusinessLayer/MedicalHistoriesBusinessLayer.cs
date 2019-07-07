using ApteanClinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using ApteanClinic.Database;
using ApteanClinic.BusinessLayer;
using System.Diagnostics;

namespace BusinessLayer
{
    public class MedicalHistoriesBusinessLayer
    {

        private HistoryDataLayer historyDataLayer;
        private PatientDataLayer patientDataLayer;
        private AppointmentDataLayer appointmentDataLayer;
        private List<Appointment> appointmentList;
        private MedicalHistoryViewModel medicalHistoryViewModel;
        private List<MedicalHistoryViewModel> medicalHistoryViewModelList;
        private PatientBusinessLayer patientBusinessLayer;
        private DoctorBusinessLayer doctorBusinessLayer;

        public MedicalHistoriesBusinessLayer()
        {
            historyDataLayer = new HistoryDataLayer();
        }
        public void AddHistory(MedicalHistory medicalHistory)
        {
            try
            {
                historyDataLayer.AddMedicalHistory(medicalHistory);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public List<MedicalHistoryViewModel> GetMedicalHistory(int ?id)
        {
            try { 
            medicalHistoryViewModelList = new List<MedicalHistoryViewModel>();
            patientBusinessLayer = new PatientBusinessLayer();
            doctorBusinessLayer = new DoctorBusinessLayer();
            List<MedicalHistory> medicalHistories;
            if (id == null)
            {
                medicalHistories = historyDataLayer.GetAllMedicalHistory();
            }
            else
            {
                patientDataLayer = new PatientDataLayer();
                medicalHistories = patientDataLayer.GetPatientMedicalHistory((int) id);
            }
            foreach (var medicalHistory in medicalHistories)
            {
                appointmentList = new List<Appointment>();
                appointmentDataLayer = new AppointmentDataLayer();
                appointmentList = appointmentDataLayer.GetAppointmentsById(medicalHistory.PatientId, medicalHistory.AppointmentId);

                if (medicalHistory.AppointmentId != 0)
                {
                    foreach (var appointment in appointmentList)
                    {
                        medicalHistoryViewModel = new MedicalHistoryViewModel
                        {
                            AppointmentId = medicalHistory.AppointmentId,
                            PatientName = patientBusinessLayer.GetPatientNameById(appointment.PatientId),
                            DoctorName = doctorBusinessLayer.GetDoctorNameById(appointment.DoctorId),
                            Date = appointment.Date.ToShortDateString(),
                            Dignosis = medicalHistory.Dignosis,
                            Medicine = medicalHistory.Medicine,
                            ClinicRemark = medicalHistory.ClinicRemark
                        };
                        medicalHistoryViewModelList.Add(medicalHistoryViewModel);
                    }
                }
                else
                {
                    medicalHistoryViewModel = new MedicalHistoryViewModel
                    {
                        AppointmentId = -1,
                        PatientName = patientBusinessLayer.GetPatientNameById(medicalHistory.PatientId),
                        DoctorName = "-",
                        Dignosis = medicalHistory.Dignosis,
                        Medicine = medicalHistory.Medicine,
                        ClinicRemark = medicalHistory.ClinicRemark

                    };
                    medicalHistoryViewModelList.Add(medicalHistoryViewModel);
                }
            }
            return medicalHistoryViewModelList;
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }

        }

        public void Dispose()
        {
            try
            {
                historyDataLayer.Dispose();
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
    }
}
