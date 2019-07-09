using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

// Регулярные выражения
// Перечислители
// Текстовый адаптер файлового потока

namespace PersonalDataGenerator
{
    class Parser
    {
        string citiesFile;
        string streetsFile;
        string lastNamesFile;
        string firstNamesMaleFile;
        string firstNamesFemaleFile;

        public List<string> Cities { get; }
        public List<string> LastNames { get; }
        public List<string> Streets { get; }
        public Dictionary<char, List<string>> FirstNames { get; }

        public Parser()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            citiesFile = Path.Combine(path, @"RawData\Cities\Ru.txt");
            streetsFile = Path.Combine(path, @"RawData\Streets\Ru.txt");
            lastNamesFile = Path.Combine(path, @"RawData\LastNames\Ru.txt");
            firstNamesMaleFile = Path.Combine(path, @"RawData\FirstNames\Male\Ru.txt");
            firstNamesFemaleFile = Path.Combine(path, @"RawData\FirstNames\Female\Ru.txt");

            Cities = new List<string>(10);
            Streets = new List<string>(10);
            LastNames = new List<string>(10);
            FirstNames = new Dictionary<char, List<string>>(10);

            Parse();
        }

        private void Parse()
        {
            ParseCities();
            ParseStreets();
            ParseLastNames();
            ParseFirstNamesMale();
            ParseFistNamesFemale();
        }


        private IEnumerable<Match> GetMatch(string file, string pattern)
        {
            using (var reader = File.OpenText(file))
            {
                string str;
                while ((str = reader.ReadLine()) != null)
                {
                    var match = Regex.Match(str, pattern);
                    if (match.Success)
                    {
                        yield return match;
                    }
                }
            }
        }

        private void ParseCities()
        {
            foreach (Match m in GetMatch(citiesFile, @"(.*) \t(.*) \t"))
            {
                Cities.Add($"{m.Groups[1]}, {m.Groups[2]}");
            }
        }

        private void ParseStreets()
        {
            foreach (Match m in GetMatch(streetsFile, @"[0-9]*\s*([0-9А-Яа-я\-]*),\s*([А-Яа-я]*)\."))
            {
                Streets.Add($"{m.Groups[1]} {m.Groups[2]}");
            }
        }

        private void ParseLastNames()
        {
            foreach (Match m in GetMatch(lastNamesFile, @"<td.*>([А-Яа-я]*)</td>"))
            {
                LastNames.Add($"{m.Groups[1]}");
            }
        }

        private void ParseFirstNamesMale()
        {
            var names = new List<string>(10);
            foreach (Match m in GetMatch(firstNamesMaleFile, @"([А-Яа-я]*)"))
            {
                names.Add($"{m.Groups[1]}");
            }
            FirstNames.Add('M', names);
        }

        private void ParseFistNamesFemale()
        {
            var names = new List<string>(10);
            foreach (Match m in GetMatch(firstNamesFemaleFile, @"([А-Яа-я]*)"))
            {
                names.Add($"{m.Groups[1]}");
            }
            FirstNames.Add('F', names);
        }
    }
}