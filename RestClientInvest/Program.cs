using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace RestClientInvest
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();
            
            watch.Start();
            Request.MainRequest();
            watch.Stop();

            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds/1000} seconds");

            /* int[][] a = {new int[] { 5, 7,9,10 },
                          new int[]{ 2, 3,3,8 },
                          new int[]{8,10,2,3},
                           new int[]{3,3,4,8}
                         };
             int[][] c = new int[4][];
             int[][] b =
             {
                 new int[] {3,10,12,18},
                 new int[]{12,1,4,9 },
                 new int[]{9,10,12,2},
                 new int[]{3,12,4,10}
             };
             Parallel.For(0, c.Length, i =>
             {
                 c[i] = new int[4];
             });
           int[][]  tc = Request.multipleMatrix(a, b, c);
             
             for(int i =0; i< tc.Length; i++)
             {
                 for(int j=0; j < tc[0].Length; j++)
                 {
                     Console.Write(tc[i][j] + " ");
                 }
                 Console.WriteLine(" ");
             }


             string result = Request.ResultAndRow(tc);


             string ss = Request.GetMd5Hash( result);
             string sss = Request.ConvertToMD5(result);
             StringComparer comparer = StringComparer.OrdinalIgnoreCase;
             if(comparer.Compare(ss,sss)== 0)
             {

                 Console.WriteLine($"ss: {ss}  is same as sss: {sss} regardless");
             }
             bool bb = VerifyMd5Hash( result, sss);
             Console.WriteLine($"ss: {ss}  is same as sss: {sss}");
             Console.WriteLine($"Result: {result} verifications is  {bb}");
             Console.ReadLine();*/
         }


         // Verify a hash against a string.
         static bool VerifyMd5Hash(string input, string hash)
         {
             MD5 md5Hash = MD5.Create();
             // Hash the input.
             string hashOfInput = Request.HashStringIntoMD5( input);

             // Create a StringComparer an compare the hashes.
             StringComparer comparer = StringComparer.OrdinalIgnoreCase;

             if (0 == comparer.Compare(hashOfInput, hash))
             {
                 return true;
             }
             else
             {
                 return false;
             }
        }

    }

   
}
