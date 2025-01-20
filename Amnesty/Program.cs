using System;
using System.Collections.Generic;
using System.Linq;

namespace Amnesty
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database dataBase = new Database();
            dataBase.Work();
        }
    }

    class Crime
    {
        private List<string> _crimes = new List<string>
        {
            "Murder",
            "Assault",
            "Robbery",
            "AntiGovernment",
            "Theft",
            "Homicide",
            "Rape",
            "Vandalism",
            "Fraud"
        };

        public string GetRandom() => _crimes[UserUtils.GetNumber(_crimes.Count)];
    }

    class Database
    {
        private List<Prisoner> _prisoners;

        public Database()
        {
            Fill();
        }

        public void Work()
        {
            ShowList(_prisoners);

            Console.WriteLine("\nЧтобы провести амнистию нажмите любую клавишу\n");
            Console.ReadKey();

            GrantAmnesty();

            Console.WriteLine("\nЛист после амнистии: \n");
            ShowList(_prisoners);
        }

        private void ShowList(List<Prisoner> list)
        {
            foreach (Prisoner offender in list)
                offender.ShowInfo();
        }

        private void GrantAmnesty()
        {
            string crimeType = "AntiGovernment";

            var remainingList = _prisoners.Where(prisoner => prisoner.Crime != crimeType).ToList();

            var amnestiedList = _prisoners.Where(prisoner => prisoner.Crime == crimeType).ToList();

            _prisoners = remainingList;

            Console.WriteLine("\nПопавшие под амнистию: \n");
            ShowList(amnestiedList);
        }

        private void Fill()
        {
            _prisoners = new List<Prisoner>()
                {
                    new Prisoner("Davidoff"),
                    new Prisoner("Marco"),
                    new Prisoner("Michael"),
                    new Prisoner("Garcia"),
                    new Prisoner("Janette"),
                    new Prisoner("Nate"),
                    new Prisoner("Nah"),
                    new Prisoner("Lusi"),
                    new Prisoner("Leam"),
                    new Prisoner("Luca"),
                    new Prisoner("Miller"),
                    new Prisoner("Negan"),
                    new Prisoner("Silco"),
                    new Prisoner("Sevika"),
                    new Prisoner("Vander"),
                    new Prisoner("Jinx"),
                    new Prisoner("Vi"),

                };
        }
    }

    class Prisoner
    {
        public Prisoner(string name)
        {
            Name = name;
            Crime = GetRandomCrime();
        }

        public string Name { get; private set; }

        public string Crime { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя - {Name}. Преступление: {Crime}.");
        }

        private string GetRandomCrime()
        {
            Crime crimes = new Crime();
            return crimes.GetRandom();
        }
    }

    static class UserUtils
    {
        private static Random s_random = new Random();
        private static bool[] s_bools = new bool[] { true, false };

        public static int GetNumber(int maxValue) => s_random.Next(maxValue);

        public static int GetNumber(int minValue, int maxValue) => s_random.Next(minValue, maxValue);
    }
}
