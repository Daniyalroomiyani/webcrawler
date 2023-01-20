using System;
using System.IO;
using System.Text;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using web;
using System.Threading;
using System.Threading.Tasks;
namespace System
{

    class Program
    {
        public static String MainList = "";

        public static String FinalResulList = "";

        public static string VerbFarsi = "همایون";
        public static string VerbEnglish = "homayoun ";



        public static string VerbFarsi2 = "شجریان";
        public static string VerbEnglish2 = "shajarian";


        public static int maxSiteWisit = 1000;
        static async Task Main(string[] args)
        {




            string fileFinal = Environment.CurrentDirectory + @"\ResultFinal.csv";
            if (File.Exists(fileFinal))
            {
                File.Delete(fileFinal);
            }
            String[] headingsF = { "Name", "Link", "Rate" };
            String separatorF = ",";
            StringBuilder outputF = new StringBuilder();
            outputF.AppendLine(string.Join(separatorF, headingsF));
            try
            {
                File.AppendAllText(fileFinal, outputF.ToString());
                Console.WriteLine("File  Final Init...");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Data could not be written to the CSV file.");
                return;
            }



            string file = Environment.CurrentDirectory + @"\Result.csv";
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            String[] headings = { "Name", "Link", "VerbFarsi", "Hit_VerbFarsi", "VerbEnglish", "Hit_VerbEnglish", "VerbFarsi2", "Hit_VerbFarsi2", "VerbEnglish2", "Hit_VerbEnglish2", "OutLinks" };
            String separator = ",";
            StringBuilder output = new StringBuilder();
            output.AppendLine(string.Join(separator, headings));
            try
            {
                File.AppendAllText(file, output.ToString());
                Console.WriteLine("File Init...");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Data could not be written to the CSV file.");
                return;
            }




            var html = @"https://www.asriran.com/fa/links";
            //  var html = @"http://www.iranhmusic.ir/";


            MainList += html + '-';




            while (CharCouner(MainList, '-') < maxSiteWisit)
            {
                Crawler cr = new Crawler(html);
                cr.start();
            }







            Console.WriteLine("All IS Done .....\n ");


        }


        public static int CharCouner(string str, char ch)
        {
            return str.Count(x => x == '$');
        }





        public static void WriteFinalExel(String name , string link , int rate)
        {



            //Here we Can Deside about the Rate 




             string file = Environment.CurrentDirectory + @"\ResultFinal.csv";
            // String[] headings = { "Name", "Link", "Verb", "Hits" , "OutLonks" };
            String separator = ",";
            StringBuilder output = new StringBuilder();
            // output.AppendLine(string.Join(separator, headings));



            String[] newLine = {name , link , rate.ToString() };
            output.AppendLine(string.Join(separator, newLine));

            try
            {
                File.AppendAllText(file, output.ToString(), Encoding.Default);
                Console.WriteLine(link);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Data could not be written to the CSV file.");
                return;
            }
            // Console.WriteLine(file);


        }
    }

}