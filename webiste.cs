
using System.IO;
using System.Text;
using System;


namespace web
{

    public class WebSite
    {
        public string name;
        public string link;

        public string[] outLinks;

        public int hitFarsi;
        public int hitEnglish;
        public int hitFarsi2;
        public int hitEnglish2;



        // public string verb;
        public string[] finaloutLinks;

        public WebSite(string name, string link, string[] outLinks, int hitsf, int hitsE , int hitsf2, int hitsE2)
        {
            this.name = name;
            this.link = link;
            this.outLinks = outLinks;
            this.hitFarsi = hitsf;
            this.hitEnglish=hitsE;
            this.hitFarsi2=hitsf2;
            this.hitEnglish2= hitsE2;
        }


        public void writeExel()
        {

            string file = Environment.CurrentDirectory + @"\Result.csv";
            // String[] headings = { "Name", "Link", "Verb", "Hits" , "OutLonks" };
            String separator = ",";
            StringBuilder output = new StringBuilder();
            // output.AppendLine(string.Join(separator, headings));



            String[] newLine = { this.name, this.link, Program.VerbFarsi, this.hitFarsi.ToString() , Program.VerbEnglish ,this.hitEnglish.ToString() , Program.VerbFarsi2 , this.hitFarsi2.ToString() , Program.VerbEnglish2 , this.hitEnglish2.ToString(), ListToString(this.outLinks, this) };
            output.AppendLine(string.Join(separator, newLine));

            try
            {
                File.AppendAllText(file, output.ToString(), Encoding.Default);
                Console.WriteLine(file);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Data could not be written to the CSV file.");
                return;
            }
            Console.WriteLine(file);

        }
        private static string ListToString(string[] items, WebSite mainsite)
        {

            string result = "";

            if (items.Count() == 0)
            {
                return "";
            }
            var mainLink = mainsite.link.Substring(12);
            if (mainLink.Contains('/'))
            {
                var test = mainLink.Substring(0, mainLink.IndexOf('/'));
                mainLink = test;
            }

            string file = Environment.CurrentDirectory + @"\" + mainLink + ".csv";
             String[] headings = { "Link", "From" };
            String separator = ",";
            StringBuilder output = new StringBuilder();
            if (!File.Exists(file))
            {
                
           
            output.AppendLine(string.Join(separator, headings));
             try
            {
                File.AppendAllText(file, output.ToString(), Encoding.Default);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Data could not be written to the CSV file.");
                // return;
            }

            }


           

            int finalLinks = 0;
            string temp = "";
            foreach (var item in items)
            {
                // Array.Clear(mainsite.outLinks);
                if (item == null)
                {
                    continue;
                }
                if (Program.MainList.Contains(item))
                {
                    continue;
                }

                // Program.MainList = "sd";

                String[] newLine = { item.ToString(), mainLink };
                output.AppendLine(string.Join(separator, newLine));
                finalLinks++;
                Program.MainList += item + '-';
                temp += item.ToString() + '\n';
            }

            try
            {
                File.AppendAllText(file, output.ToString(), Encoding.Default);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Data could not be written to the CSV file.");
                // return;
            }
            Console.WriteLine(file);

            result = @"Result-" + mainLink + ".csv";

            mainsite.finaloutLinks = new string[finalLinks];
            // var ERR = temp.Split('\n');
            mainsite.finaloutLinks = temp.Split('\n');
            for (int i = 0; i < finalLinks; i++)
            {

            }
            return result;

        }
    }





}