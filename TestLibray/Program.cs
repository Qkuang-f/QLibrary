using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QkuangLibrary.DataStruct;

namespace TestLibray
{
    public class A<T>
    {
         class B { 
        
        
        }
    }
   
  
    class Program
    {

        static void Main(string[] args)
        {
            //A<string> a = new A<string>();
            SingleLinkList<string> list1 = new SingleLinkList<string>();
            Console.WriteLine("是否是空表：{0}", list1.IsEmpty);
            list1.Insert("你好", 0);
            list1.Append("早上");
            list1.Append("中上");

            list1.Insert("陌生人", 0);
            list1.Insert("傻逼", 2);

            foreach (var item in list1)
            {
                Console.Write(item+"-");
            }

            Console.WriteLine("表长：{0}", list1.GetLength);
            Console.WriteLine("是否是空表：{0}", list1.IsEmpty);
            Console.WriteLine("删除的是：{0}", list1.Delete(2));
            Console.WriteLine("表长：{0}", list1.GetLength);

            Console.WriteLine("获取的是：{0}", list1.GetElem(0));
            Console.WriteLine("获取的是：{0}", list1.Locate("中上"));
            Console.WriteLine("表长：{0}", list1.GetLength);
            list1.Clear();
            list1.Append("中上");


            foreach (var item in list1)
            {
                Console.Write(item + "-");
            }

            Console.ReadKey();


        }



    }
}
