using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace QkuangLibrary.DataStruct
{

    public interface IQueue<T>
    {
        /// <summary>
        /// 获取队列元素个数
        /// </summary>
        int Count { get; }
        /// <summary>
        /// 判空
        /// </summary>
        bool IsEmpty { get; }
        /// <summary>
        /// 清空队列
        /// </summary>
        void Clear();
        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool EnQueue(T item);
        /// <summary>
        /// 出队,若为空表，返回default
        /// </summary>
        /// <returns></returns>
        T DeQueue();
        /// <summary>
        /// 获取队头元素值，若为空表返回default;
        /// </summary>
        /// <returns></returns>
        T GetHead();
    }

    #region 顺序队列（已测试）

    /// <summary>
    /// 顺序队列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SeqQueue<T> : IQueue<T>, IEnumerable<T>, IEnumerator<T>
    {
        private T[] array;
        private int maxsize;

        private int head;
        private int tail;

        private bool PriorIsInsert;     //前一个元素操作是否是插入

        private int current;    //enumera使用

        public SeqQueue(int maxsize)
        {

            this.maxsize = maxsize;
            array = new T[maxsize];
            head = 0;
            tail = 0;
            PriorIsInsert = false;
        }

        public int Count => IsFull ? maxsize : (tail + maxsize - head) % maxsize;

        public bool IsEmpty => !PriorIsInsert && head == tail;

        public bool IsFull => PriorIsInsert && head == tail;

        public T Current => array[(tail + maxsize - current) % maxsize];

        object IEnumerator.Current => throw new NotImplementedException();

        public void Clear()
        {
            PriorIsInsert = false;
            tail = head;
        }

        public T DeQueue()
        {
            if (IsEmpty)
                return default;
            PriorIsInsert = false;
            head = (head + 1) % maxsize;
            return array[(head + maxsize - 1) % maxsize];

        }

        public bool EnQueue(T item)
        {
            if (IsFull)
                return false;
            PriorIsInsert = true;
            array[tail] = item;
            tail = (tail + 1) % maxsize;

            return true;

        }

        public T GetHead()
        {
            if (IsEmpty)
                return default;
            return array[head];
        }

        //************Enumera接口实现

        public IEnumerator<T> GetEnumerator()
        {
            Reset();
            return this;
        }


        public bool MoveNext()
        {
            current--;
            if (current > 0)
                return true;

            return false;

        }

        public void Reset()
        {
            current = this.Count + 1;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {

        }

    }

    #endregion

    #region 链式队列（已测试）

    /// <summary>
    /// 链式队列，不带头节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkQueue<T> : IQueue<T>, IEnumerable<T>, IEnumerator<T>
    {

        class Node
        {
            public T data;
            public Node next;
            public Node() => this.data = default;
            public Node(T item) => this.data = item;
            public Node(T item, Node next)
            {
                this.data = item;
                this.next = next;

            }
        }

        private int length;
        private Node head;
        private Node tail;

        private Node enumera;

        public LinkQueue()
        {
            this.length = 0;
            this.head = null;
            this.tail = null;

        }

        public int Count => length;

        public bool IsEmpty => this.head == null;

        public T Current => enumera.data;

        object IEnumerator.Current => throw new NotImplementedException();

        public void Clear()
        {
            this.head = null;
            this.tail = null;
            this.length = 0;
        }

        public T DeQueue()
        {
            if (IsEmpty)
                return default;

            T value = this.head.data;
            this.head = this.head.next;
            if (this.head == null)              //当队列只有一个元素时，避免内存泄漏。
                this.tail = null;

            this.length--;
            return value;
        }

        public bool EnQueue(T item)
        {
            if (IsEmpty)
            {
                this.tail = new Node(item);
                this.head = tail;
            }
            else
            {
                this.tail.next = new Node(item);
                this.tail = this.tail.next;
            }

            length++;
            return true;
        }

        public T GetHead()
        {
            if (IsEmpty)
                return default;
            return this.head.data;
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
            if (enumera.next == null)
                return false;

            enumera = enumera.next;
            return true;

        }

        public void Reset()
        {
            enumera = new Node(default, this.head);
        }

        public void Dispose()
        {
            enumera = null;
        }
    }

    #endregion



}
