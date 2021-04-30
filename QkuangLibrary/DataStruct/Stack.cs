using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace QkuangLibrary.DataStruct
{
    /// <summary>
    /// 栈接口
    /// </summary>
    public interface IStack<T>
    {
        /// <summary>
        /// 获取元素个数
        /// </summary>
        int Count { get; }
        /// <summary>
        /// 判空
        /// </summary>
        bool IsEmpty { get; }
        /// <summary>
        /// 清空栈
        /// </summary>
        void Clear();
        /// <summary>
        /// 入栈
        /// </summary>
        /// <param name="item">元素</param>
        bool Push(T item);
        /// <summary>
        /// 出栈，空表时返回default
        /// </summary>
        /// <returns></returns>
        T Pop();

        /// <summary>
        /// 获取栈顶元素
        /// </summary>
        /// <returns></returns>
        T GetTop();
    }

    #region 顺序栈

    /// <summary>
    /// 顺序栈
    /// 枚举器从栈顶往栈底遍历
    /// </summary>
    public class SeqStack<T>:IStack<T>, IEnumerable<T>, IEnumerator<T>
    {
        private int maxsize;    //栈最大容量
        private T[] array;
        private int top;        //空时为-1

        private int currentEnum;

        public T this[int index]
        {
            get
            {
                return array[index];
            }
            set
            {
                array[index] = value;
            }
        }

        /// <summary>
        /// 获取静态长度
        /// </summary>
        public int GetLength => array.Length;

        public int Count => top+1;

        /// <summary>
        /// 判满
        /// </summary>
        public bool IsFull => top >= GetLength-1;
        public bool IsEmpty => top==-1;

        public T Current => array[this.currentEnum];

        object IEnumerator.Current => throw new NotImplementedException();

        /// <summary>
        /// 构造顺序栈
        /// </summary>
        /// <param name="maxsize">最大容量</param>
        public SeqStack(int maxsize)
        {
            //初始化，必须设定最大容量
            top = -1;
            this.maxsize = maxsize;
            array = new T[maxsize];
        }

         public void Clear()
        {
            this.top = -1;
        }

        public bool Push(T item)
        {
            if (IsFull)
                return false;
            array[++top] = item;

            return true;
        }

        public T Pop()
        {
            if (IsEmpty)
                return default;

            return array[top--];
        }

        public T GetTop()
        {
            if (IsEmpty)
                return default;
            return array[top];
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
            currentEnum--;
            return currentEnum >= 0;
        }

        public void Reset()
        {
            this.currentEnum = this.top+1;
        }

        public void Dispose()
        {
            
        }
    }

    #endregion

    #region 链栈

    /// <summary>
    /// 链栈
    /// 注意：这里枚举器是从栈顶往下遍历，因为这样实现起来简单。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkStack<T> : IStack<T>,IEnumerable<T>,IEnumerator<T>
    {
        /// <summary>
        /// 内部类节点用
        /// </summary>
        class Node
        {
            public T Data { get; set; }
            public Node NextNode { get; set; }
            public Node() => this.Data = default;
            public Node(T item) => this.Data = item;
            public Node(T item,Node next)
            {
                this.Data = item;
                this.NextNode = next;
            }

        }

        private Node Top;    //StackTop 指针。

        private int length;

        private Node currentNode;       //Enumerate 需要。


        public int Count => length<0?0:length;

        public bool IsEmpty => Top==null;

        public T Current => this.currentNode.Data;

        object IEnumerator.Current => throw new NotImplementedException();

        public void Clear()=> this.Top = null;


        public T GetTop() => IsEmpty ? default : this.Top.Data;
       

        public T Pop()
        {
            if (IsEmpty)
                return default;
            T result = this.Top.Data;
            this.Top = this.Top.NextNode;
            this.length--;
            return result;
        }

        public bool Push(T item)
        {
            Node temp = this.Top;
            this.Top = new Node(item, temp);
            this.length++;
            return true;
        }

        public bool MoveNext()
        {
            if (currentNode.NextNode == null)
                return false;
            currentNode = currentNode.NextNode;
            return true;
        }

        public void Reset()
        {
            this.currentNode = new Node(default, this.Top);
        }

        public void Dispose()
        {
            this.currentNode = null;
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
    }

    #endregion
}
