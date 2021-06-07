using System;
namespace _3DS_Viewer_Title_Changer
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(
                "Select Viewer Software Version:\n\n" +
                "1. Katsukity/Keity\n" +
                "2. Loopy");

            int.TryParse(Console.ReadLine(), out int ch);
            switch (ch)
            {
                case 1:
                    Keity.Menu();
                    break;
                case 2:
                    Loopy.Menu();
                    break;

                default:
                    Console.WriteLine("Invalid Choice.");
                    Console.ReadKey();
                    return;
            }
        }

    }
}
