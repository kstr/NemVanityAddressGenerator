using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NemApi;

namespace NemVanityAddressGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var vanitystring = "";
            var tokenData = new byte[32];

            string privateKey = "";
            string address = "";
            double counter = 0;
            byte network = 0x68;

            var kickofftime = DateTime.Now;

            for (var x = 0; x < args.Count(); x++)
            {
                switch (args[x].Trim().ToUpper())
                {
                    case "/S":
                        vanitystring = args[x + 1];
                        break;
                    case "/T":
                        network = 98;
                        break;
                }
            }

            Console.WriteLine("Welcome to Koen's (Vanity) NEM address Generator.");
            Console.WriteLine("Use the /S switch and your vanity word.");
            Console.WriteLine("Use the /T for a testnetwork address.");
            Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =");
            Console.WriteLine("");


            do
            {
                counter++;
                RandomNumberGenerator.Create().GetBytes(tokenData);
                privateKey = ByteArrayToString(tokenData);
                address = AddressEncoding.ToEncoded(network, new PublicKey(new PrivateKey(privateKey)));

                if (counter % 10 == 0)
                {
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write("Attempt: " + counter);
                }

            } while (!(address.EndsWith(vanitystring)));


            var duration = (DateTime.Now - kickofftime).Seconds;
            Console.WriteLine("");
            Console.WriteLine("Found a match in "+ duration +" seconds.  Speed was "+ (counter/ duration).ToString("0.00") +" attempts per second.");
            Console.WriteLine("");
            Console.WriteLine("Your adress:");
            Console.WriteLine(address);
            Console.WriteLine("Your private key");
            Console.WriteLine(privateKey);

            Console.WriteLine("");
            Console.WriteLine("Thank you, come again.");
            Console.WriteLine("");

        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}