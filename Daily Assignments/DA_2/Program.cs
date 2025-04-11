namespace Assignment_1_Inheritance
{
    using System;

    /*class Employee
    {
        private int empId;
        private string name;
        private string dob;
        private double salary;
        public Employee(int empId, string name, string dob, double salary)
        {
            this.empId = empId;
            this.name = name;
            this.dob = dob;
            this.salary = salary;
        }
        public double GetSalary()
        {
            return salary;
        }
        public virtual double ComputeSalary()
        {
            return salary;
        }
        public virtual void DisplayDetails()
        {
            Console.WriteLine($"Employee ID: {empId}\nName: {name}\nDOB: {dob}\nSalary: {ComputeSalary()}");
        }
    }
    class Manager : Employee
    {
        private double onsiteAllowance;
        private double bonus;
        public Manager(int empId, string name, string dob, double salary, double onsiteAllowance, double bonus)
            : base(empId, name, dob, salary)
        {
            this.onsiteAllowance = onsiteAllowance;
            this.bonus = bonus;
        }
        public override double ComputeSalary()
        {
            return GetSalary() + onsiteAllowance + bonus;
        }

        public override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"Onsite Allowance: {onsiteAllowance}\nBonus: {bonus}\nTotal Salary: {ComputeSalary()}");
        }

    }
    public class FunctionCounter
    {

        private static int Count = 0;


        public static void Count_function()
        {
            Count++;
            Console.WriteLine($"Count_function called {Count} times.");
        }

        class Distance
        {
            public int dist;

            public static Distance operator ++(Distance a)
            {
                Distance temp = new Distance();
                temp.dist = a.dist + 1;  
                return temp;
            }
        }


    }


    class Program
    {
        static void Main()
        {
            Employee emp = new Employee(101, "John Doe", "1990-05-14", 50000);
            emp.DisplayDetails();

            Console.WriteLine("\n");

            Manager mgr = new Manager(102, "Jane Smith", "1985-07-20", 70000, 10000, 15000);
            mgr.DisplayDetails();

            Console.Write("Enter the number of times to call the function: ");
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                FunctionCounter.Count_function();
            }
        }
    }*/
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



        
