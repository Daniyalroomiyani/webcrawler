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
        static async Task Main(string[] args)
        {


            string file = Environment.CurrentDirectory + @"\Result.csv";
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            String[] headings = { "Name", "Link", "Verb", "Hits", "OutLinks" };
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
            string srch = "سایت";
            // int dpth = 100;
            // string[] linksout;

            await Crawler(html , srch);
            // if (linksout.Count() < dpth)
            // {
            //     dpth = linksout.Count();
            // }
            // string[] linksout2;

            // for (int i = 0; i < dpth; i++)
            // {
            //     Crawler(linksout[i], srch);
            // }




            Console.WriteLine("All IS Done .....\n ");


        }

        public static string filterLinks(string str, string mainDomain)
        {
            // string result = "";
            // result= str.Substring(6 , str.Length-7);
            if (!str.Contains("www"))
            {
                return "-1";
            }
            if (!(str.Contains(".ir") || str.Contains(".com")))
            {
                return "-1";
            }
            mainDomain = mainDomain.Substring(11);
            if (str.Contains(mainDomain))
                return "-1";
            else
                return str;
        }


        private static async Task Crawler(string url, string Verb)
        {

           await Task.Run(async ()=>{
                try
                {
                    HtmlWeb web = new HtmlWeb();


                    var htmlDoc = web.Load(url).ParsedText;


                    var cRegexName = new Regex("<title>(.*?)</title>");
                    var ResultName = cRegexName.Matches(htmlDoc)[0].Groups[1].Value.ToString();


                    var cRegexvarb = new Regex(Verb);
                    var ResultverbCount = cRegexvarb.Matches(htmlDoc).Count;


                    // var cRegex = new Regex("<a[\\s]+([^>]+)>((?:.(?!\\<\\/a\\>))*.)</a>");
                    // var cRegex = new Regex("/(?:(?:https?|ftp):\\/\\/)?[\\w\\-?=%.]+\\.[\\w\\-?=%.]");
                    var cRegex = new Regex("((https?|ftp|gopher|telnet|file):((//)|(\\\\))+[\\w\\d:#@%/;$()~_?\\+-=\\\\\\.&]*)");

                    var Result = cRegex.Matches(htmlDoc);



                    int ValidCountResult = 0;
                    for (int i = 0; i < Result.Count; i++)
                    {
                        if (filterLinks(Result[i].Groups[1].Value.ToString(), url) != "-1")
                        {
                            ValidCountResult++;
                            // outList[i]  = Result[i].Groups[1].Value.ToString().Contains(html).ToString();
                            // outList[i] = filterLinks( Result[i].Groups[1].Value.ToString() , html ); 
                        }

                    }

                    string[] outList = new string[ValidCountResult];

                    for (int i = 0; i < ValidCountResult; i++)
                    {
                        if (filterLinks(Result[i].Groups[1].Value.ToString(), url) != "-1")
                        {
                            // ValidCountResult++;
                            // outList[i]  = Result[i].Groups[1].Value.ToString().Contains(html).ToString();
                            outList[i] = filterLinks(Result[i].Groups[1].Value.ToString(), url);
                        }

                    }

                    WebSite mysite = new WebSite(ResultName, url, outList, Verb, ResultverbCount);
                    mysite.writeExel();
                    foreach (var item in mysite.finaloutLinks)
                    {
                        await Crawler(item , Verb);
                        
                    }
                   

                }
                catch (Exception exep)
                {
                    Console.WriteLine(exep.Message);
                   


                }
            });

        }

    }
}