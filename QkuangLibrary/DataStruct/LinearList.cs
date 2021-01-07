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
        /// 求长度
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

    /// <summary>
    /// 动态实现的顺序表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SeqList<T> : IlinearList<T>
    {
        private T[] array;
        private int currentIndex;
        private int currentLength;
        public SeqList(int magnitude)
        {
            Magnitude = magnitude;
            array = new T[magnitude];
            currentLength = magnitude;
            currentIndex = 0;
        }
        /// <summary>
        /// 数量级，用于每次扩展时决定扩展的大小
        /// </summary>
        public int Magnitude { get; set; }
        public int GetLength => currentIndex;

        public bool IsEmpty { 
            get{
                if (currentIndex == 0) return true;
                return false;
            }
        }

        public void Append(T item)
        {
            
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public T Delete(int i)
        {
            throw new NotImplementedException();
        }

        public T GetElem(int i)
        {
            throw new NotImplementedException();
        }

        public void Insert(T item, int i)
        {
            throw new NotImplementedException();
        }

        public int Locate(T value)
        {
            throw new NotImplementedException();
        }

        //private T[] ExpandCapacity()
        //{
        //    T[] result = new T[currentLength + Magnitude];
        //    array.CopyTo()
        //}
    }


}
