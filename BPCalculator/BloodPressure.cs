using System;
using System.ComponentModel.DataAnnotations;

namespace BPCalculator
{
    // BP categories
    public enum BPCategory
    {
        [Display(Name="Low Blood Pressure")] Low,
        [Display(Name="Normal Blood Pressure")]  Normal,
        [Display(Name="Pre-High Blood Pressure")] PreHigh,
        [Display(Name ="High Blood Pressure")]  High
    };

    public class BloodPressure
    {
        public const int SystolicMin = 70;
        public const int SystolicMax = 190;
        public const int DiastolicMin = 40;
        public const int DiastolicMax = 100;

        [Range(SystolicMin, SystolicMax, ErrorMessage = "Invalid Systolic Value")]
        public int Systolic { get; set; }                       // mmHG

        [Range(DiastolicMin, DiastolicMax, ErrorMessage = "Invalid Diastolic Value")]
        public int Diastolic { get; set; }                      // mmHG

        // calculate BP category
        public BPCategory Category
        {
            get
            {
                // implement as part of project
                //throw new NotImplementedException("not implemented yet");
                if (this.Systolic < 90 && this.Diastolic < 60)
                {
                    return BPCategory.Low;
                }
                else if (this.Systolic < 120 && this.Diastolic < 80)
                {
                    return BPCategory.Normal;
                }
                else if (this.Systolic < 140 && this.Diastolic < 90)
                {
                    return BPCategory.PreHigh;
                }
                else
                {
                    return BPCategory.High;
                }


            }
        }
    }
}
