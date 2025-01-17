namespace M_Koer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sisesta esimene arv");
            string number1 = Console.ReadLine();
            int nr1 = int.Parse(number1);

            Console.WriteLine("Sisesta teine arv");
            string number2 = Console.ReadLine();
            int nr2 = int.Parse(number2);

            Console.WriteLine("Sisesta tehe ( +, -, *, / )");
            string mark = Console.ReadLine();

            if (mark == "+")
            {
                Console.WriteLine(nr1 + nr2);
            }
            else if (mark == "-")
            {
                Console.WriteLine(nr1 - nr2);
            }
            else if (mark == "*")
            {
                Console.WriteLine(nr1 * nr2);
            }
            else
            {
                Console.WriteLine(nr1 / nr2);
            }
        }
    }
}
