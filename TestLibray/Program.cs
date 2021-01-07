using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QkuangLibrary.DataStruct;

namespace TestLibray
{
    
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = new int[] { 1, 2, 3, 4, 5 };
            int[] b = new int[10];
            // a.CopyTo(b, 2);
            Console.WriteLine(a.ToString());

            Console.ReadKey();
        }


    }
}
