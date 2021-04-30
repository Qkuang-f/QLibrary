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

        public static void ListPrint(SingleLinkList<string> lis)
        {
            foreach (var item in lis)
            {
                Console.Write(item + "-");
            }
            Console.WriteLine();
        }

        public static void StackPrint(LinkStack<string> lis)
        {
            foreach (var item in lis)
            {
                Console.Write(item + "-");
               
            }
            Console.WriteLine();
        }

     

        public static void DataStructPrint(IEnumerable<string> lis)
        {
            Console.Write("数据结构：");
            foreach (var item in lis)
            {
                Console.Write(item + "-");
            }
            Console.WriteLine();
        }


        public static int Test(int n)
        {
            if (n <= 0)
            {
                return 1;
            }
            else
            {
                return n * Test(n - 1);
            }
        }

        static void Main(string[] args)
        {

            Console.WriteLine(Test(3));

            #region 线性表测试
            ////A<string> a = new A<string>();
            //SingleLinkList<string> list1 = new SingleLinkList<string>();
            //Console.WriteLine("是否是空表：{0}", list1.IsEmpty);
            //list1.Append("早上");
            //ListPrint(list1);

            //Console.WriteLine(list1.GetLength);
            //list1.Insert("你好", 0);
            //ListPrint(list1);

            //list1.Append("中上");
            //ListPrint(list1);


            //list1.Insert("陌生人", 0);
            //ListPrint(list1);
            //Console.WriteLine(list1.GetLength);

            //list1.Insert("傻逼", list1.GetLength-1);
            //ListPrint(list1);


            //Console.WriteLine("倒置表：" + list1.ReverseList());
            //ListPrint(list1);


            //Console.WriteLine("表长：{0}", list1.GetLength);
            //Console.WriteLine("是否是空表：{0}", list1.IsEmpty);
            //Console.WriteLine("删除的是：{0}", list1.Delete(4));
            //Console.WriteLine("表长：{0}", list1.GetLength);
            //ListPrint(list1);


            //Console.WriteLine("获取的是：{0}", list1.GetElem(0));
            //Console.WriteLine("获取的是：{0}", list1.Locate("傻逼"));
            //Console.WriteLine("表长：{0}", list1.GetLength);


            //list1.Delete(list1.GetLength - 2);

            //list1.SetElem("睡觉了", 0);

            //ListPrint(list1);

            //Console.WriteLine("倒置表：" + list1.ReverseList());
            //ListPrint(list1);

            //Console.WriteLine("清空");
            //list1.Clear();

            //Console.WriteLine("倒置表：" + list1.ReverseList());
            //ListPrint(list1);

            //list1.Append("中上");

            //Console.WriteLine("倒置表：" + list1.ReverseList());
            //ListPrint(list1);
            //ListPrint(list1);

            //Console.ReadKey();

            #endregion

            #region Stack测试

            //SeqStack<string> list2 = new SeqStack<string>(4);

            //Console.WriteLine($"判空{list2.IsEmpty}，元素：{list2.Count}");
            //DataStructPrint(list2);
            //list2.Clear();
            //DataStructPrint(list2);

            //list2.Push("一");
            //list2.Push("二");
            //list2.Push("三");
            //list2.Push("四");
            //list2.Push("五");
            //DataStructPrint(list2);

            //Console.WriteLine($"判空{list2.IsEmpty}，元素：{list2.Count}");


            //Console.WriteLine($"获取：{list2.GetTop()}");

            //Console.WriteLine($"出栈：{list2.Pop()}");
            //Console.WriteLine($"获取：{list2.GetTop()}");

            //Console.WriteLine($"出栈：{list2.Pop()}");
            //Console.WriteLine($"出栈：{list2.Pop()}");
            //Console.WriteLine($"判空{list2.IsEmpty}，元素：{list2.Count}");

            //Console.WriteLine($"出栈：{list2.Pop()}");
            //Console.WriteLine($"出栈：{list2.Pop()}");
            //Console.WriteLine($"出栈：{list2.Pop()}");
            //Console.WriteLine($"判空{list2.IsEmpty}，元素：{list2.Count}");


            //list2.Push("六");
            //DataStructPrint(list2);


            #endregion

            #region 队列

            LinkQueue<string> list = new LinkQueue<string>();

            Console.WriteLine($"元素个数：{list.Count}；是否空：{list.IsEmpty}");

            DataStructPrint(list);

            list.EnQueue("一");
            list.EnQueue("二");
            list.EnQueue("三");
            Console.WriteLine($"元素个数：{list.Count}；是否空：{list.IsEmpty}");

            list.EnQueue("四");
            list.EnQueue("五");
            list.EnQueue("六");

            DataStructPrint(list);

            Console.WriteLine($"元素个数：{list.Count}；是否空：{list.IsEmpty}");

            Console.WriteLine($"取出：{list.DeQueue()}");
            Console.WriteLine($"取出：{list.DeQueue()}");
            DataStructPrint(list);

            Console.WriteLine($"队头：{list.GetHead()}");

            list.Clear();
            //Console.WriteLine($"取出：{list.DeQueue()}");
            //Console.WriteLine($"取出：{list.DeQueue()}");
            //Console.WriteLine($"取出：{list.DeQueue()}");
            //Console.WriteLine($"取出：{list.DeQueue()}");


            DataStructPrint(list);

            #endregion

            Console.ReadKey();

        }



    }
}
