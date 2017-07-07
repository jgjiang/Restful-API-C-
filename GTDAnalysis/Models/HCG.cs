using System;
namespace GTDAnalysis.Models
{
    public class HCG
    {
        public int Id { get; set; }
        public string Mrn { get; set; }
        public int WeekNum { get; set; }
        public int HCGValue { get; set; }
        public int PatientId { get; set; }

        public HCG(int Id,string Mrn,int WeekNum, int HCGValue, int PatientId){
            this.Id = Id;
            this.Mrn = Mrn;
            this.WeekNum = WeekNum;
            this.HCGValue = HCGValue;
            this.PatientId = PatientId;
        }


    }
}
