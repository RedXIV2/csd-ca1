using System;
using Xunit;
using BPCalculator;

namespace BPCalculatorTests
{
    public class BPCategoryTests
    {
        [Fact]
        public void LowBloodPressureReturned()
        {
            BloodPressure bp = new BloodPressure();

            bp.Systolic = 82;
            bp.Diastolic = 55;

            string category = bp.Category.ToString();

            Xunit.Assert.Matches("Low", category);                 
        }

        [Fact]
        public void NormalBloodPressureReturned()
        {
            BloodPressure bp = new BloodPressure();

            bp.Systolic = 100;
            bp.Diastolic = 55;

            string category = bp.Category.ToString();

            Xunit.Assert.Matches("Normal", category);
        }

        [Fact]
        public void PreHighBloodPressureReturned()
        {
            BloodPressure bp = new BloodPressure();

            bp.Systolic = 100;
            bp.Diastolic = 85;

            string category = bp.Category.ToString();

            Xunit.Assert.Matches("PreHigh", category);
        }

        [Fact]
        public void HighBloodPressureReturned()
        {
            BloodPressure bp = new BloodPressure();

            bp.Systolic = 100;
            bp.Diastolic = 805;

            string category = bp.Category.ToString();

            Xunit.Assert.Matches("High", category);
        }

    }
}
