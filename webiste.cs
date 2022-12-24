
using System.IO;
using System.Text;
using System;


namespace web{

    public class WebSite{
        public string name;
        public string link;

        public string[] outLinks;

        public int hits;

        public string verb;
        public string[] finaloutLinks;

        public WebSite(string name , string link , string[] outLinks ,  string verb,int hits){
            this.name= name;
            this.link = link;
            this.outLinks = outLinks;
            this.verb=verb;
            this.hits=hits;
        }


        public  void writeExel(){
            
            string file = Environment.CurrentDirectory+@"\Result.csv";
            // String[] headings = { "Name", "Link", "Verb", "Hits" , "OutLonks" };
            String separator = ",";
            StringBuilder output = new StringBuilder();
            // output.AppendLine(string.Join(separator, headings));


      
            String[] newLine = { this.name ,this.link, this.verb ,this.hits.ToString(),ListToString(this.outLinks, this) };
            output.AppendLine(string.Join(separator, newLine));
        
                    try
                {
                    File.AppendAllText(file, output.ToString(),Encoding.Default);
                    Console.WriteLine(file);

                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Data could not be written to the CSV file.");
                        return;
                    }
            Console.WriteLine(file);

        }
        private static string ListToString(string[] items , WebSite mainsite){

              string result = "";

             if (items.Count() == 0 )
             {
                return "";
             }
            var  mainLink = mainsite.link.Substring(10);
             if(mainLink.Contains('/')){
                var test = mainLink.Substring(0 , mainLink.IndexOf('/'));
                mainLink= test;
             }
             
            string file = Environment.CurrentDirectory+@"\"+mainLink+".csv";
            if(File.Exists(file)){
                File.Delete(file);
            }
         

            String[] headings = { "Link","From" };
            String separator = ",";
            StringBuilder output = new StringBuilder();
            output.AppendLine(string.Join(separator, headings));

                       try
                {
                    File.AppendAllText(file, output.ToString(),Encoding.Default);
                    Console.WriteLine(file);

                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Data could not be written to the CSV file.");
                        // return;
                    }
           
            int finalLinks = 0;
            string temp="" ;
              foreach (var item in items)
              {
                // Array.Clear(mainsite.outLinks);
                if(item == null){
                    continue;
                }

                String[] newLine = { item.ToString() ,  mainLink };
                output.AppendLine(string.Join(separator, newLine));
                finalLinks++;
                temp+=item.ToString()+'\n';
              }

                       try
                {
                    File.AppendAllText(file, output.ToString(),Encoding.Default);
                    Console.WriteLine(file);

                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Data could not be written to the CSV file.");
                        // return;
                    }
            Console.WriteLine(file);
             
                result = @"Result"+mainLink+".csv";
                
                mainsite.finaloutLinks = new string[finalLinks];
                // var ERR = temp.Split('\n');
                mainsite.finaloutLinks = temp.Split('\n');
                for ( int i =0 ; i< finalLinks ; i++){
                    
                }
              return result;  

        } 
    }





}