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
            SeqList<int> a1 = new SeqList<int>(3);
            a1.Append(1);
            a1.Append(2);
            a1.Append(3);
            a1.Append(4);

            foreach (var item in a1)
            {
                Console.Write(item+",");
            }
                Console.WriteLine("当前长度" + a1.GetLength);

            a1.Insert(9, 4);

            foreach (var item in a1)
            {
                Console.Write(item + ",");
            }
            Console.WriteLine("当前长度" + a1.GetLength);

            Console.ReadKey();
        }


    }
}
