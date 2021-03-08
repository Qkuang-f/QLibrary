using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace QkuangLibrary.DataStruct
{
    //预计实现顺序表（静态、动态）链表、结合链表半动态顺序表

    /// <summary>
    /// 线性表接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IlinearList<T>
    {
        /// <summary>
        /// 求元素个数
        /// </summary>
        int GetLength { get; }
        /// <summary>
        /// 清空表
        /// </summary>
        void Clear();
        /// <summary>
        /// 是否空表
        /// </summary>
        bool IsEmpty { get; }
        /// <summary>
        /// 附加（追加）操作
        /// </summary>
        /// <param name="item"></param>
        void Append(T item);
        /// <summary>
        /// 插入操作,索引从0开始
        /// </summary>
        /// <param name="item">元素</param>
        /// <param name="i">插入后的位置</param>
        void Insert(T item, int i);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        T Delete(int i);
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        T GetElem(int i);
        /// <summary>
        /// 按值查找
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        int Locate(T value);
    }

 

    #region 顺序表

    /// <summary>
    /// 动态实现的顺序表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SeqList<T> : IlinearList<T>,IEnumerator<T>,IEnumerable<T>
    {
        private int INDEX;          //枚举器使用光标

        private T[] array;              //存储
        private int currentIndex;       //当前最后一个元素索引
        private int currentLength;      //当前容器的大小

        /// <summary>
        /// 创建顺序表
        /// </summary>
        /// <param name="magnitude">数量级，超过当前容量时，下次扩展该数量的容量</param>
        public SeqList(int magnitude)
        {
            
            Magnitude = magnitude;
            array = new T[magnitude];
            currentLength = magnitude;
            currentIndex = -1;
        }

        public T this[int index]
        {
            get
            {
                if (index > currentIndex)
                {
                    throw new Exception("索引超出范围");
                }
                return array[index];
            }
            set
            {
                if (index > currentIndex)
                {
                    throw new Exception("索引超出范围");
                }
                array[index] = value;
            }
        }
        /// <summary>
        /// 数量级，用于每次扩展时决定扩展的大小
        /// </summary>
        public int Magnitude { get; set; }
        public int GetLength => currentIndex+1;

        public bool IsEmpty { 
            get{
                if (currentIndex == -1) return true;
                return false;
            }
        }

        public T Current => array[INDEX];        

        object IEnumerator.Current => array[INDEX];

        public void Append(T item)
        {
            //currentIndex++;
            //if (currentIndex >= currentLength)
            //{
            //    array = ExpandCapacity();
            //}
            //array[currentIndex] = item;

            Insert(item, currentIndex + 1);
        }
        /// <summary>
        /// 清空表，但是容器的内存大小任然保留
        /// </summary>
        public void Clear()
        {
            currentIndex = -1;
        }

        public T Delete(int i)
        {
            T result = default(T);
            if (!IsEmpty&&IsOnIndex(i))
            {
                result = array[i];
                for (; i < currentIndex; i++)
                {
                    array[i] = array[i+1];
                }
                currentIndex--;
            }
            else
            {
                throw new Exception("索引超出范围");
            }
            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="v">按值查找</param>
        /// <returns></returns>
        public T Delete(T v)
        {
            return this.Delete(this.Locate(v));
        }

        public void Dispose()
        {
            Console.WriteLine("清理Dispose");
            INDEX = -1;
        }

        public T GetElem(int i)
        {
            if (!IsOnIndex(i))
                throw new Exception("索引当前超出容量");
            return this[i];
        }

        public IEnumerator<T> GetEnumerator()
        {
            this.Reset();
            return this;
        }

        public void Insert(T item, int i)
        {
            if (IsOnIndex(i)||i==currentIndex+1)
            {
                currentIndex++;
                if (currentIndex >= currentLength)
                {
                    array = ExpandCapacity();
                }

                for (int j = currentIndex; j > i; j--)
                {
                    array[j] = array[j - 1];
                }
                array[i] = item;
            }
            else
            {
                throw new Exception("索引当前超出容量");
            }
           
        }

        public int Locate(T value)
        {
            if (IsEmpty)
            {
                return -1;
            }

            for(int i = 0; i < GetLength; i++)
            {
                if (value.Equals(array[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public bool MoveNext()
        {
            if (++INDEX > currentIndex)
                return false;
            return true;
        }

        public void Reset()
        {
            INDEX = -1;
        }

        //扩展容量
        private T[] ExpandCapacity()
        {

            T[] result = new T[currentLength + Magnitude];
            currentLength = result.Length;
            array.CopyTo(result, 0);
            return result;
        }

        //Ienumerable接口实现
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        //判断索引是否在元素内
        private bool IsOnIndex(int i)
        {
            if (i > currentIndex||i<0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    #endregion


    #region 单链表


    public class SingleLinkList<T> : IlinearList<T>,IEnumerable<T>,IEnumerator<T>
    {
       
        class Node
        {
            public T Data { get; set; }     //存储数据
            public Node NextNode { get; set; }      //下一个节点

            // 四种情况下的构造函数
            public Node(T d, Node next)
            {
                Data = d;
                NextNode = next;
            }
            public Node(Node next) => NextNode = next;
            public Node(T d) => Data = d;
            public Node() => this.Data = default;


        }

        private Node head;      //头节点

        /// <summary>
        /// 判断索引是否存在于当前链表中
        /// </summary>
        /// <param name="i">索引</param>
        /// <returns></returns>
        private bool IsOnIndex(int i)
        {
            if (i < GetLength && i >= 0)
            {
                return true;
            }else
            {
                return false;
            }
        }


        // 两个IEnumera的接口实现

        private Node enumerasCurrentField;


        public T Current
        {

            get
            {
                //Console.WriteLine("Current");
                return this.enumerasCurrentField.Data;
            }

        }

        object IEnumerator.Current => throw new NotImplementedException();
        //************接口实现

        /// <summary>
        /// 每次都需要遍历链表，经历少用。
        /// </summary>
        public int GetLength
        {
            get
            {
                int i = 0;
                Node p = this.head;
                while (p != null)
                {
                    p = p.NextNode;
                    i++;
                }
                return i;
            }
        }

        public bool IsEmpty
        {
            get
            {
                if (this.head == null)
                    return true;
                return false;
            }
        }


        public void Append(T item)
        {
            //Console.WriteLine("Append");
            Node p = new Node(item);

            if (this.head == null)
            {
                this.head = p;
                return;
            }

            Node q = this.head;
            while (q.NextNode != null)
            {
                q = q.NextNode;
            }

            
            q.NextNode = p;
            
        }

        public void Clear()
        {
            this.head = null;
        }

        public T Delete(int i)
        {
            
            if (IsOnIndex(i))
            {
                Node q = new Node();
                Node p = this.head;     //删除指针
                if (i == 0)
                {
                    this.head = this.head.NextNode;
                    return p.Data;
                }

                for(int j = 0; j < i; j++)
                {
                    q = p;
                    p = p.NextNode;
                }

                q.NextNode = p.NextNode;
                return p.Data;

            }else
            {
                throw new Exception("索引超出链表范围");
            }
        }

        public T GetElem(int i)
        {
            if (IsOnIndex(i))
            {
                Node p = this.head;
                for(int j = 0; j < i; j++)
                {
                    p = p.NextNode;
                }
                return p.Data;
            }
            else
            {
                throw new Exception("索引超出链表范围");
            }
        }

        public void Insert(T item, int i)
        {
           
            if (IsOnIndex(i))
            {
               

                Node q = new Node();        // 前置
                Node p = this.head;         //后置

                if (i == 0)
                {
                    q = new Node(item);
                    q.NextNode = p;
                    this.head = q;
                    return;
                }

                for (int j = 0; j < i; j++)
                {
                    q = p;
                    p = p.NextNode;
                }

                q.NextNode = new Node(item);
                q.NextNode.NextNode = p;

            }else if (i == GetLength)
            {
                //允许索引为当前最大索引的后一位数
                this.Append(item);
            }
            else
            {
                throw new Exception("索引超出链表范围");
            }
        }

        public int Locate(T value)
        {
            Node p = this.head;
            int i = -1;
            while (p != null)
            {
                i++;
                if (p.Data.Equals(value))
                    return i;
                    
                p = p.NextNode;
            }

            return -1;
        }



        //************Over

        public IEnumerator<T> GetEnumerator()
        {
            //Console.WriteLine("GetEnumerator");
            Reset();
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Console.WriteLine("GetEnumerator，非泛型");

            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            //Console.WriteLine("MoveNext");
            this.enumerasCurrentField = this.enumerasCurrentField.NextNode;
            if (this.enumerasCurrentField != null)
            {
                
                return true;
            }
            else
            {
                return false;
            }

        }

        public void Reset()
        {
            //Console.WriteLine("Reset");
            this.enumerasCurrentField = new Node();
            this.enumerasCurrentField.NextNode = this.head;
        }

        public void Dispose()
        {
            //Console.WriteLine("Dispose");
        }


    }

    #endregion

}
