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

        static void Main(string[] args) {

            string Resources = Path.Combine(Assembly.GetExecutingAssembly().Location, "../../../..", "Resources");

            CK2File inFile = new CK2File(Path.Combine(Resources, "nested.ck2"));

            new CK2File(Path.Combine(Resources, "nested_out.ck2"))
                .Write(inFile.Read());


            /*
            Console.WriteLine(
                data.version
            );

            Console.WriteLine(
                data.player.type
            );

            Console.WriteLine(
                data.dyn_title[1].title
            );

            Console.WriteLine(
                data.dynasties.Get("10290907").culture
            );

            Console.WriteLine(
                data.disease.disease_small_pox.active
            );
            */

            Console.ReadKey();

        }

    }

}
