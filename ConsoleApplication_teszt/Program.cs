using System.IO;

namespace HelloWorld
{
    public class Program
    {
        public static string AbsPath { get; set; }
        public static string Prefix { get; set; }
        //public static int PadLength { get; set; }
        public static int RemainingChars { get; set; }

        static void Main(string[] args)
        {
            bool ok = false;
            string input;
            do {
                Console.Write("Abszolút elérési útvonal: ");
                input = Console.ReadLine() ?? "";
                if (input == "") {
                    Console.Write("Nem lehet üres!");
                } else if (input.Equals(ConsoleKey.Escape)) {
                    return;
                } else if (!Directory.Exists(input)) {
                    Console.Write("Nem létező útvonal!");
                } else {
                    AbsPath = input;
                    ok = true;
                }
            } while (!ok);
            
            ok = false;
            do {
                Console.Write("Fájlnév új prefixe (default: üres): ");
                input = Console.ReadLine() ?? "";
                if (input.Equals(ConsoleKey.Escape)) {
                    return;
                } else {
                    Prefix = input ?? "";
                    ok = true;
                }
            } while (!ok);

            ok = false;
            do {
                Console.Write("Fájlnév utolsó hány karaktere ne változzon (default: 0): ");
                input = Console.ReadLine() ?? "0";
                if (!int.TryParse(input, out int remainingChars)) {
                    Console.Write("Érvénytelen szám!");
                } else if (input.Equals(ConsoleKey.Escape)) {
                    return;
                } else {
                    RemainingChars = remainingChars;
                    ok = true;
                }
            } while (!ok);

            // ok = false;
            // do {
            //     Console.Write("Digitek hossza (default: 2): ");
            //     string padLength = Console.ReadLine() ?? "2";
            //     if (!int.TryParse(input, out int padlength)) {
            //         Console.Write("Érvénytelen szám!");
            //     } else if (input.Equals(ConsoleKey.Escape)) {
            //         return;
            //     } else {
            //         PadLength = padlength;
            //         ok = true;
            //     }
            // } while (!ok);

            Console.WriteLine("Elérési útvonal: " + AbsPath);
            Console.WriteLine("Fájlnév új prefixe: " + Prefix);
            Console.WriteLine("Változatlan karakterek száma: " + RemainingChars);
            //Console.WriteLine("Digitek hossza: " + PadLength);

            ConsoleKey response = ConsoleKey.Escape;
            do {
                Console.Write("OK? [i/n] ");
                response = Console.ReadKey(false).Key;
            } while (response != ConsoleKey.I && response != ConsoleKey.N);

            //var filestream = File.Open("D:\\Korean\\Sejong\\Sejong Korean Conversation\\3", FileMode.Open);
            var files = Directory.GetFiles(AbsPath);
            //string first_filename = Path.GetFileName(files.First());
            //string to_replace = first_filename.Substring(0, first_filename.IndexOf("Tr"));
            int i = 1;
            var list = files.ToList();
            list.Sort();
            foreach (string filepath in list) {
                int ext_length = Path.GetExtension(filepath).Length;
                if (RemainingChars.Equals(0)) {
                    var digits = files.Count().ToString().Length;
                    File.Move(filepath, filepath.Replace(filepath.Substring(0, filepath.Length - ext_length), AbsPath + "\\" + Prefix + (i++).ToString().PadLeft(digits, '0')));
                } else {
                    File.Move(filepath, filepath.Replace(filepath.Substring(0, filepath.Length - ext_length - RemainingChars), AbsPath + "\\" + Prefix));  
                }
            }
            Console.WriteLine("Done:)");
        }
    }
}