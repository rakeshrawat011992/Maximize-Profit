using System;
using System.IO;

namespace Trading
{

    class TradingShares
    {
        //main class
        public static void Main()
        {
            //console app entry main method

            //Method for getting array of shares either from file or a direct array string
            GetSharesDatacls _GetSharesDatacls = new GetSharesDatacls();
            string sharesData = _GetSharesDatacls.GetSharesData();

            try
            {
                double[] sharesValArr = Array.ConvertAll(sharesData.Split(','), double.Parse);
                int numberOfDays = sharesValArr.Length;

                //Function calling 
                GetSharesGain _GetSharesGain = new GetSharesGain();
                double maxGain = _GetSharesGain.MaximumGain(sharesValArr, numberOfDays);
                if (maxGain == -1)
                    Console.WriteLine("Can't have profit in shares buying and selling they are in decreasing order");
                else
                    Console.WriteLine("\n Also Maximum Profit in shares is " + maxGain);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.Write("Wrong type of array for entered " + ex.Message);
                Environment.Exit(0);
            }
        }
    }
    class GetSharesDatacls
    {
        public string GetSharesData()
        {
            Console.WriteLine("This program takes monthly shares opening history data of Computershare market opening stock prices \n" +
                "press 1 to upload text file path of monthly data or\n" +
                "press 2 to enter comma separated array string in this format 1,2,3 \n" +
                "else press any key to exit");
            string keyVal = Console.ReadLine();
            string arrayText = "";
            if (keyVal == "1")
            {
                getFileData _getFileData = new getFileData();
                arrayText = _getFileData.getFileText();
                return arrayText;
            }
            else if (keyVal == "2")
            {
                Console.WriteLine("Enter the array string");
                arrayText = Console.ReadLine();
                return arrayText;
            }
            else
                Environment.Exit(0);
            return arrayText;
        }

    }

    class GetSharesGain
    {
        public double MaximumGain(double[] sharesValArr, int numberOfDays)
        {
            // Initialize Result 
            double maxGain = sharesValArr[1] - sharesValArr[0];
            double buyingDayVal = sharesValArr[0];
            double sellingDayVal = sharesValArr[1];
            int buyingDay = 1;
            int sellingDay = 2;
            int i, j;
            try
            {
                for (i = 1; i < numberOfDays; i++)
                {
                    for (j = i + 1; j < numberOfDays; j++)
                    {
                        if (sharesValArr[j] - sharesValArr[i] > maxGain)
                        {
                            maxGain = sharesValArr[j] - sharesValArr[i];
                            sellingDayVal = sharesValArr[j];
                            sellingDay = j + 1;
                            buyingDayVal = sharesValArr[i];
                            buyingDay = i + 1;
                        }
                    }
                }
                Console.WriteLine(buyingDay + "(" + buyingDayVal + ")" + "," + sellingDay + "(" + sellingDayVal + ")");
                return Math.Round(maxGain, 2);
            }
            catch (Exception ex)
            {
                Console.Write("You entered an invalid Array" + ex.Message);
                Environment.Exit(0);
            }
            return -1;
        }
    }

    public interface IreadFile
    {
        string getFileText();
    }
    class getFileData : IreadFile
    {
        public string getFileText()
        {
            string fileText = "";
            try
            {
                Console.WriteLine("Enter the file path");
                string filePath = Console.ReadLine();
                fileText = File.ReadAllText(filePath);
                return fileText;
            }
            catch (Exception ex)
            {
                Console.Write("You entered an invalid file path." + ex.Message);
                Environment.Exit(0);
            }
            return fileText;
        }

    }
}
