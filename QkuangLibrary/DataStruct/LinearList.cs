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
        /// 附加操作
        /// </summary>
        /// <param name="item"></param>
        void Append(T item);
        /// <summary>
        /// 插入操作
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

        private T[] array;
        private int currentIndex;       //当前最后一个元素索引
        private int currentLength;      //当前容器的大小
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

        public void Dispose()
        {
            //Console.WriteLine("清理Dispose");
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

}
