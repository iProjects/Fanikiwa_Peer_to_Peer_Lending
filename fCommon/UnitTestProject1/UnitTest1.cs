using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using fCommon.Utility;
using fanikiwaGL.Framework;
using System.Configuration;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //  STOCommissionFeesPaidFlag t = Config.GetEnumValue<STOCommissionFeesPaidFlag>("LOANREPAYMENTFEESFLAG");
            //System.Diagnostics.Debug.WriteLine(" the value of ["+t+"] is ["+(short)t+"]");
        }

        [Flags]
        enum Colors { None = 0, Red = 1, Green = 2, Blue = 4 };

        [TestMethod]
        public void Test2()
        {
            string[] colorStrings = { "0", "2", "8", "blue", "Blue", "Yellow", "Red, Green", "CommAndLoanPaid" };
            try
            {
                foreach (string colorString in colorStrings)
                {

                    //Colors colorValue = (Colors)Enum.Parse(typeof(Colors), colorString);
                    //    STOCommissionFeesPaidFlag colorValue = (STOCommissionFeesPaidFlag)
                    //Enum.Parse(typeof(STOCommissionFeesPaidFlag), colorString);
                    //    if (Enum.IsDefined(typeof(STOCommissionFeesPaidFlag), colorValue) | colorValue.ToString().Contains(","))
                    //        Console.WriteLine("Converted '{0}' to {1}.", colorString, colorValue.ToString());
                    //    else
                    //        Console.WriteLine("{0} is not an underlying value of the STOCommissionFeesPaidFlag enumeration.", colorString);

                }

                throw new ArgumentNullException();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                Utils.ShowError(ex);
                Console.WriteLine(ex.ToString());
            }
        }






    }

}
