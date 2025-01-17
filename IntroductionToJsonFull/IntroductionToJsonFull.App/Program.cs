   
﻿
using Newtonsoft.Json;

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IntroductionToJsonFull.App
{
    class Program
    {
        // step one - using the nuget package manager
        // https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-in-visual-studio 
        static void Main(string[] args) //deserialiszation basically converts one type into another type (can be anything)
        {
            //new_();
            pokemon();
            Console.WriteLine("Hello World!");

            string json = @"{""key1"":""james_example"",""key2"":""age""}";

            IDictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            foreach (string k in values.Keys)
                Console.WriteLine($"{k}: {values[k]}");


            // e.g. https://www.newtonsoft.com/json/help/html/DeserializeObject.htm
            string json2 = @"{
            'Email': 'james@example.com',
            'Active': true,
            'CreatedDate': '2021-01-20T00:00:00Z',
            'Roles': [
                'User',
                'Admin'
            ]
            }";



            Account account = JsonConvert.DeserializeObject<Account>(json2);


            Console.WriteLine(account.Email);
            // For GDP
            string txt = "TextFile2.Txt";
            StreamReader stream = new StreamReader(txt);
            string text = stream.ReadToEnd();
            IDictionary<string, double> GDP = JsonConvert.DeserializeObject<Dictionary<string, double>>(text);
            //write key along with dashes to represent int value
            Console.WriteLine("Enter 3 letter key");
            string input = Console.ReadLine();
            Console.WriteLine(GDP[input]);
            double current = 0;
            foreach (KeyValuePair<string, double> item in GDP)
            {
                if (item.Value > current)
                    current = item.Value;
            }
            Console.WriteLine($"Highest value is {current}");
            foreach (KeyValuePair<string, double> item in GDP)
            {
                Console.WriteLine(item.ToString());
                int num = Convert.ToInt32(Math.Round(item.Value, 1));
                Console.WriteLine($"Num is {num}");
                for (int i = 1; i <= num; ++i)
                {
                    Console.Write("|");
                }
                Console.ReadKey();
            }
            Console.WriteLine(GDP[input]);

            Console.ReadKey();
        }
        static void new_()
        {



            string jsonDownload;
            using (var wc = new WebClient())
            {
                jsonDownload = wc.DownloadString("http://api.worldbank.org/v2/countries/USA/indicators/NY.GDP.MKTP.CD?per_page=5000&format=json");

            }

            dynamic j = JArray.Parse(jsonDownload);

            dynamic l = j[1];
            Console.WriteLine(l.Count);
            foreach (dynamic row in l)
            {
                Console.WriteLine($"{row.date}: {row.countryiso3code}: ${row.value / 1000000000:F3}bn");
            }

            Console.ReadKey();



        }

        static void pokemon()
        {
            string Json_d;
            using (var wc = new WebClient())
            {
                Json_d = wc.DownloadString("https://raw.githubusercontent.com/Biuni/PokemonGO-Pokedex/master/pokedex.json");
            }
            dynamic j = JConstructor.Parse(Json_d);

            List<Pokemon> pokemons = new List<Pokemon>();
            Pokemon add = new Pokemon();

            foreach (dynamic row in j.pokemon)
            {
                if ($"{row.id}" != null)
                {
                    pokemons.Add(add);
                    add.ID = row.id;
                }
                if ($"{row.name}" != null)
                {
                    add.Name = row.name;
                }
                if ($"{row.type}" != null)
                {
                    add.Type = row.Type;
                }
                if ($"{row.weaknesses}" != null)
                {
                    add.Type = row.weaknesses;
                }
            }

            
            pokemons.Add(add);
            foreach(Pokemon pokemon in pokemons)
            {
                if (pokemon.Weaknesses != null && pokemon.Name != null && pokemon.Type != null)
                {
                    Console.WriteLine($"Name: {pokemon.Name} \nType: {string.Join(",", (pokemon.Type.ToObject<string[]>()))}\nWeaknesses: {string.Join(",", (pokemon.Weaknesses.ToObject<string[]>()))}");
                }
             
               
            }
        }
        public class Pokemon
        {
            string name;
            int id;
            JArray type;
            JArray weaknesses;

            public string Name { get => name; set => name = value; }
            public int ID { get => id ; set => id = value; }
            public  JArray Type { get => type; set => type = value; }
            public JArray Weaknesses { get => weaknesses; set => weaknesses = value; }

           
        }
           

        public class Account
        {
            string _email;
            bool _active;
            DateTime _created;
            IList<string> _roles;

            public string Email { get => _email; set => _email = value; }
            public bool Active { get => _active; set => _active = value; }
            public DateTime CreatedDate { get => _created; set => _created = value; }
            public IList<string> Roles { get => _roles; set => _roles = value; }

            public Account(string e, bool a, DateTime c, IList<string> r)
            {
                _email = e;
                _active = a;
                _created = c;
                _roles = r;
            }
        }
    }
}
