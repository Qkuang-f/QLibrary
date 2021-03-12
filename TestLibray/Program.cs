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

        public static void ListPrint(SingleLinkList<string> lis)
        {
            foreach (var item in lis)
            {
                Console.Write(item + "-");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            //A<string> a = new A<string>();
            SingleLinkList<string> list1 = new SingleLinkList<string>();
            Console.WriteLine("是否是空表：{0}", list1.IsEmpty);
            list1.Append("早上");
            ListPrint(list1);

            Console.WriteLine(list1.GetLength);
            list1.Insert("你好", 0);
            ListPrint(list1);

            list1.Append("中上");
            ListPrint(list1);


            list1.Insert("陌生人", 0);
            ListPrint(list1);
            Console.WriteLine(list1.GetLength);

            list1.Insert("傻逼", list1.GetLength-1);
            ListPrint(list1);


            Console.WriteLine("倒置表：" + list1.ReverseList());
            ListPrint(list1);


            Console.WriteLine("表长：{0}", list1.GetLength);
            Console.WriteLine("是否是空表：{0}", list1.IsEmpty);
            Console.WriteLine("删除的是：{0}", list1.Delete(4));
            Console.WriteLine("表长：{0}", list1.GetLength);
            ListPrint(list1);


            Console.WriteLine("获取的是：{0}", list1.GetElem(0));
            Console.WriteLine("获取的是：{0}", list1.Locate("傻逼"));
            Console.WriteLine("表长：{0}", list1.GetLength);


            list1.Delete(list1.GetLength - 2);

            list1.SetElem("睡觉了", 0);

            ListPrint(list1);

            Console.WriteLine("倒置表：" + list1.ReverseList());
            ListPrint(list1);

            Console.WriteLine("清空");
            list1.Clear();

            Console.WriteLine("倒置表：" + list1.ReverseList());
            ListPrint(list1);

            list1.Append("中上");

            Console.WriteLine("倒置表：" + list1.ReverseList());
            ListPrint(list1);
            ListPrint(list1);

            Console.ReadKey();


        }



    }
}
