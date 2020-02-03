
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
                    GetDataSetA(i);
                });
                Parallel.For(0, b.Length, i =>
                {
                    GetDataSetB(i);
                });
                int[][] resultMatrix = multiplyMatrix(a, b, c);
                string resMatrixElementString = ConcatenateResultMatrixElements(resultMatrix);
                string md5tHashedMatrixString = HashStringIntoMD5(resMatrixElementString);
                PostResult(md5tHashedMatrixString);
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
        private static void PostResult(string md5)
        {
           string stringData = JsonConvert.SerializeObject(md5);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8,"application/json");
            HttpResponseMessage response = client.PostAsync("api/numbers/validate", contentData).Result;
            Console.WriteLine("JSON TO POST = {0}", stringData);
            if (response.IsSuccessStatusCode)
             {
                 var content = response.Content.ReadAsStringAsync().Result;
                 var obj = JsonConvert.DeserializeObject<PostRequest>(content);
                 Console.WriteLine("Validation passphrase is:   "+ obj.Value);

             }
             else
                 Console.WriteLine("Post Request Error");
        }
           private static void GetDataSetA(int i)
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
           private static void GetDataSetB(int i)
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


       
       public static int[][] multiplyMatrix(int[][]a,int[][]b, int[][] c)
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
     public   static string ConcatenateResultMatrixElements(int[][]c)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < c.Length; i++)
            {
                for(int j = 0; j < c[0].Length; j++)
                {
                    sb.Append(c[i][j]);
                }
                
               
            }
            return sb.ToString();
        }
    
      public  static string HashStringIntoMD5(string str)
        {
            var md5Hash = MD5.Create();
            var sourceBytes = Encoding.UTF8.GetBytes(str);
            var hashBytes = md5Hash.ComputeHash(sourceBytes);
            var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            return hash;
        }
    }
}
