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

        /// <summary>
        /// 修改指定位置的值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="i"></param>
        void SetElem(T value, int i);
    }



    #region 顺序表

    /// <summary>
    /// 动态实现的顺序表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SeqList<T> : IlinearList<T>, IEnumerator<T>, IEnumerable<T>
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
        public int GetLength => currentIndex + 1;

        public bool IsEmpty
        {
            get
            {
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
            if (!IsEmpty && IsOnIndex(i))
            {
                result = array[i];
                for (; i < currentIndex; i++)
                {
                    array[i] = array[i + 1];
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
            if (IsOnIndex(i) || i == currentIndex + 1)
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

            for (int i = 0; i < GetLength; i++)
            {
                if (value.Equals(array[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public void SetElem(T value, int i)
        {
            this.array[i] = value;
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
            if (i > currentIndex || i < 0)
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


    #region 遗弃单链表

    /// <summary>
    /// 已经抛弃，不带头节点，单链表。旧代码，太烂了，放这里，已经重新写了一个单链表。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OldSingleLinkList<T> : IlinearList<T>, IEnumerable<T>, IEnumerator<T>
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
            }
            else
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

                for (int j = 0; j < i; j++)
                {
                    q = p;
                    p = p.NextNode;
                }

                q.NextNode = p.NextNode;
                return p.Data;

            }
            else
            {
                throw new Exception("索引超出链表范围");
            }
        }

        public T GetElem(int i)
        {
            if (IsOnIndex(i))
            {
                Node p = this.head;
                for (int j = 0; j < i; j++)
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

            }
            else if (i == GetLength)
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

        public void SetElem(T value, int i)
        {
            throw new NotImplementedException();
        }
    }

    #endregion

    #region 单链表(已经测试)



    /// <summary>
    /// 带头节点单链表(两个空节点作为首尾，并不存值）
    /// 特点：0位置插入，删除、末尾追加元素最快的。
    /// </summary>

    public class SingleLinkList<T> : IlinearList<T>, IEnumerable<T>, IEnumerator<T>
    {
        public class Node
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



        private Node head;
        private Node tail;
        private int length;
        private Node current;   //枚举器循环指针

        public int GetLength => this.length;

        public bool IsEmpty => this.head.NextNode == this.tail;

        public T Current => current.Data;


        object IEnumerator.Current => throw new NotImplementedException();

        public SingleLinkList()
        {
            this.head = new Node();
            this.tail = new Node();
            this.head.NextNode = this.tail;
            this.length = 0;
        }




        //*********线性表接口实现


        public void Append(T item)
        {
            //注意这里实际是值复制，因此需要重置尾节点。
            NodeBeforeInsert(this.tail, item);
            this.tail = tail.NextNode;
        }

        public void Clear()
        {
            this.head.NextNode = this.tail;
        }

        public T Delete(int i)
        {
            Node p = GetNode(i);
            T value = p.Data;

            NodeDelete(p);
            return value;
        }

        public T GetElem(int i)
        {
            return GetNode(i).Data;
        }

        public void Insert(T item, int i)
        {
            NodeBeforeInsert(GetNode(i), item);
        }

        public int Locate(T value)
        {
            Node p;
            return GetIndexAndNode(value, out p);
        }

        public void SetElem(T value, int i)
        {
            GetNode(i).Data = value;
        }


        //**********单链表扩展

        /// <summary>
        /// 按值查找并重设值
        /// </summary>
        /// <param name="value">查找值</param>
        /// <param name="item">重设值</param>
        /// <returns></returns>
        public bool SetElem(T value, T item)
        {
            return GetElemOperate(value, item, (n, t) => n.Data = item);


        }

        /// <summary>
        /// 按值查找并前插
        /// </summary>
        /// <param name="value">查找值</param>
        /// <param name="item">插入值</param>
        /// <returns></returns>
        public bool ElemAfterInsert(T value, T item)
        {
            return GetElemOperate(value, item, NodeAfterInsert);
        }
        /// <summary>
        /// 按值查找并后插
        /// </summary>
        /// <param name="value">查找值</param>
        /// <param name="item">插入值</param>
        /// <returns></returns>
        public bool ElemBeforeInsert(T value, T item)
        {
            return GetElemOperate(value, item, NodeBeforeInsert);
        }

        public bool Delete(T value)
        {
            Node p;
            if (GetIndexAndNode(value, out p) == -1)
                return false;
            NodeDelete(p);

            return true;
        }


        //**********节点封装方法，不公开原因，避免用户违规操作，删除首尾节点，或在首节点前插入值、尾节点后插入值。

        /// <summary>
        /// 按位查找节点
        /// </summary>
        /// <param name="i">索引</param>
        /// <returns></returns>
        private Node GetNode(int i)
        {
            if (i >= this.length)
                throw new Exception("索引超出链表范围");
            Node p = this.head;

            for (int j = 0; j <= i; j++)
            {
                p = p.NextNode;
            }

            return p;
        }

        /// <summary>
        /// 按值查找节点，没找到则返回-1，out null
        /// </summary>
        /// <param name="v">值</param>
        /// <param name="node">值的节点</param>
        /// <returns>索引</returns>
        private int GetIndexAndNode(T v, out Node node)
        {
            int value = -1;
            node = null;
            Node p = this.head;
            while (p.NextNode != this.tail)
            {
                p = p.NextNode;
                value++;
                if (p.Data.Equals(v))
                {
                    node = p;
                    return value;
                }
            }

            return -1;
        }

        /// <summary>
        /// 在节点之后插入值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        private void NodeAfterInsert(Node node, T value)
        {
            if (node == null)
                return;
            Node p = new Node(value, node.NextNode);
            node.NextNode = p;
            this.length++;
        }

        /// <summary>
        /// 在节点之前插入值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        private void NodeBeforeInsert(Node node, T value)
        {
            if (node == null)
                return;
            T t = node.Data;
            node.Data = value;
            NodeAfterInsert(node, t);
        }

        /// <summary>
        /// 节点删除，如果删除到了尾节点自动重置尾节点
        /// </summary>
        /// <param name="node"></param>
        private void NodeDelete(Node node)
        {
            if (node == null)
                return;

            if (node.NextNode == this.tail)
                this.tail = node;
            node.Data = node.NextNode.Data;
            node.NextNode = node.NextNode.NextNode;
            this.length--;
            //如果删除节点为尾节点，需要重置尾节点标识


        }

        //**********辅助方法

        /// <summary>
        /// 按值查找并操作
        /// </summary>
        /// <param name="value">查找的值</param>
        /// <param name="item">操作需要的参数</param>
        /// <param name="op">操作</param>
        /// <returns></returns>
        private bool GetElemOperate(T value, T item, Action<Node, T> op)
        {
            Node p;
            if (GetIndexAndNode(value, out p) == -1)
                return false;
            op(p, item);
            return true;
        }


        //*******枚举器接口实现
        public IEnumerator<T> GetEnumerator()
        {
            Reset();
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            if (this.current.NextNode != this.tail)
            {
                this.current = this.current.NextNode;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            this.current = this.head;
        }

        public void Dispose()
        {

        }
    }

    #endregion


    #region 双链表(已经测试）

    /// <summary>
    /// 只带头节点双链表，操作没有单链表多，因为相比之下，单链表更好
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public class DoubleLinkList<T> : IlinearList<T>,IEnumerable<T>,IEnumerator<T>
    {

        /// <summary>
        /// 双链表节点
        /// </summary>
        class DNode
        {
            public T Data { get; set; }
            public DNode NextNode { get; set; }
            public DNode PriorNode { get; set; }

            public DNode(T item) => this.Data = item;
            public DNode() => this.Data = default;

        }

        private DNode head;
        private DNode tail;
        private int length;

        private DNode current;

        public int GetLength => this.length;

        public bool IsEmpty => this.head == this.tail;

        public T Current => current.Data;

        object IEnumerator.Current => throw new NotImplementedException();

        public DoubleLinkList()
        {
            this.head = new DNode();
            this.tail = this.head;
            this.length = 0;
        }

        //***********线性表接口实现

        public void Append(T item)
        {
            NodeAfterInsert(item,this.tail);
        }

        public void Clear()
        {
            DNode p = this.head;
            while (p != null)
            {
                //将所有节点的前驱置空
                p.PriorNode = null;
                p = p.NextNode;
            }
            this.head.NextNode = null;
            this.tail = this.head;

        }

        public T Delete(int i)
        {
            
            return NodeDelete(GetNode(i));
        }

        public T GetElem(int i)
        {
            DNode p = GetNode(i);
            return p.Data;
        }

        public void Insert(T item, int i)
        {
           
            NodeBeforeInsert(item, GetNode(i));
        }

        public int Locate(T value)
        {
            DNode p;
            return GetIndexAndNode(value,out p);
        }

        public void SetElem(T value, int i)
        {
            GetNode(i).Data = value;
        }


        //*********双链表封装方法

        /// <summary>
        /// 按位查找，因为是双链表，因此允许从后往前找。这点，比单链表快
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private DNode GetNode(int i)
        {
            if (i >= this.length)
                //排除了空表的情况
                throw new Exception("索引超出链表范围");
            DNode p;
            
            if (i > length / 2)
            {
                p = this.tail;
               
                for (int j = this.length - 1; j > i; j--)
                {
                    p = p.PriorNode;
                }

            }
            else
            {
                p = this.head;
                
                for (int j=0; j <= i; j++)
                {
                    p = p.NextNode;
                }
            }

            return p;
        }


        /// <summary>
        /// 按值查找，找不到返回 -1 ，out null
        /// </summary>
        /// <param name="value">查找值</param>
        /// <param name="node"></param>
        /// <returns></returns>
        private int GetIndexAndNode(T value, out DNode node)
        {
            int i = -1;
            node = null;
            DNode p = this.head;
            while (p.NextNode != null)
            {
                i++;
                p = p.NextNode;
                if (p.Data.Equals(value))
                {
                    node = p;
                    return i;
                }
            }

            return -1;

        }

        /// <summary>
        /// 节点前插
        /// </summary>
        /// <param name="item"></param>
        /// <param name="node"></param>
        private void NodeBeforeInsert(T item, DNode node)
        {
            if (node == null)
                return;
            DNode t = new DNode(item);
            DNode q = node.PriorNode;
            t.NextNode = node;
            node.PriorNode = t;
            q.NextNode = t;
            t.PriorNode = q;
            this.length++;
        }

        /// <summary>
        /// 节点后插
        /// </summary>
        /// <param name="item">插入的元素</param>
        /// <param name="node"></param>
        private void NodeAfterInsert(T item, DNode node)
        {
            if (node == null)
                return;
            DNode p = node.NextNode;
            DNode t = new DNode(item);
            if (p == null)
            {
                //后驱节点为null，表示当前为尾节点
                this.length++;
                node.NextNode = t;
                t.PriorNode = node;
                this.tail = t;
                return;
            }

            NodeBeforeInsert(item, p);
        }

        /// <summary>
        /// 节点删除，如果失败，返回Default值
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private T NodeDelete(DNode node)
        {
            if (node == null)
                return default;
            this.length--;
            DNode q = node.PriorNode;
            DNode p = node.NextNode;

            q.NextNode = p;

            if (p == null)
            {
                //当前节点为尾节点，在删除节点前重置尾节点
                this.tail = q;
            }
            else
            {
                p.PriorNode = q;
            }
            return node.Data;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Reset();
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {

            if (this.current.NextNode == null)
            {
                return false;
            }else
            {
                this.current = this.current.NextNode;
                return true;
            }
        }

        public void Reset()
        {
            this.current = this.head;
        }

        public void Dispose()
        {
            
        }
    }

    #endregion



}
