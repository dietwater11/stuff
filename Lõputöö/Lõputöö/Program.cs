using System.Security.Cryptography;

namespace Lõputöö
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Kuidas soovid sorteerida?");
            Console.WriteLine("1. Sorteerida nimekirja nimede järgi");
            Console.WriteLine("2. Sorteerida nimekirja vanuse järgi");
            Console.WriteLine("3. Kirjutada .txt faili");
            Console.WriteLine("4. Printida terminali teemanti");
            Console.WriteLine("5. Ei soovi midagi, quit");
            Console.WriteLine();

            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    ThenByName();
                    break;

                case 2:
                    ThenByAge();
                    break;

                case 3:
                    TxtFile();
                    break;

                case 4:
                    PrindDiamond();
                    break;

                case 5:
                    Console.WriteLine("Ok, tsau!!");
                    break;

                default:
                    Console.WriteLine("-----------------------");
                    Console.WriteLine("Sellist valikut ei ole!");
                    Console.WriteLine("Proovi uuesti.");
                    Console.WriteLine("-----------------------");
                    Main(args);
                    break;
            }
        }


        public static void ThenByName()
        {
            var thenByResult = List.peoples
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Age);

            Console.WriteLine("ThenBy nimede järgi");
            foreach (var people in thenByResult)
            {
                Console.WriteLine(people.Name + " " + people.Age);
            }
        }

        public static void ThenByAge()
        {
            var thenByResult = List.peoples
                .OrderBy(x => x.Age)
                .ThenBy(x => x.Name);

            Console.WriteLine("ThenBy vanuse järgi");
            foreach (var people in thenByResult)
            {
                Console.WriteLine(people.Age + " " + people.Name);
            }
        }

        static void TxtFile()
        {
            string tekst = Console.ReadLine();
            string tekstpath = @"C:/Users/opilane/Desktop/tekst.txt";
            string valepath = @"C:/Users/opilane/Desktop/wrongpath/tekst.txt";
            if (tekstpath == tekstpath) // ma ei mäleta kuidas pathi kontrollida
            {
                try
                {
                    using (StreamWriter write = new StreamWriter(tekstpath, true))
                    {
                        write.WriteLine(tekst + "\n");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Mingi error");
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
            else
            {
                try
                {
                    using (StreamWriter write = new StreamWriter(valepath, true))
                    {
                        write.WriteLine(tekst + "\n");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Mingi error");
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }

        static void PrindDiamond()
        {
            int i, j, n, l;

            Console.WriteLine("Sisesta teemanti suurus::");
            n = Convert.ToInt32(Console.ReadLine());

            for (i = 0; i <= n; i++)
            {
                for (j = 1; j <= n - i; j++)
                {
                    Console.Write(" ");
                }
                for (j = 1; j <= 2 * i - 1; j++)
                {
                    Console.Write("*");
                }
                Console.Write("\n");

            }
            for (i = n - 1; i >= 0; i--)
            {
                for (j = 1; j <= n - i; j++)
                {
                    Console.Write(" ");
                }
                for (j = 1; j <= 2 * i - 1; j++)
                {
                    Console.Write("*");
                }
                Console.Write("\n");

            }
        }
    }
}
