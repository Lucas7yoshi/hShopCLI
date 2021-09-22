using System;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using RestSharp;

namespace hShopCLI
{
    class Program
    {
        public const string version = "0.0.2";

        private static hShop shop;

        static async Task Main(string[] args)
        {
            Console.WriteLine($"hShopCLI {version} by Lucas7yoshi");
            Console.WriteLine("Loading index...");
            shop = new hShop();

            var index = await shop.GetIndex();
            while (true)
            {
                var subcategories = "Available categories:\n"
                  + string.Join("\n",
                  index.Entries.Games.Subcategories.Select(
                      x => $"{x.Key.PadRight(index.Entries.Games.Subcategories.Max(x => x.Key.Length))}   ({x.Value.DisplayName})"));

                Console.WriteLine(subcategories + "\nYou may also type \"any\" to search through all of them");
                var anySearch = false;
                var searchCategory = Console.ReadLine().ToLower();
                if (searchCategory == "any")
                    anySearch = true;

                Console.WriteLine();
                Console.Write("Please enter your search: ");
                var search = Console.ReadLine();
                Console.WriteLine("Searching...");
                var results = await shop.Search(search);
                if (anySearch)
                {
                    //results = results.Where(x => x.Category == "games").ToList();
                }
                else
                {
                    results = results.Where(x => x.Subcategory == searchCategory).ToList(); // && x.Category == "games"
                }

                if (results.Count > 4)
                    Console.Clear();

                List<string> searchResults = new List<string>();

                for (int i = 0; i < results.Count; i++)
                {
                    var g = results[i];
                    searchResults.Add($"[{i}] [{g.Category} - {g.Subcategory}] {g.Name} ({BytesToString((long)g.Size)})");
                }

                Console.WriteLine(new string('-', searchResults.Max(x => x.Length)));
                Console.WriteLine(string.Join("\n", searchResults));
                Console.WriteLine(new string('-', searchResults.Max(x => x.Length)));
                Console.Write("Enter the number(s) of what you'd like to download (Space Seperated): ");
                var selectedIndexes = new List<int>();

                foreach (var i in Console.ReadLine().Split(' '))
                {
                    selectedIndexes.Add(int.Parse(i));
                }
                Console.WriteLine($"Beginning {selectedIndexes.Count} download(s)");
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/downloads/");
                foreach (var i in selectedIndexes)
                {
                    var t = results[i];

                    var downloadUrl = await shop.RequestDownload(t.Id.ToString());

                    Console.WriteLine($"Downloading {searchResults[i]}...");
                    var d = new Downloader(downloadUrl);
                    await d.BeginDownload($"{Directory.GetCurrentDirectory()}/downloads/{t.Name} [{i}].cia");
                    Console.WriteLine("Download complete");
                }

                Console.WriteLine("Press enter to restart from the beginning...");
                Console.ReadLine();
            }

            await Task.Delay(-1);
        }

        public static string BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }
    }
}
