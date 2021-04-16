using System;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Net;


namespace hShopCLI
{
    class Program
    {
        public const string version = "0.0.1";

        private hShop shop;

        static void Main(string[] args)
        {
            //escape the static async hell
            new Program().Run().GetAwaiter().GetResult();
        }

        public async Task Run()
        {
            Console.WriteLine($"hShopCLI {version} by Lucas7yoshi");
            Console.WriteLine("Loading index...");
            shop = new();

            var index = await shop.GetIndex();
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

            await Task.Delay(-1);
        }

        private ProgressBar pb;

        private void DownloadTest()
        {
            var wc = new WebClient();
            pb = new ProgressBar();
            wc.DownloadProgressChanged += Wc_DownloadProgressChanged;
            wc.DownloadFileAsync(new Uri("https://download4.erista.me/content/9064?token=0050f26177d4d0d40c721fa82ae24179"), "deleteme.bin");
        }

        private void Wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            pb.Report((double)e.ProgressPercentage / 100);
        }
    }
}
