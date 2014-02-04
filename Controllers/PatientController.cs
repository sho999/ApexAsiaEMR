using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// simon added namespaces below
using ApexAsiaDAL;
using ApexAsiaEMR.Models;
using System.Reflection;

namespace ApexAsiaEMR.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private ApexAsiaDAL.DAL _db = new ApexAsiaDAL.DAL();

        //
        // GET: /Patient/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegisterPatient()
        {
            ViewBag.currentLoginUsername = @User.Identity.Name;
            return View();
        }


        public ActionResult PatientDetails(int id)
        {
            if (id > 0)
            {
                Patient patient = this._db.GetPatientById(id);
                ViewBag.currentPatient = patient;
            }

            return View();
        }

        public ActionResult ViewPatientAppointments(int id)
        {
            List<viewAppointment> appointments = new List<viewAppointment>();
            if (id > 0)
            {
                Patient patient = this._db.GetPatientById(id);
                ViewBag.currentPatient = patient;
                appointments = this._db.GetAppointmentViewsByPatientId(id);
            }

            return View(appointments);
        }

        private PatientMedHist InsertNewPatientMedHistForPatientId(int patientId, PatientMedHist newMedHist)
        {
            newMedHist.PatientId = patientId;
            newMedHist.EnteredBy = User.Identity.Name;
            newMedHist.EnteredDateTime = DateTime.Now;
            newMedHist.Q1 = 1;
            newMedHist.Q2 = 2;
            newMedHist.Q3 = 3;
            newMedHist.Q4 = 4;
            newMedHist.Q5 = 5;
            newMedHist.Q6 = 6;
            newMedHist.Q7 = 7;
            newMedHist.Q8 = 8;
            newMedHist.Q9 = 9;
            newMedHist.Q10 = 10;
            newMedHist.Q11 = 11;
            newMedHist.Q12 = 12;
            newMedHist.Q13 = 13;
            newMedHist.Q14 = 14;
            newMedHist.Q15 = 15;
            newMedHist.Q16 = 16;
            newMedHist.Q17 = 17;
            newMedHist.Q18 = 18;
            newMedHist.Q19 = 19;
            newMedHist.Q20 = 20;

            newMedHist.Q21 = 21;
            newMedHist.Q22 = 22;
            newMedHist.Q23 = 23;
            newMedHist.Q24 = 24;
            newMedHist.Q25 = 25;
            newMedHist.Q26 = 26;
            newMedHist.Q27 = 27;
            newMedHist.Q28 = 28;
            newMedHist.Q29 = 29;
            newMedHist.Q30 = 30;

            newMedHist.Q31 = 31;
            newMedHist.Q32 = 32;
            newMedHist.Q33 = 33;
            newMedHist.Q34 = 34;
            newMedHist.Q35 = 35;
            newMedHist.Q36 = 36;
            newMedHist.Q37 = 37;
            newMedHist.Q38 = 38;
            newMedHist.Q39 = 39;
            newMedHist.Q40 = 40;
            newMedHist.Q41 = 41;
            newMedHist.Q42 = 42;
            newMedHist.Q43 = 43;
            _db.InsertPatientMedHist(newMedHist);
            return newMedHist;
        }

        [Authorize(Roles = "Admin,Manager,Physician,Nurse")]
        public ActionResult MedHistory(int id)
        {
            MedHistViewModel viewModel = new MedHistViewModel();
            List<LegendDisease> diseaseList = new List<LegendDisease>();
            if (id > 0)
            {
                diseaseList = _db.GetAllLegendDiseases();
                Patient patient = this._db.GetPatientById(id);

                PatientMedHist medHist = new PatientMedHist();
                medHist = _db.GetPatientMedHistByPatientId(id);

                if (medHist != null)
                {
                    viewModel = populateMedHistViewModel(viewModel, diseaseList, medHist);
                }
                else
                {
                    PatientMedHist newMedHist = new PatientMedHist();
                    newMedHist = InsertNewPatientMedHistForPatientId(id, newMedHist);
                    viewModel = populateMedHistViewModel(viewModel, diseaseList, newMedHist);
                }

                PatientFamilyHist familyHist = new PatientFamilyHist();
                familyHist = _db.GetPatientFamilyHistByPatientId(id);
                if (familyHist != null)
                {
                    viewModel.PatientFamilyHistObj = familyHist;
                }
                else
                {
                    PatientFamilyHist newFamilyHist = new PatientFamilyHist();
                    newFamilyHist = InsertNewFamilyHistForPatientId(id, newFamilyHist);
                    viewModel.PatientFamilyHistObj = newFamilyHist;
                }

                PatientVaccination vaccination = this._db.GetPatientVaccinationByPatientId(id);
                if (vaccination != null)
                {
                    viewModel.PatientVaccinationObj = vaccination;
                }
                else
                {
                    PatientVaccination newVaccination = new PatientVaccination();
                    newVaccination.PatientId = id;
                    newVaccination.EnteredBy = User.Identity.Name;
                    newVaccination.EnteredDateTime = DateTime.Now;

                    this._db.InsertPatientVaccination(newVaccination);
                }

                CurrentMedication currentMedication = this._db.GetCurrentMedicationByPatientId(id);
                if (currentMedication != null)
                {
                    viewModel.CurrentMedication = currentMedication;
                }
                else
                {
                    CurrentMedication newCurrentMedication = new CurrentMedication();
                    newCurrentMedication.PatientId = id;
                    newCurrentMedication.EnteredBy = User.Identity.Name;
                    newCurrentMedication.EnteredDateTime = DateTime.Now;

                    this._db.InsertCurrentMedication(newCurrentMedication);
                }

                ViewBag.currentPatient = patient;
            }
            return View(viewModel);
        }

        private PatientFamilyHist InsertNewFamilyHistForPatientId(int patientId, PatientFamilyHist newFamilyHist)
        {
            newFamilyHist.PatientId = patientId;
            newFamilyHist.EnteredBy = User.Identity.Name;
            newFamilyHist.EnteredDateTime = DateTime.Now;
            List<PatientFamilyHistFeild> fields = this._db.GetAllPatientFamilyHistFeilds();
            foreach (PatientFamilyHistFeild f in fields)
            {
                switch (f.Id)
                {
                    case 1:
                        newFamilyHist.FH1 = f.DiseaseId;
                        break;
                    case 2:
                        newFamilyHist.FH2 = f.DiseaseId;
                        break;
                    case 3:
                        newFamilyHist.FH3 = f.DiseaseId;
                        break;
                    case 4:
                        newFamilyHist.FH4 = f.DiseaseId;
                        break;
                    case 5:
                        newFamilyHist.FH5 = f.DiseaseId;
                        break;
                    case 6:
                        newFamilyHist.FH6 = f.DiseaseId;
                        break;
                    case 7:
                        newFamilyHist.FH7 = f.DiseaseId;
                        break;
                    case 8:
                        newFamilyHist.FH8 = f.DiseaseId;
                        break;
                }

            }

            this._db.InsertPatientFamilyHist(newFamilyHist);
            return newFamilyHist;
        }

        [Authorize(Roles = "Admin,Manager,Physician,Nurse")]
        [HttpPost]
        public ActionResult UpdateMedHistory(Models.MedHistViewModel viewModel)
        {
            doUpdateMedHistory(viewModel);
            return RedirectToAction("MedHistory", "Patient", new { id = viewModel.PatientId});
        }

        private void doUpdateMedHistory(Models.MedHistViewModel viewModel)
        {
            PatientMedHist patientMedHist = this._db.GetPatientMedHistById(viewModel.MedHistId);
            PatientFamilyHist familyHist = this._db.GetPatientFamilyHistById(viewModel.PatientFamilyHistObj.Id);
            PatientVaccination vaccination = this._db.GetPatientVaccinationByPatientId(viewModel.PatientId);
           
            if (patientMedHist != null)
            {
                patientMedHist.A1YesNo = viewModel.A1YesNo;
                patientMedHist.A2YesNo = viewModel.A2YesNo;
                patientMedHist.A3YesNo = viewModel.A3YesNo;
                patientMedHist.A4YesNo = viewModel.A4YesNo;
                patientMedHist.A5YesNo = viewModel.A5YesNo;
                patientMedHist.A6YesNo = viewModel.A6YesNo;
                patientMedHist.A7YesNo = viewModel.A7YesNo;
                patientMedHist.A8YesNo = viewModel.A8YesNo;
                patientMedHist.A9YesNo = viewModel.A9YesNo;
                patientMedHist.A10YesNo = viewModel.A10YesNo;

                patientMedHist.A11YesNo = viewModel.A11YesNo;
                patientMedHist.A12YesNo = viewModel.A12YesNo;
                patientMedHist.A13YesNo = viewModel.A13YesNo;
                patientMedHist.A14YesNo = viewModel.A14YesNo;
                patientMedHist.A15YesNo = viewModel.A15YesNo;
                patientMedHist.A16YesNo = viewModel.A16YesNo;
                patientMedHist.A17YesNo = viewModel.A17YesNo;
                patientMedHist.A18YesNo = viewModel.A18YesNo;
                patientMedHist.A19YesNo = viewModel.A19YesNo;
                patientMedHist.A20YesNo = viewModel.A20YesNo;

                patientMedHist.A21YesNo = viewModel.A21YesNo;
                patientMedHist.A22YesNo = viewModel.A22YesNo;
                patientMedHist.A23YesNo = viewModel.A23YesNo;
                patientMedHist.A24YesNo = viewModel.A24YesNo;
                patientMedHist.A25YesNo = viewModel.A25YesNo;
                patientMedHist.A26YesNo = viewModel.A26YesNo;
                patientMedHist.A27YesNo = viewModel.A27YesNo;
                patientMedHist.A28YesNo = viewModel.A28YesNo;
                patientMedHist.A29YesNo = viewModel.A29YesNo;
                patientMedHist.A30YesNo = viewModel.A30YesNo;

                patientMedHist.A31YesNo = viewModel.A31YesNo;
                patientMedHist.A32YesNo = viewModel.A32YesNo;
                patientMedHist.A33YesNo = viewModel.A33YesNo;
                patientMedHist.A34YesNo = viewModel.A34YesNo;
                patientMedHist.A35YesNo = viewModel.A35YesNo;
                patientMedHist.A36YesNo = viewModel.A36YesNo;
                patientMedHist.A37YesNo = viewModel.A37YesNo;
                patientMedHist.A38YesNo = viewModel.A38YesNo;
                patientMedHist.A39YesNo = viewModel.A39YesNo;
                patientMedHist.A40YesNo = viewModel.A40YesNo;

                patientMedHist.A41YesNo = viewModel.A41YesNo;
                patientMedHist.A42YesNo = viewModel.A42YesNo;
                patientMedHist.A43YesNo = viewModel.A43YesNo;

                patientMedHist.A1Comment = viewModel.A1Comment;
                patientMedHist.A2Comment = viewModel.A2Comment;
                patientMedHist.A3Comment = viewModel.A3Comment;
                patientMedHist.A4Comment = viewModel.A4Comment;
                patientMedHist.A5Comment = viewModel.A5Comment;
                patientMedHist.A6Comment = viewModel.A6Comment;
                patientMedHist.A7Comment = viewModel.A7Comment;
                patientMedHist.A8Comment = viewModel.A8Comment;
                patientMedHist.A9Comment = viewModel.A9Comment;
                patientMedHist.A10Comment = viewModel.A10Comment;

                patientMedHist.A11Comment = viewModel.A11Comment;
                patientMedHist.A12Comment = viewModel.A12Comment;
                patientMedHist.A13Comment = viewModel.A13Comment;
                patientMedHist.A14Comment = viewModel.A14Comment;
                patientMedHist.A15Comment = viewModel.A15Comment;
                patientMedHist.A16Comment = viewModel.A16Comment;
                patientMedHist.A17Comment = viewModel.A17Comment;
                patientMedHist.A18Comment = viewModel.A18Comment;
                patientMedHist.A19Comment = viewModel.A19Comment;
                patientMedHist.A20Comment = viewModel.A20Comment;

                patientMedHist.A21Comment = viewModel.A21Comment;
                patientMedHist.A22Comment = viewModel.A22Comment;
                patientMedHist.A23Comment = viewModel.A23Comment;
                patientMedHist.A24Comment = viewModel.A24Comment;
                patientMedHist.A25Comment = viewModel.A25Comment;
                patientMedHist.A26Comment = viewModel.A26Comment;
                patientMedHist.A27Comment = viewModel.A27Comment;
                patientMedHist.A28Comment = viewModel.A28Comment;
                patientMedHist.A29Comment = viewModel.A29Comment;
                patientMedHist.A30Comment = viewModel.A30Comment;

                patientMedHist.A31Comment = viewModel.A31Comment;
                patientMedHist.A32Comment = viewModel.A32Comment;
                patientMedHist.A33Comment = viewModel.A33Comment;

                patientMedHist.A34Comment = viewModel.A34Comment;
                patientMedHist.A35Comment = viewModel.A35Comment;

                patientMedHist.A40Comment = viewModel.A40Comment;
                patientMedHist.A41Comment = viewModel.A41Comment;
                patientMedHist.A42Comment = viewModel.A42Comment;
                patientMedHist.A43Comment = viewModel.A43Comment;

                patientMedHist.UpdatedBy = User.Identity.Name;
                patientMedHist.UpdatedDateTime = DateTime.Now;

                this._db.UpdatePatientMedHist(patientMedHist);
            }

            if (familyHist != null)
            {
                familyHist.FH1YesNo = viewModel.PatientFamilyHistObj.FH1YesNo;
                familyHist.FH2YesNo = viewModel.PatientFamilyHistObj.FH2YesNo;
                familyHist.FH3YesNo = viewModel.PatientFamilyHistObj.FH3YesNo;
                familyHist.FH4YesNo = viewModel.PatientFamilyHistObj.FH4YesNo;
                familyHist.FH5YesNo = viewModel.PatientFamilyHistObj.FH5YesNo;
                familyHist.FH6YesNo = viewModel.PatientFamilyHistObj.FH6YesNo;
                familyHist.FH7YesNo = viewModel.PatientFamilyHistObj.FH7YesNo;
                familyHist.FH8YesNo = viewModel.PatientFamilyHistObj.FH8YesNo;

                familyHist.FH1Comment = viewModel.PatientFamilyHistObj.FH1Comment;
                familyHist.FH2Comment = viewModel.PatientFamilyHistObj.FH2Comment;
                familyHist.FH3Comment = viewModel.PatientFamilyHistObj.FH3Comment;
                familyHist.FH4Comment = viewModel.PatientFamilyHistObj.FH4Comment;
                familyHist.FH5Comment = viewModel.PatientFamilyHistObj.FH5Comment;
                familyHist.FH6Comment = viewModel.PatientFamilyHistObj.FH6Comment;
                familyHist.FH7Comment = viewModel.PatientFamilyHistObj.FH7Comment;
                familyHist.FH8Comment = viewModel.PatientFamilyHistObj.FH8Comment;

                familyHist.UpdatedBy = User.Identity.Name;
                familyHist.UpdatedDateTime = DateTime.Now;

                this._db.UpdatePatientFamilyHist(familyHist);
            }

            if(vaccination != null){
                vaccination.Hepatitis = viewModel.PatientVaccinationObj.Hepatitis;
                vaccination.OtherVaccination = viewModel.PatientVaccinationObj.OtherVaccination;
                vaccination.UpdatedBy = User.Identity.Name;
                vaccination.UpdatedDateTime = DateTime.Now;

                this._db.UpdatePatientVaccination(vaccination);
            }

            CurrentMedication currentMedication = this._db.GetCurrentMedicationByPatientId(viewModel.PatientId);
            if (currentMedication != null)
            {
                currentMedication.Medication1 = viewModel.CurrentMedication.Medication1;
                currentMedication.Medication2 = viewModel.CurrentMedication.Medication2;
                currentMedication.Medication3 = viewModel.CurrentMedication.Medication3;
                currentMedication.Medication4 = viewModel.CurrentMedication.Medication4;
                currentMedication.Medication5 = viewModel.CurrentMedication.Medication5;
                currentMedication.Medication6 = viewModel.CurrentMedication.Medication6;
                currentMedication.Medication7 = viewModel.CurrentMedication.Medication7;
                currentMedication.Medication8 = viewModel.CurrentMedication.Medication8;
                currentMedication.Medication9 = viewModel.CurrentMedication.Medication9;
                currentMedication.Medication10 = viewModel.CurrentMedication.Medication10;
                currentMedication.Medication11 = viewModel.CurrentMedication.Medication11;
                currentMedication.Medication12 = viewModel.CurrentMedication.Medication12;

                currentMedication.UpdatedBy = User.Identity.Name;
                currentMedication.UpdatedDateTime = DateTime.Now;

                this._db.UpdateCurrentMedication(currentMedication);
            }


        }

        private MedHistViewModel populateMedHistViewModel(MedHistViewModel viewModel, List<LegendDisease> diseaseList, PatientMedHist medHist)
        {
            viewModel.MedHistId = medHist.Id;
            viewModel.PatientId = medHist.PatientId;

            viewModel.Q1Description = diseaseList.ElementAt(medHist.Q1.Value - 1).DiseaseName;
            viewModel.Q1MyanmarDescription = diseaseList.ElementAt(medHist.Q1.Value - 1).DiseaseMyanmar;
            viewModel.A1YesNo = (medHist.A1YesNo.HasValue) ? medHist.A1YesNo.Value : false;
            viewModel.A1Comment = medHist.A1Comment;

            viewModel.Q2Description = diseaseList.ElementAt(medHist.Q2.Value - 1).DiseaseName;
            viewModel.Q2MyanmarDescription = diseaseList.ElementAt(medHist.Q2.Value - 1).DiseaseMyanmar;
            viewModel.Q3Description = diseaseList.ElementAt(medHist.Q3.Value - 1).DiseaseName;
            viewModel.Q3MyanmarDescription = diseaseList.ElementAt(medHist.Q3.Value - 1).DiseaseMyanmar;
            viewModel.Q4Description = diseaseList.ElementAt(medHist.Q4.Value - 1).DiseaseName;
            viewModel.Q4MyanmarDescription = diseaseList.ElementAt(medHist.Q4.Value - 1).DiseaseMyanmar;
            viewModel.Q5Description = diseaseList.ElementAt(medHist.Q5.Value - 1).DiseaseName;
            viewModel.Q5MyanmarDescription = diseaseList.ElementAt(medHist.Q5.Value - 1).DiseaseMyanmar;
            viewModel.Q6Description = diseaseList.ElementAt(medHist.Q6.Value - 1).DiseaseName;
            viewModel.Q6MyanmarDescription = diseaseList.ElementAt(medHist.Q6.Value - 1).DiseaseMyanmar;
            viewModel.Q7Description = diseaseList.ElementAt(medHist.Q7.Value - 1).DiseaseName;
            viewModel.Q7MyanmarDescription = diseaseList.ElementAt(medHist.Q7.Value - 1).DiseaseMyanmar;
            viewModel.Q8Description = diseaseList.ElementAt(medHist.Q8.Value - 1).DiseaseName;
            viewModel.Q8MyanmarDescription = diseaseList.ElementAt(medHist.Q8.Value - 1).DiseaseMyanmar;
            viewModel.Q9Description = diseaseList.ElementAt(medHist.Q9.Value - 1).DiseaseName;
            viewModel.Q9MyanmarDescription = diseaseList.ElementAt(medHist.Q9.Value - 1).DiseaseMyanmar;
            viewModel.Q10Description = diseaseList.ElementAt(medHist.Q10.Value - 1).DiseaseName;
            viewModel.Q10MyanmarDescription = diseaseList.ElementAt(medHist.Q10.Value - 1).DiseaseMyanmar;

            viewModel.Q11Description = diseaseList.ElementAt(medHist.Q11.Value - 1).DiseaseName;
            viewModel.Q11MyanmarDescription = diseaseList.ElementAt(medHist.Q11.Value - 1).DiseaseMyanmar;
            viewModel.Q12Description = diseaseList.ElementAt(medHist.Q12.Value - 1).DiseaseName;
            viewModel.Q12MyanmarDescription = diseaseList.ElementAt(medHist.Q12.Value - 1).DiseaseMyanmar;
            viewModel.Q13Description = diseaseList.ElementAt(medHist.Q13.Value - 1).DiseaseName;
            viewModel.Q13MyanmarDescription = diseaseList.ElementAt(medHist.Q13.Value - 1).DiseaseMyanmar;
            viewModel.Q14Description = diseaseList.ElementAt(medHist.Q14.Value - 1).DiseaseName;
            viewModel.Q14MyanmarDescription = diseaseList.ElementAt(medHist.Q14.Value - 1).DiseaseMyanmar;
            viewModel.Q15Description = diseaseList.ElementAt(medHist.Q15.Value - 1).DiseaseName;
            viewModel.Q15MyanmarDescription = diseaseList.ElementAt(medHist.Q15.Value - 1).DiseaseMyanmar;
            viewModel.Q16Description = diseaseList.ElementAt(medHist.Q16.Value - 1).DiseaseName;
            viewModel.Q16MyanmarDescription = diseaseList.ElementAt(medHist.Q16.Value - 1).DiseaseMyanmar;
            viewModel.Q17Description = diseaseList.ElementAt(medHist.Q17.Value - 1).DiseaseName;
            viewModel.Q17MyanmarDescription = diseaseList.ElementAt(medHist.Q17.Value - 1).DiseaseMyanmar;
            viewModel.Q18Description = diseaseList.ElementAt(medHist.Q18.Value - 1).DiseaseName;
            viewModel.Q18MyanmarDescription = diseaseList.ElementAt(medHist.Q18.Value - 1).DiseaseMyanmar;
            viewModel.Q19Description = diseaseList.ElementAt(medHist.Q19.Value - 1).DiseaseName;
            viewModel.Q19MyanmarDescription = diseaseList.ElementAt(medHist.Q19.Value - 1).DiseaseMyanmar;
            viewModel.Q20Description = diseaseList.ElementAt(medHist.Q20.Value - 1).DiseaseName;
            viewModel.Q20MyanmarDescription = diseaseList.ElementAt(medHist.Q20.Value - 1).DiseaseMyanmar;


            viewModel.Q21Description = diseaseList.ElementAt(medHist.Q21.Value - 1).DiseaseName;
            viewModel.Q21MyanmarDescription = diseaseList.ElementAt(medHist.Q21.Value - 1).DiseaseMyanmar;
            viewModel.Q22Description = diseaseList.ElementAt(medHist.Q22.Value - 1).DiseaseName;
            viewModel.Q22MyanmarDescription = diseaseList.ElementAt(medHist.Q22.Value - 1).DiseaseMyanmar;
            viewModel.Q23Description = diseaseList.ElementAt(medHist.Q23.Value - 1).DiseaseName;
            viewModel.Q23MyanmarDescription = diseaseList.ElementAt(medHist.Q23.Value - 1).DiseaseMyanmar;
            viewModel.Q24Description = diseaseList.ElementAt(medHist.Q24.Value - 1).DiseaseName;
            viewModel.Q24MyanmarDescription = diseaseList.ElementAt(medHist.Q24.Value - 1).DiseaseMyanmar;
            viewModel.Q25Description = diseaseList.ElementAt(medHist.Q25.Value - 1).DiseaseName;
            viewModel.Q25MyanmarDescription = diseaseList.ElementAt(medHist.Q25.Value - 1).DiseaseMyanmar;
            viewModel.Q26Description = diseaseList.ElementAt(medHist.Q26.Value - 1).DiseaseName;
            viewModel.Q26MyanmarDescription = diseaseList.ElementAt(medHist.Q26.Value - 1).DiseaseMyanmar;
            viewModel.Q27Description = diseaseList.ElementAt(medHist.Q27.Value - 1).DiseaseName;
            viewModel.Q27MyanmarDescription = diseaseList.ElementAt(medHist.Q27.Value - 1).DiseaseMyanmar;
            viewModel.Q28Description = diseaseList.ElementAt(medHist.Q28.Value - 1).DiseaseName;
            viewModel.Q28MyanmarDescription = diseaseList.ElementAt(medHist.Q28.Value - 1).DiseaseMyanmar;
            viewModel.Q29Description = diseaseList.ElementAt(medHist.Q29.Value - 1).DiseaseName;
            viewModel.Q29MyanmarDescription = diseaseList.ElementAt(medHist.Q29.Value - 1).DiseaseMyanmar;
            viewModel.Q30Description = diseaseList.ElementAt(medHist.Q30.Value - 1).DiseaseName;
            viewModel.Q30MyanmarDescription = diseaseList.ElementAt(medHist.Q30.Value - 1).DiseaseMyanmar;

            viewModel.Q31Description = diseaseList.ElementAt(medHist.Q31.Value - 1).DiseaseName;
            viewModel.Q31MyanmarDescription = diseaseList.ElementAt(medHist.Q31.Value - 1).DiseaseMyanmar;
            viewModel.Q32Description = diseaseList.ElementAt(medHist.Q32.Value - 1).DiseaseName;
            viewModel.Q32MyanmarDescription = diseaseList.ElementAt(medHist.Q32.Value - 1).DiseaseMyanmar;
            viewModel.Q33Description = diseaseList.ElementAt(medHist.Q33.Value - 1).DiseaseName;
            viewModel.Q33MyanmarDescription = diseaseList.ElementAt(medHist.Q33.Value - 1).DiseaseMyanmar;
            viewModel.Q34Description = diseaseList.ElementAt(medHist.Q34.Value - 1).DiseaseName;
            viewModel.Q34MyanmarDescription = diseaseList.ElementAt(medHist.Q34.Value - 1).DiseaseMyanmar;
            viewModel.Q35Description = diseaseList.ElementAt(medHist.Q35.Value - 1).DiseaseName;
            viewModel.Q35MyanmarDescription = diseaseList.ElementAt(medHist.Q35.Value - 1).DiseaseMyanmar;
            viewModel.Q36Description = diseaseList.ElementAt(medHist.Q36.Value - 1).DiseaseName;
            viewModel.Q36MyanmarDescription = diseaseList.ElementAt(medHist.Q36.Value - 1).DiseaseMyanmar;
            viewModel.Q37Description = diseaseList.ElementAt(medHist.Q37.Value - 1).DiseaseName;
            viewModel.Q37MyanmarDescription = diseaseList.ElementAt(medHist.Q37.Value - 1).DiseaseMyanmar;
            viewModel.Q38Description = diseaseList.ElementAt(medHist.Q38.Value - 1).DiseaseName;
            viewModel.Q38MyanmarDescription = diseaseList.ElementAt(medHist.Q38.Value - 1).DiseaseMyanmar;
            viewModel.Q39Description = diseaseList.ElementAt(medHist.Q39.Value - 1).DiseaseName;
            viewModel.Q39MyanmarDescription = diseaseList.ElementAt(medHist.Q39.Value - 1).DiseaseMyanmar;
            viewModel.Q40Description = diseaseList.ElementAt(medHist.Q40.Value - 1).DiseaseName;
            viewModel.Q40MyanmarDescription = diseaseList.ElementAt(medHist.Q40.Value - 1).DiseaseMyanmar;

            viewModel.Q41Description = diseaseList.ElementAt(medHist.Q41.Value - 1).DiseaseName;
            viewModel.Q41MyanmarDescription = diseaseList.ElementAt(medHist.Q41.Value - 1).DiseaseMyanmar;
            viewModel.Q42Description = diseaseList.ElementAt(medHist.Q42.Value - 1).DiseaseName;
            viewModel.Q42MyanmarDescription = diseaseList.ElementAt(medHist.Q42.Value - 1).DiseaseMyanmar;
            viewModel.Q43Description = diseaseList.ElementAt(medHist.Q43.Value - 1).DiseaseName;
            viewModel.Q43MyanmarDescription = diseaseList.ElementAt(medHist.Q43.Value - 1).DiseaseMyanmar;

            viewModel.A1YesNo = (medHist.A1YesNo.HasValue) ? medHist.A1YesNo.Value : false;
            viewModel.A2YesNo = (medHist.A2YesNo.HasValue) ? medHist.A2YesNo.Value : false;
            viewModel.A3YesNo = (medHist.A3YesNo.HasValue) ? medHist.A3YesNo.Value : false;
            viewModel.A4YesNo = (medHist.A4YesNo.HasValue) ? medHist.A4YesNo.Value : false;
            viewModel.A5YesNo = (medHist.A5YesNo.HasValue) ? medHist.A5YesNo.Value : false;
            viewModel.A6YesNo = (medHist.A6YesNo.HasValue) ? medHist.A6YesNo.Value : false;
            viewModel.A7YesNo = (medHist.A7YesNo.HasValue) ? medHist.A7YesNo.Value : false;
            viewModel.A8YesNo = (medHist.A8YesNo.HasValue) ? medHist.A8YesNo.Value : false;
            viewModel.A9YesNo = (medHist.A9YesNo.HasValue) ? medHist.A9YesNo.Value : false;
            viewModel.A10YesNo = (medHist.A10YesNo.HasValue) ? medHist.A10YesNo.Value : false;

            viewModel.A11YesNo = (medHist.A11YesNo.HasValue) ? medHist.A11YesNo.Value : false;
            viewModel.A12YesNo = (medHist.A12YesNo.HasValue) ? medHist.A12YesNo.Value : false;
            viewModel.A13YesNo = (medHist.A13YesNo.HasValue) ? medHist.A13YesNo.Value : false;
            viewModel.A14YesNo = (medHist.A14YesNo.HasValue) ? medHist.A14YesNo.Value : false;
            viewModel.A15YesNo = (medHist.A15YesNo.HasValue) ? medHist.A15YesNo.Value : false;
            viewModel.A16YesNo = (medHist.A16YesNo.HasValue) ? medHist.A16YesNo.Value : false;
            viewModel.A17YesNo = (medHist.A17YesNo.HasValue) ? medHist.A17YesNo.Value : false;
            viewModel.A18YesNo = (medHist.A18YesNo.HasValue) ? medHist.A18YesNo.Value : false;
            viewModel.A19YesNo = (medHist.A19YesNo.HasValue) ? medHist.A19YesNo.Value : false;
            viewModel.A20YesNo = (medHist.A20YesNo.HasValue) ? medHist.A20YesNo.Value : false;

            viewModel.A21YesNo = (medHist.A21YesNo.HasValue) ? medHist.A21YesNo.Value : false;
            viewModel.A22YesNo = (medHist.A22YesNo.HasValue) ? medHist.A22YesNo.Value : false;
            viewModel.A23YesNo = (medHist.A23YesNo.HasValue) ? medHist.A23YesNo.Value : false;
            viewModel.A24YesNo = (medHist.A24YesNo.HasValue) ? medHist.A24YesNo.Value : false;
            viewModel.A25YesNo = (medHist.A25YesNo.HasValue) ? medHist.A25YesNo.Value : false;
            viewModel.A26YesNo = (medHist.A26YesNo.HasValue) ? medHist.A26YesNo.Value : false;
            viewModel.A27YesNo = (medHist.A27YesNo.HasValue) ? medHist.A27YesNo.Value : false;
            viewModel.A28YesNo = (medHist.A28YesNo.HasValue) ? medHist.A28YesNo.Value : false;
            viewModel.A29YesNo = (medHist.A29YesNo.HasValue) ? medHist.A29YesNo.Value : false;
            viewModel.A30YesNo = (medHist.A30YesNo.HasValue) ? medHist.A30YesNo.Value : false;

            viewModel.A31YesNo = (medHist.A31YesNo.HasValue) ? medHist.A31YesNo.Value : false;
            viewModel.A32YesNo = (medHist.A32YesNo.HasValue) ? medHist.A32YesNo.Value : false;
            viewModel.A33YesNo = (medHist.A33YesNo.HasValue) ? medHist.A33YesNo.Value : false;
            viewModel.A34YesNo = (medHist.A34YesNo.HasValue) ? medHist.A34YesNo.Value : false;
            viewModel.A35YesNo = (medHist.A35YesNo.HasValue) ? medHist.A35YesNo.Value : false;
            viewModel.A36YesNo = (medHist.A36YesNo.HasValue) ? medHist.A36YesNo.Value : false;
            viewModel.A37YesNo = (medHist.A37YesNo.HasValue) ? medHist.A37YesNo.Value : false;
            viewModel.A38YesNo = (medHist.A38YesNo.HasValue) ? medHist.A38YesNo.Value : false;
            viewModel.A39YesNo = (medHist.A39YesNo.HasValue) ? medHist.A39YesNo.Value : false;
            viewModel.A40YesNo = (medHist.A40YesNo.HasValue) ? medHist.A40YesNo.Value : false;

            viewModel.A41YesNo = (medHist.A41YesNo.HasValue) ? medHist.A41YesNo.Value : false;
            viewModel.A42YesNo = (medHist.A41YesNo.HasValue) ? medHist.A42YesNo.Value : false;
            viewModel.A43YesNo = (medHist.A41YesNo.HasValue) ? medHist.A43YesNo.Value : false;

            viewModel.A1Comment = medHist.A1Comment;
            viewModel.A2Comment = medHist.A2Comment;
            viewModel.A3Comment = medHist.A3Comment;
            viewModel.A4Comment = medHist.A4Comment;
            viewModel.A5Comment = medHist.A5Comment;
            viewModel.A6Comment = medHist.A6Comment;
            viewModel.A7Comment = medHist.A7Comment;
            viewModel.A8Comment = medHist.A8Comment;
            viewModel.A9Comment = medHist.A9Comment;
            viewModel.A10Comment = medHist.A10Comment;

            viewModel.A11Comment = medHist.A11Comment;
            viewModel.A12Comment = medHist.A12Comment;
            viewModel.A13Comment = medHist.A13Comment;
            viewModel.A14Comment = medHist.A14Comment;
            viewModel.A15Comment = medHist.A15Comment;
            viewModel.A16Comment = medHist.A16Comment;
            viewModel.A17Comment = medHist.A17Comment;
            viewModel.A18Comment = medHist.A18Comment;
            viewModel.A19Comment = medHist.A19Comment;
            viewModel.A20Comment = medHist.A20Comment;

            viewModel.A21Comment = medHist.A21Comment;
            viewModel.A22Comment = medHist.A22Comment;
            viewModel.A23Comment = medHist.A23Comment;
            viewModel.A24Comment = medHist.A24Comment;
            viewModel.A25Comment = medHist.A25Comment;
            viewModel.A26Comment = medHist.A26Comment;
            viewModel.A27Comment = medHist.A27Comment;
            viewModel.A28Comment = medHist.A28Comment;
            viewModel.A29Comment = medHist.A29Comment;
            viewModel.A30Comment = medHist.A30Comment;

            viewModel.A31Comment = medHist.A31Comment;
            viewModel.A32Comment = medHist.A32Comment;
            viewModel.A33Comment = medHist.A33Comment;

            viewModel.A34Comment = medHist.A34Comment;
            viewModel.A35Comment = medHist.A35Comment;

            viewModel.A40Comment = medHist.A40Comment;
            viewModel.A41Comment = medHist.A41Comment;
            viewModel.A42Comment = medHist.A42Comment;
            viewModel.A43Comment = medHist.A43Comment;

            return viewModel;
        }

        public ActionResult SearchPatient()
        {
            //ViewBag.currentLoginUsername = @User.Identity.Name;
            //List<Patient> result = 
            return View();
        }

        public ActionResult ManagePatients()
        {
            return View();
        }

        public ActionResult Appointment(ICollection<int> ids)
        {
            if (ids != null && ids.Count > 0)
            {
                ViewBag.currentPatientId = ids.ElementAt(0);
                ViewBag.selectedPhysicianId = ids.ElementAt(1);
            }
            return View();
        }

        public ActionResult MakeAppointmentForPatientId(int id)
        {
            Patient patient = this._db.GetPatientById(id);
            ViewBag.currentPatientId = id;
            ViewBag.patientName = patient.Name;
            return View();
        }

        public ActionResult MakeAppointment(ICollection<int> ids)
        {
            if (ids != null && ids.Count > 0)
            {
                int patientId = ids.ElementAt(0);
                int physicianId = ids.ElementAt(1);
                Patient patient = this._db.GetPatientById(patientId);
                ViewBag.currentPatientId = patientId;
                if (patient != null)
                {
                    ViewBag.currentPatientName = patient.Name;
                }

                ViewBag.selectedPhysicianId = physicianId;
                PhysicianTitle physicianTitle = this._db.GetPhysicianById(physicianId);
                if (physicianTitle != null)
                {
                    string physicianName = physicianTitle.Prefix + " " + physicianTitle.Name;
                    ViewBag.currentPhysicianName = physicianName;
                }
            }
            return View();
        }

    }
}
