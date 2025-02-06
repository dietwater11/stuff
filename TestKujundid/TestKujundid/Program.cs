
using System.ComponentModel.Design;
using System.Drawing;
using System.Reflection;

namespace TestKujundid
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sisesta millist kujundit soovid näha!!\n\nRing, Ruut, Teemant, Ristkülik\n\n:::::::::::::::::::: ");
            string anwser = Console.ReadLine();

            switch (anwser)
            {
                case "Ruut":
                    Console.WriteLine("Valisid ruudu.");
                    ruut();
                    break;
                case "Ring":
                    Console.WriteLine("Valisid ringi.");
                    ring();
                    break;
                case "Teemant":
                    Console.WriteLine("Valisid teemanti.");
                    teemant();
                    break;
                case "Ristkülik":
                    Console.WriteLine("Valisid ristküliku.");
                    ristkülik();
                    break;
                default:
                    Console.WriteLine(anwser + " ei ole valikus, palun vali õige kujund!");
                    break;
            }

            Console.WriteLine("\n\nKas soovid sisestada uut kujundit??\nY / N \n");
            string newshape = Console.ReadLine();
            if (newshape == "Y")
            {
                Main(args);
            }
            else
            {
                Console.WriteLine("OK, bye!!");
                Console.ReadKey();
                
            }
            
            

        }

        static void ruut()
        {
            Console.WriteLine("\nSisesta ruudu külje pikkus:  ");
            int size = int.Parse(Console.ReadLine());
            Console.WriteLine("\n");

            for (int row = 1; row <= size; row++)
            {
                for (int column = 1; column <= size; column++)
                {
                    string mark = "x ";
                    Console.Write(mark);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nRuudu ümbermõõt on " + size * 4 + " ja pindala on " + size * size + " ühikut.");
        }

        static void ring()
        {
            double radius;
            double thickness = 0.4;
            char symbol = 'x';

            do
            {
                Console.Write("\nSisesta ringi raadius: ");
                if (!double.TryParse(Console.ReadLine(), out radius) || radius <= 0)
                {
                    Console.WriteLine("Raadius peab olema positiivne number!!");
                }
            }
            while (radius <= 0);

            Console.WriteLine();
            double rIn = radius - thickness, rOut = radius + thickness;

            for (double y = radius; y >= -radius; --y)
            {
                for (double x = -radius; x < rOut; x += 0.5)
                {
                    double value = x * x + y * y;
                    if (value >= rIn * rIn && value <= rOut * rOut)
                    {
                        Console.Write(symbol);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nRingi ümbermõõt on " + radius * radius + " ja pindala on " + radius * 3.14 + " ühikut, ma ei tea ma unustasin kuidas ringi värki arvutada :))");


        }
        static void teemant()
        {
            Console.WriteLine("\nSisesta teemanti külje pikkus: ");
            int i, j, n;

            n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n");

            for (i = 0; i <= n; i++)
            {
                for (j = 1; j <= n - i; j++)
                {
                    Console.Write("  ");
                }
                for (j = 1; j <= 2 * i - 1; j++)
                {
                    Console.Write("x ");
                }
                Console.Write("\n");
            }
            Console.WriteLine("\nTeemanti ümbermõõt on " + n * 3 + " ja pindala on " + (n * n) / 2 + " ühikut. ");
        }

        static void ristkülik()
        {
            Console.WriteLine("\nSisesta ristküliku pikkus:  ");
            int pikkus = int.Parse(Console.ReadLine());
            Console.WriteLine("\nSisesta ristküliku laius:  ");
            int laius = int.Parse(Console.ReadLine());
            Console.WriteLine("\n");

            for (int row = 1; row <= pikkus; row++)
            {
                for (int column = 1; column <= laius; column++)
                {
                    string mark = "x ";
                    Console.Write(mark);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nRistküliku ümbermõõt on " + (pikkus + laius) * 2 + " ja pindala on " + pikkus * laius + " ühikut.");
        }
    }
}
