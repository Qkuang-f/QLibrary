using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibray
{

    public delegate T MyDelegate< T> (int a);

    public class Covariant
    {
        public void Test()
        {
            List<Animal> ani = new List<Animal>();
           
            IMyList<Animal> cat2 = new SelfList<Cat>();
        }
    }

    public interface IMyList<out T>
    {
         
    }
    public class SelfList<T> : IMyList<T>
    {
        public void Fun1(T va)
        {

        }
    }

    /// <summary>
    /// 协变研究
    /// </summary>
    public class Animal
    {

    }

    public class Cat:Animal
    {

    }

    public interface InterA1
    {
        
    }
    public interface interA2<T> : InterA1
    {

    }
}
