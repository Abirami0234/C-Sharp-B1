namespace Assignment_1_4
{
    using System;

    public class TimePeriod
    {
        private int _seconds;

        public TimePeriod(int seconds = 0)
        {
            _seconds = seconds;
        }

        // Property to get and set time in hours
        public double Hours
        {
            get { return _seconds / 3600.0; }
            set { _seconds = (int)(value * 3600); }
        }

        public override string ToString()
        {
            return string.Format("{0:F2} hours or {1} seconds", Hours, _seconds);
        }
    }
    public abstract class Furniture
    {
        public string Name { get; set; }
        public string Material { get; set; }
        public double Price { get; set; }

        public Furniture(string name, string material, double price)
        {
            Name = name;
            Material = material;
            Price = price;
        }

        public abstract void DisplayInfo();
        public abstract double CalculateCost();
    }

    // Chair subclass
    public class Chair : Furniture
    {
        public int Legs { get; set; }
        public int Armrests { get; set; }

        public Chair(string material, double price, int legs, int armrests)
            : base("Chair", material, price)
        {
            Legs = legs;
            Armrests = armrests;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Chair Material: {Material}, Legs: {Legs}, Armrests: {Armrests}, Price: {Price}");
        }

        public override double CalculateCost()
        {
            return Price + (Legs * 5) + (Armrests * 10);
        }
    }

    // Bookshelf subclass
    public class Bookshelf : Furniture
    {
        public int Shelves { get; set; }
        public int Compartments { get; set; }

        public Bookshelf(string material, double price, int shelves, int compartments)
            : base("Bookshelf", material, price)
        {
            Shelves = shelves;
            Compartments = compartments;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Bookshelf Material: {Material}, Shelves: {Shelves}, Compartments: {Compartments}, Price: {Price}");
        }

        public override double CalculateCost()
        {
            return Price + (Shelves * 15) + (Compartments * 20);
        }
    }
    // Example usage
    public class Program
    {
        public static void Main()
        {
            TimePeriod time = new TimePeriod();
            time.Hours = 5;  
            Console.WriteLine(time);  

            time = new TimePeriod(7685);  
            Console.WriteLine(time.Hours);

            Chair chair = new Chair("Wood", 50, 4, 2);
            chair.DisplayInfo();  
            Console.WriteLine("Chair Cost: " + chair.CalculateCost());  

            Bookshelf bookshelf = new Bookshelf("Metal", 100, 5, 3);
            bookshelf.DisplayInfo();  
            Console.WriteLine("Bookshelf Cost: " + bookshelf.CalculateCost());
        }
    }

}
