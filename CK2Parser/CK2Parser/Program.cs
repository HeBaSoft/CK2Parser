using CK2Parser.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Parser {

    class Program {

        static void Main(string[] args) {

            dynamic data = new CK2File(@"C:\Users\Filipsi\Desktop\835_07_26.ck2").Read();

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

            Console.ReadKey();

        }

    }

}
