using CK2Parser.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tests {

    class Test {

        private static readonly string Resources = Path.Combine(Assembly.GetExecutingAssembly().Location, "../../../..", "Resources");

        static void Main(string[] args) {
            Stopwatch watch = Stopwatch.StartNew();

            //ReadSimple();
            //ReadNested();
            //ReadWrapped();
            //ReadMultipleKeys();
            //ReadArray();
            //RewriteMain();
            //EditSimple();

            watch.Stop();
            Console.WriteLine("Task took {0} to finish.", TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds).ToReadableString());
            Console.ReadKey();
        }

        static void ReadSimple() {
            CK2File Ck2f = CK2File.Read(Path.Combine(Resources, "simple.ck2"));

            Console.WriteLine(Ck2f.Structure.version);
            Console.WriteLine(Ck2f.Structure.game_speed);
            Console.WriteLine(Ck2f.Structure.player_realm);
        }

        static void ReadNested() {
            CK2File Ck2f = CK2File.Read(Path.Combine(Resources, "nested.ck2"));

            Console.WriteLine(Ck2f.Structure.disease_outbreak.start_date);
            Console.WriteLine(Ck2f.Structure.disease_outbreak.disease_period.vassal_limit);
            Console.WriteLine(Ck2f.Structure.disease_outbreak.past_provinces.claim.weak);
            Console.WriteLine(Ck2f.Structure.disease_outbreak.past_provinces.claim.title.title);
        }

        static void ReadWrapped() {
            CK2File Ck2f = CK2File.Read(Path.Combine(Resources, "wrapped.ck2"));

            Console.WriteLine(Ck2f.Structure.player.wrapper.id);
            Console.WriteLine(Ck2f.Structure.wrapper.game_speed);
            Console.WriteLine(Ck2f.Structure.wrapper.mapmode);
        }

        static void ReadMultipleKeys() {
            CK2File Ck2f = CK2File.Read(Path.Combine(Resources, "multiplekeys.ck2"));

            Console.WriteLine(Ck2f.Structure.dyn_title[0].base_title);
            Console.WriteLine(Ck2f.Structure.dyn_title[2].title);
            Console.WriteLine(Ck2f.Structure.dyn_title[3].is_dynamic);
        }

        static void ReadArray() {
            CK2File Ck2f = CK2File.Read(Path.Combine(Resources, "array.ck2"));

            Console.WriteLine(string.Join(",", Ck2f.Structure.light_infantry_f));
            Console.WriteLine(string.Join(",", Ck2f.Structure.archers_f));
            Console.WriteLine(string.Join(",", Ck2f.Structure.att));
            Console.WriteLine(string.Join(",", Ck2f.Structure.traits));
        }

        static void RewriteMain() {
            CK2File
                .Read(Path.Combine(Resources, "835_07_26.ck2"))
                .SaveAs(Path.Combine(Resources, "835_07_26_rewritten.ck2"));
        }

        static void EditSimple() {
            CK2File Ck2f = CK2File.Read(Path.Combine(Resources, "simple.ck2"));

            Console.WriteLine("Before: version={0}", Ck2f.Structure.version);
            Ck2f.Structure.version = 42;
            Console.WriteLine("After : version={0}", Ck2f.Structure.version);

            Ck2f.SaveAs(Path.Combine(Resources, "simple_edit.ck2"));
        }

    }

}
