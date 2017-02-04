using CK2Parser.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tests {

    class Test {

        private static readonly string Resources = Path.Combine(Assembly.GetExecutingAssembly().Location, "../../../..", "Resources");

        static void Main(string[] args) {
            //ReadSimple();
            //ReadNested();
            //ReadWrapped();
            //ReadMultipleKeys();
            //RewriteMain();

            Console.WriteLine("Done!");
            Console.ReadKey();
        }

        static void ReadSimple() {
            CK2File inFile = new CK2File(Path.Combine(Resources, "simple.ck2"));
            dynamic data = inFile.Read();

            Console.WriteLine(data.version);
            Console.WriteLine(data.game_speed);
            Console.WriteLine(data.player_realm);
        }

        static void ReadNested() {
            CK2File inFile = new CK2File(Path.Combine(Resources, "nested.ck2"));
            dynamic data = inFile.Read();

            Console.WriteLine(data.disease_outbreak.start_date);
            Console.WriteLine(data.disease_outbreak.disease_period.vassal_limit);
            Console.WriteLine(data.disease_outbreak.past_provinces.claim.weak);
            Console.WriteLine(data.disease_outbreak.past_provinces.claim.title.title);
        }

        static void ReadWrapped() {
            CK2File inFile = new CK2File(Path.Combine(Resources, "wrapped.ck2"));
            dynamic data = inFile.Read();

            Console.WriteLine(data.player.wrapper.id);
            Console.WriteLine(data.wrapper.game_speed);
            Console.WriteLine(data.wrapper.mapmode);
        }

        static void ReadMultipleKeys() {
            CK2File inFile = new CK2File(Path.Combine(Resources, "multiplekeys.ck2"));
            dynamic data = inFile.Read();

            Console.WriteLine(data.dyn_title[0].base_title);
            Console.WriteLine(data.dyn_title[2].title);
            Console.WriteLine(data.dyn_title[3].is_dynamic);
        }

        static void RewriteMain() {
            CK2File inFile = new CK2File(Path.Combine(Resources, "835_07_26.ck2"));

            new CK2File(Path.Combine(Resources, "835_07_26_rewritten.ck2"))
                .Write(inFile.Read());
        }

    }

}
