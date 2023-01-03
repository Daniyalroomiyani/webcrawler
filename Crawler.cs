
using System;
using System.IO;
using System.Text;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using web;
using System.Threading;
using System.Threading.Tasks;
namespace System;

public class Crawler
{
    public string url;

    public Crawler(string t)
    {
        this.url = t;

    }


    public async Task start()
    {


        try
        {
            HtmlWeb web = new HtmlWeb();


            var htmlDoc = web.Load(url).ParsedText;


            var cRegexName = new Regex("<title>(.*?)</title>");
            var ResultName = cRegexName.Matches(htmlDoc)[0].Groups[1].Value.ToString();


            var cRegexvarbF = new Regex(Program.VerbFarsi);
            var ResultverbFarsiCount = cRegexvarbF.Matches(htmlDoc).Count;


            var cRegexvarbE = new Regex(Program.VerbEnglish);
            var ResultverbEnglishCount = cRegexvarbE.Matches(htmlDoc).Count;

            var cRegexvarbF2 = new Regex(Program.VerbFarsi2);
            var ResultverbFarsi2Count = cRegexvarbF2.Matches(htmlDoc).Count;


            var cRegexvarbE2 = new Regex(Program.VerbEnglish2);
            var ResultverbEnglish2Count = cRegexvarbE2.Matches(htmlDoc).Count;




            var Rate =(( ResultverbFarsiCount + ResultverbEnglishCount  ) * 100  ) + ((ResultverbFarsi2Count + ResultverbEnglish2Count ) *50);


            Program.WriteFinalExel(ResultName , url , Rate);

            // var cRegex = new Regex("<a[\\s]+([^>]+)>((?:.(?!\\<\\/a\\>))*.)</a>");
            // var cRegex = new Regex("^((http|https)://)[-a-zA-Z0-9@:%._\\+~#?&//=]{2,256}\\.[a-z]{2,6}\\b([-a-zA-Z0-9@:%._\\+~#?&//=]*)$");
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

            WebSite mysite = new WebSite(ResultName, url, outList,ResultverbFarsiCount ,ResultverbEnglishCount, ResultverbFarsi2Count , ResultverbEnglish2Count);
            mysite.writeExel();

            for (int i = 0; i < mysite.finaloutLinks.Count(); i++)
            {
                var myCR = new Crawler(mysite.finaloutLinks[i]);
                myCR.start();
            }



        }
        catch (Exception exep)
        {
            Console.WriteLine(exep.Message);




        }




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
        if (str.Substring(0, 3) == "www" || str.Substring(0, 4) == "http")
        {
            if (Program.MainList.Contains(str))
            {
                return "-1";
            }


            mainDomain = mainDomain.Substring(mainDomain.IndexOf('.') + 1, mainDomain.Length - mainDomain.IndexOf('.') - 2);
            if (str.Contains(mainDomain))
                return "-1";
            else
                return str;
        }
        else
            return "-1";
    }


}