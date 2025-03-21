namespace LINQJoin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("LINQ");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Kas sa tahad vaadata tabelit (Y/N)?");

           
            Run:
                string choice = Console.ReadLine();
                if (choice == "Y" || choice == "y")
                {
                    JoinLINQ();
                }
                else if (choice == "N" || choice == "n")
                {
                    Console.WriteLine("Ok.");
                }
                else
                {
                    Console.WriteLine("Sellist valikut pole, proovi uuesti.");
                    goto Run;
                }
        }

        static void JoinLINQ()
        {
            var innerJoin = Humans.peoples
                .Join
                (
                    Genders.genderList,
                    a => a.GenderId,
                    b => b.Id,
                    (a, b) => new
                    {
                        Name = a.Name,
                        GenderName = b.GenderName,
                    }
                );

            foreach (var item in innerJoin)
            {
                Console.WriteLine(item.Name + " - " + item.GenderName);
            }

            //Ühendab andmed mõlemast tabelist, võtab teisest tabelis inimese soo vastava ID järgi ja lisab selle esimesse tabelisse.
        }
    }
}
