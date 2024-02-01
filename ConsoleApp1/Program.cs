using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.IO;


namespace ConsoleApp1
{
    class Iroda
    {
        public string kod { get; set; }
        public string kezdoDatum { get; set; }
        public List<int> letszamok { get; set; }


        public Iroda(string sor)
        {
            var v = sor.Split(' ');
            kod = v[0];
            kezdoDatum = v[1];
            letszamok = new List<int>();
            for (int i = 2; i < v.Length; i++)
            {
                letszamok.Add(int.Parse(v[i]));
            }
        }
        public override string ToString()
        {
            string s = string.Empty;
            s += $"{kod,-15} {kezdoDatum}";
            for (int i = 0; i < letszamok.Count; i++)
            {
                s += letszamok[i];
                s += " ";
            }
            return s;
        }


    }
    class Program
    {
        static void Main(string[] args)
        {


            List<Iroda> irodak = new List<Iroda>();


            using StreamReader reader = new StreamReader(@"..\..\..\src\irodak.txt");
            {
                while (!reader.EndOfStream)
                {
                    irodak.Add(new Iroda(reader.ReadLine()));
                }
            }
            //var legolcsobbMonitor = monitorok.OrderBy(monitor => monitor.NettoAr).First();
            //Console.WriteLine($"Legolcsóbb monitor: {legolcsobbMonitor}");


            //8
            //var emeletek = new List<int>();


            //foreach (var item in irodak)
            //{
            //    emeletek.Add(item.letszamok.Sum());
            //}
            //Console.WriteLine($"{emeletek.Max()} a max dolgozók száma a{emeletek.IndexOf(emeletek.Max())+1}. emeleten");


            Console.WriteLine("\n8. feladat");


            var emeletek = irodak.Select(item => item.letszamok.Sum()).ToList();
            var maxDolgozokSzama = emeletek.Max();
            var maxIndex = emeletek.IndexOf(maxDolgozokSzama) + 1;


            Console.WriteLine($"{maxDolgozokSzama} a max dolgozók száma a {maxIndex}. emeleten");




            Console.WriteLine("\n9. feladat");


            //bool f9 = false;
            //string kod = string.Empty;
            //string sorszam = string.Empty;


            //foreach (var iroda in irodak)
            //{
            //    int length = iroda.letszamok.Count();
            //    for (int i = 0; i < 12; i++)
            //    {
            //        if (iroda.letszamok[i] == 9)
            //        {
            //            f9 = true;
            //            kod = iroda.kod;
            //            sorszam = (i + 1).ToString();


            //        }
            //    }
            //}
            var result = irodak
             .SelectMany(iroda => iroda.letszamok.Select((szam, index) => new { iroda, szam, index }))
             .FirstOrDefault(item => item.szam == 9);


            bool f9 = result != null;
            string kod = f9 ? result.iroda.kod : string.Empty;
            string sorszam = f9 ? (result.index + 1).ToString() : string.Empty;


            if (f9) Console.WriteLine($"\nA {kod} kóddal ellátott cég  {sorszam}.-ik irodájában dologoznak kilencen");
            else Console.WriteLine("\nNincs olyan iroda ahol 9 ember dologozik");
            //var legnagyobbbLetszam = irodak.OrderBy(x => x).First();*/


            //9
            //for (int i = 0; i < irodak.Count; i++)
            //{
            //    for (int j = 0; j < irodak[i].letszamok.Count; j++)
            //    {
            //        if (irodak[i].letszamok[j].)
            //        {


            //        }
            //    }
            //}






            Console.WriteLine("\n10. feladat");
            int sorszam2 = 0;


            foreach (var iroda in irodak)
            {
                int length = iroda.letszamok.Count();
                for (int i = 0; i < 12; i++)
                {
                    if (iroda.letszamok[i] > 5)
                    {
                        sorszam2++;


                    }
                }
            }

            Console.WriteLine($"Az 5-nél több fős irodák száma: {sorszam2}");


            // 11. feladat
            using (StreamWriter writer = new StreamWriter(@"..\..\..\src\ures_irodak.txt"))
            {
                foreach (var iroda in irodak)
                {
                    var uresIrodak = iroda.letszamok
                        .Select((szam, index) => new { szam, index })
                        .Where(item => item.szam == 0)
                        .Select(item => item.index + 1);


                    if (uresIrodak.Any())
                    {
                        writer.Write($"{iroda.kod} ");
                        writer.WriteLine(string.Join(" ", uresIrodak));
                    }
                }
            }




            // 12. feladat
            var logmeinAtlag = irodak
                .Where(iroda => iroda.kod == "LOGMEIN")
                .SelectMany(iroda => iroda.letszamok)
                .Average();


            Console.WriteLine($"\n12. feladat: A LOGMEIN cég irodáiban átlagosan {Convert.ToInt32(logmeinAtlag)} személy dolgozik.");


            using (StreamWriter writer = new StreamWriter(@"..\..\..\src\dolgozok_szama.txt"))
            {
                writer.WriteLine("13. feladat:");
                foreach (var iroda in irodak)
                {
                    writer.WriteLine($"{irodak.IndexOf(iroda) + 1} emelet: {iroda.letszamok.Sum()} dolgozó");
                }
            }


            // 14. feladat
            var osszesDolgozo = irodak.SelectMany(iroda => iroda.letszamok).Sum();
            Console.WriteLine($"\n14. feladat: Az irodaházban összesen {osszesDolgozo} dolgozó van.");


            // 15. feladat
            var elsoIrodaBerlesEve = irodak.Min(iroda => int.Parse(iroda.kezdoDatum));
            Console.WriteLine($"\n15. feladat: Az első irodabérlés éve: {elsoIrodaBerlesEve}");


            // 16. feladat
            var legutolsoIrodaBerlesEve = irodak.Max(iroda => int.Parse(iroda.kezdoDatum));
            var evszamDiff = DateTime.Now.Year - legutolsoIrodaBerlesEve;
            Console.WriteLine($"\n16. feladat: {evszamDiff} éve nem történt új irodabérlés.");




        }
    }
}
