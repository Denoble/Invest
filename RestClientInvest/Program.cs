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

         }


       

    }

   
}
