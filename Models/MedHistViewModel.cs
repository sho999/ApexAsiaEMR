using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ApexAsiaDAL;

namespace ApexAsiaEMR.Models
{
    public class MedHistViewModel
    {
        public int MedHistId { get; set; }
        public int PatientId { get; set; }

        public PatientFamilyHist PatientFamilyHistObj { get; set; }

        public PatientVaccination PatientVaccinationObj { get; set; }

        public CurrentMedication CurrentMedication { get; set; }

        public DateTime EnteredDateTime { get; set; }
        public string EnteredBy { get; set; }
	    
        // Question 1 to 43
        public int Q1 { get; set; }
        public string Q1Description { get; set; }
        public bool A1YesNo { get; set; }
        public string A1Comment { get; set; }

        public int Q2 { get; set; }
        public string Q2Description { get; set; }
        public bool A2YesNo { get; set; }
        public string A2Comment { get; set; }

        public int Q3 { get; set; }
        public string Q3Description { get; set; }
        public bool A3YesNo { get; set; }
        public string A3Comment { get; set; }

        public int Q4 { get; set; }
        public string Q4Description { get; set; }
        public bool A4YesNo { get; set; }
        public string A4Comment { get; set; }

        public int Q5 { get; set; }
        public string Q5Description { get; set; }
        public bool A5YesNo { get; set; }
        public string A5Comment { get; set; }

        public int Q6 { get; set; }
        public string Q6Description { get; set; }
        public bool A6YesNo { get; set; }
        public string A6Comment { get; set; }

        public int Q7 { get; set; }
        public string Q7Description { get; set; }
        public bool A7YesNo { get; set; }
        public string A7Comment { get; set; }

        public int Q8 { get; set; }
        public string Q8Description { get; set; }
        public bool A8YesNo { get; set; }
        public string A8Comment { get; set; }

        public int Q9 { get; set; }
        public string Q9Description { get; set; }
        public bool A9YesNo { get; set; }
        public string A9Comment { get; set; }

        public int Q10 { get; set; }
        public string Q10Description { get; set; }
        public bool A10YesNo { get; set; }
        public string A10Comment { get; set; }

        public int Q11 { get; set; }
        public string Q11Description { get; set; }
        public bool A11YesNo { get; set; }
        public string A11Comment { get; set; }

        public int Q12 { get; set; }
        public string Q12Description { get; set; }
        public bool A12YesNo { get; set; }
        public string A12Comment { get; set; }

        public int Q13 { get; set; }
        public string Q13Description { get; set; }
        public bool A13YesNo { get; set; }
        public string A13Comment { get; set; }

        public int Q14 { get; set; }
        public string Q14Description { get; set; }
        public bool A14YesNo { get; set; }
        public string A14Comment { get; set; }

        public int Q15 { get; set; }
        public string Q15Description { get; set; }
        public bool A15YesNo { get; set; }
        public string A15Comment { get; set; }

        public int Q16 { get; set; }
        public string Q16Description { get; set; }
        public bool A16YesNo { get; set; }
        public string A16Comment { get; set; }

        public int Q17 { get; set; }
        public string Q17Description { get; set; }
        public bool A17YesNo { get; set; }
        public string A17Comment { get; set; }

        public int Q18 { get; set; }
        public string Q18Description { get; set; }
        public bool A18YesNo { get; set; }
        public string A18Comment { get; set; }

        public int Q19 { get; set; }
        public string Q19Description { get; set; }
        public bool A19YesNo { get; set; }
        public string A19Comment { get; set; }

        public int Q20 { get; set; }
        public string Q20Description { get; set; }
        public bool A20YesNo { get; set; }
        public string A20Comment { get; set; }


        public int Q21 { get; set; }
        public string Q21Description { get; set; }
        public bool A21YesNo { get; set; }
        public string A21Comment { get; set; }

        public int Q22 { get; set; }
        public string Q22Description { get; set; }
        public bool A22YesNo { get; set; }
        public string A22Comment { get; set; }

        public int Q23 { get; set; }
        public string Q23Description { get; set; }
        public bool A23YesNo { get; set; }
        public string A23Comment { get; set; }

        public int Q24 { get; set; }
        public string Q24Description { get; set; }
        public bool A24YesNo { get; set; }
        public string A24Comment { get; set; }

        public int Q25 { get; set; }
        public string Q25Description { get; set; }
        public bool A25YesNo { get; set; }
        public string A25Comment { get; set; }

        public int Q26 { get; set; }
        public string Q26Description { get; set; }
        public bool A26YesNo { get; set; }
        public string A26Comment { get; set; }

        public int Q27 { get; set; }
        public string Q27Description { get; set; }
        public bool A27YesNo { get; set; }
        public string A27Comment { get; set; }

