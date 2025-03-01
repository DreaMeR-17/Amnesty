using System;
using System.Collections.Generic;
using System.Linq;

namespace Amnesty
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            database.Work();
        }
    }

    class Crime
    {
        public string Name { get; }

        public Crime(string name)
        {
            Name = name;
        }
    }

    class CrimeRepository
    {
        private List<Crime> _crimes;

        public CrimeRepository()
        {
            _crimes = new List<Crime>
            {
                new Crime("Murder"),
                new Crime("Assault"),
                new Crime("Robbery"),
                new Crime("AntiGovernment"),
                new Crime("Theft"),
                new Crime("Homicide"),
                new Crime("Rape"),
                new Crime("Vandalism"),
                new Crime("Fraud")
            };
        }

        public Crime GetRandomCrime() => _crimes[UserUtils.GetNumber(_crimes.Count)];
    }

    class Database
    {
        private List<Prisoner> _prisoners;
        private CrimeRepository _crimeRepository;

        public Database()
        {
            _crimeRepository = new CrimeRepository();
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

            var amnestiedList = _prisoners.Where(prisoner => prisoner.Crime.Name == crimeType).ToList();
            _prisoners.RemoveAll(prisoner => prisoner.Crime.Name == crimeType);

            Console.WriteLine("\nПопавшие под амнистию: \n");
            ShowList(amnestiedList);
        }

        private void Fill()
        {
            List<string> names = new List<string>
            {
                "Davidoff", "Marco", "Michael", "Garcia", "Janette",
                "Nate", "Nah", "Lusi", "Leam", "Luca",
                "Miller", "Negan", "Silco", "Sevika", "Vander",
                "Jinx", "Vi"
            };

            _prisoners = names.Select(name => new Prisoner(name, _crimeRepository.GetRandomCrime())).ToList();
        }
    }

    class Prisoner
    {
        public string Name { get; private set; }
        public Crime Crime { get; private set; }

        public Prisoner(string name, Crime crime)
        {
            Name = name;
            Crime = crime;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя - {Name}. Преступление: {Crime.Name}.");
        }
    }

    static class UserUtils
    {
        private static Random s_random = new Random();

        public static int GetNumber(int maxValue) => 
                          s_random.Next(maxValue);
    }
}

