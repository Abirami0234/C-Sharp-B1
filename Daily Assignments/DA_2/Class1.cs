using System;


namespace Assignment_1_Inheritance_
{

    class BaseClass
    {
        public virtual void Show()
        {
            Console.WriteLine("Base Class Method");
        }
    }

    class DerivedClass : BaseClass
    {
        public sealed override void Show()
        {
            Console.WriteLine("Sealed Method in Derived Class");
        }
    }

    
    class Program
    {
        static void Main()
        {
            DerivedClass obj = new DerivedClass();
            obj.Show(); 
        }
    }
}
