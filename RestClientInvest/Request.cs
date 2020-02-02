
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

using System.Threading.Tasks;

namespace RestClientInvest
{
    
  public  class Request:HttpRequestException
    {

        public int StatusCode { get; set; }

        public string Content { get; set; }

        static int[][] a = new int[1000][];
        static int[][] b = new int[1000][];
        static int[][] c = new int[1000][];
        static List<int> tempArr = new List<int>();
        static HttpClient client = new HttpClient();
        static string baseAddr = "https://recruitment-test.investcloud.com/";

       
   public static void MainRequest()
        {
            instanitateArrays(a, b, c);
            client.BaseAddress = new Uri(baseAddr);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync("api/numbers/init/1000").Result;
            Console.WriteLine("SYSTEM COMPUTING .......");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Initializations: Successful");
                Parallel.For(0, a.Length, i =>
                {

                    DeserializeMatrixA(i);

                });
                Parallel.For(0, b.Length, i =>
                {


                    DeserializeMatrixB(i);
                });

                int[][] tempC = multipleMatrix(a, b, c);

                string rows = ResultAndRow(tempC);
               
                string finalMD5 = ConvertToMD5(rows);
                string fmd5 = GetMd5Hash(rows);
                var hashedString = new MD5Object();
                PostResult(hashedString);
            }




        }
        private static void instanitateArrays(int[][] a, int[][] b, int[][] c)
        {
            Parallel.For(0, a.Length, i => {
                a[i] = new int[1000];
                b[i] = new int[1000];
                c[i] = new int[1000];
              
                
            });
           
        }
        private static void PostResult(MD5Object md5)
        {
           
            
             
            string stringData = JsonConvert.SerializeObject(md5);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8,"application/json");
            HttpResponseMessage response = client.PostAsync("api/numbers/validate", contentData).Result;
            

            if (response.IsSuccessStatusCode)
             {
                 var content = response.Content.ReadAsStringAsync().Result;
                 var obj = JsonConvert.DeserializeObject<PostRequest>(content);
                 Console.WriteLine("Validation passphrase is:   "+ obj.Value);

             }
             else
                 Console.WriteLine("Post Request Error");
        }
           private static void DeserializeMatrixA(int i)
           {

               StringBuilder sb = new StringBuilder();
               sb.Append("api/numbers/A/row/");
               sb.Append(i);
               string url = sb.ToString();
             
               var response = client.GetAsync(url).Result;
               var content = response.Content.ReadAsStringAsync().Result;
               if (response.IsSuccessStatusCode)
               {

                   var obj = JsonConvert.DeserializeObject<JSONObject>(content);
                   a[i] = obj.Value;
            
               }
           }
           private static void DeserializeMatrixB(int i)
           {
               StringBuilder sb = new StringBuilder();
               sb.Append("api/numbers/B/row/");
               sb.Append(i);
               string url = sb.ToString();
             
               var response = client.GetAsync(url).Result;
               var content = response.Content.ReadAsStringAsync().Result;
               if (response.IsSuccessStatusCode)
               {

                   var obj = JsonConvert.DeserializeObject<JSONObject>(content);
                   b[i] = obj.Value;
                 
               }
           }


       
       public static int[][] multipleMatrix(int[][]a,int[][]b, int[][] c)
        {
            Parallel.For(0, a.Length, i => { 

                for(int j = 0; j < b.Length; j++)
                {
                    int temp = 0;
                    for (int k = 0; k < b[0].Length; k++)
                    {
                       temp +=  a[i][k] * b[k][j];
                    }
                    c[i][j] = temp;
                }
            });
            return c;
            
        }
     public   static string ResultAndRow(int[][]c)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < c.Length; i++)
            {
                for(int j = 0; j < c[0].Length; j++)
                {
                    sb.Append(c[i][j]);
                }
                //string s = string.Join("", c[i]);
                
               
            }
            return sb.ToString();
        }
     public   static string ResultAndCol(int[][]c)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder cb = null;
            
            for (int i = 0; i < c[0].Length; i++)
            {
                cb = new StringBuilder();

                for (int j = 0; j < c.Length; j++)
                {
                    cb.Append(c[j][i]);
                }
               
                sb.Append(cb.ToString());
            
            }
            return sb.ToString();
        }
        static string ResultToString(int[][] c)
        {
           
            string result = "";

            for (int i = 0;i< c.Length;i++)
              {
               result+= string.Join("", c[i]);
              }
           
            return result;
        }
      public  static string ConvertToMD5(string str)
        {
            var md5Hash = MD5.Create();
            var sourceBytes = Encoding.UTF8.GetBytes(str);
            var hashBytes = md5Hash.ComputeHash(sourceBytes);

            var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

            return hash;
        }
        public static string GetMd5Hash(string input)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }



    }
}
