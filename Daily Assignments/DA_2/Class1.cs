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

    // class FurtherDerived : DerivedClass
    // {
    //     public override void Show() ❌ Error: Cannot override sealed method
    //     {
    //         Console.WriteLine("Attempting to Override Sealed Method");
    //     }
    // }

    class Program
    {
        static void Main()
        {
            DerivedClass obj = new DerivedClass();
            obj.Show();  // Output: Sealed Method in Derived Class
        }
    }
}