        public int Q28 { get; set; }
        public string Q28Description { get; set; }
        public bool A28YesNo { get; set; }
        public string A28Comment { get; set; }

        public int Q29 { get; set; }
        public string Q29Description { get; set; }
        public bool A29YesNo { get; set; }
        public string A29Comment { get; set; }

        public int Q30 { get; set; }
        public string Q30Description { get; set; }
        public bool A30YesNo { get; set; }
        public string A30Comment { get; set; }

        public int Q31 { get; set; }
        public string Q31Description { get; set; }
        public bool A31YesNo { get; set; }
        public string A31Comment { get; set; }

        public int Q32 { get; set; }
        public string Q32Description { get; set; }
        public bool A32YesNo { get; set; }
        public string A32Comment { get; set; }

        public int Q33 { get; set; }
        public string Q33Description { get; set; }
        public bool A33YesNo { get; set; }
        public string A33Comment { get; set; }

        public int Q34 { get; set; }
        public string Q34Description { get; set; }
        public bool A34YesNo { get; set; }
        public string A34Comment { get; set; }

        public int Q35 { get; set; }
        public string Q35Description { get; set; }
        public bool A35YesNo { get; set; }
        public string A35Comment { get; set; }

        public int Q36 { get; set; }
        public string Q36Description { get; set; }
        public bool A36YesNo { get; set; }
        public string A36Comment { get; set; }

        public int Q37 { get; set; }
        public string Q37Description { get; set; }
        public bool A37YesNo { get; set; }
        public string A37Comment { get; set; }

        public int Q38 { get; set; }
        public string Q38Description { get; set; }
        public bool A38YesNo { get; set; }
        public string A38Comment { get; set; }

        public int Q39 { get; set; }
        public string Q39Description { get; set; }
        public bool A39YesNo { get; set; }
        public string A39Comment { get; set; }

        public int Q40 { get; set; }
        public string Q40Description { get; set; }
        public bool A40YesNo { get; set; }
        public string A40Comment { get; set; }

        public int Q41 { get; set; }
        public string Q41Description { get; set; }
        public bool A41YesNo { get; set; }
        public string A41Comment { get; set; }

        public int Q42 { get; set; }
        public string Q42Description { get; set; }
        public bool A42YesNo { get; set; }
        public string A42Comment { get; set; }

        public int Q43 { get; set; }
        public string Q43Description { get; set; }
        public bool A43YesNo { get; set; }
        public string A43Comment { get; set; }


        public string Q1MyanmarDescription { get; set; }
        public string Q2MyanmarDescription { get; set; }
        public string Q3MyanmarDescription { get; set; }
        public string Q4MyanmarDescription { get; set; }
        public string Q5MyanmarDescription { get; set; }
        public string Q6MyanmarDescription { get; set; }
        public string Q7MyanmarDescription { get; set; }
        public string Q8MyanmarDescription { get; set; }
        public string Q9MyanmarDescription { get; set; }
        public string Q10MyanmarDescription { get; set; }

        public string Q11MyanmarDescription { get; set; }
        public string Q12MyanmarDescription { get; set; }
        public string Q13MyanmarDescription { get; set; }
        public string Q14MyanmarDescription { get; set; }
        public string Q15MyanmarDescription { get; set; }
        public string Q16MyanmarDescription { get; set; }
        public string Q17MyanmarDescription { get; set; }
        public string Q18MyanmarDescription { get; set; }
        public string Q19MyanmarDescription { get; set; }
        public string Q20MyanmarDescription { get; set; }

        public string Q21MyanmarDescription { get; set; }
        public string Q22MyanmarDescription { get; set; }
        public string Q23MyanmarDescription { get; set; }
        public string Q24MyanmarDescription { get; set; }
        public string Q25MyanmarDescription { get; set; }
        public string Q26MyanmarDescription { get; set; }
        public string Q27MyanmarDescription { get; set; }
        public string Q28MyanmarDescription { get; set; }
        public string Q29MyanmarDescription { get; set; }
        public string Q30MyanmarDescription { get; set; }

        public string Q31MyanmarDescription { get; set; }
        public string Q32MyanmarDescription { get; set; }
        public string Q33MyanmarDescription { get; set; }
        public string Q34MyanmarDescription { get; set; }
        public string Q35MyanmarDescription { get; set; }
        public string Q36MyanmarDescription { get; set; }
        public string Q37MyanmarDescription { get; set; }
        public string Q38MyanmarDescription { get; set; }
        public string Q39MyanmarDescription { get; set; }
        public string Q40MyanmarDescription { get; set; }

        public string Q41MyanmarDescription { get; set; }
        public string Q42MyanmarDescription { get; set; }
        public string Q43MyanmarDescription { get; set; }



    }
}