using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DS_Viewer_Title_Changer
{
    class Program
    {
        private static string FileName = Directory.GetCurrentDirectory() + "\\english.lang";

        private static int Title_Offset = 0;
        private static int OTitle_Offset = 0x64EA;
        private static int NTitle_Offset = 0x70EA;
        private static int Title_Length = 0x3E;

        private static byte[] _defaultNameBlock = {
    0x6E, 0x00, 0x6F, 0x00, 0x6E, 0x00, 0x73, 0x00, 0x74, 0x00, 0x64, 0x00,
    0x20, 0x00, 0x33, 0x00, 0x6C, 0x00, 0x69, 0x00, 0x6E, 0x00, 0x65, 0x00,
    0x20, 0x00, 0x44, 0x00, 0x69, 0x00, 0x66, 0x00, 0x66, 0x00, 0x20, 0x00,
    0x53, 0x00, 0x69, 0x00, 0x67, 0x00, 0x6E, 0x00, 0x61, 0x00, 0x6C, 0x00,
    0x20, 0x00, 0x76, 0x00, 0x69, 0x00, 0x65, 0x00, 0x77, 0x00, 0x65, 0x00,
    0x72
};

        static void Main(string[] args)
        {

            Console.WriteLine("Select Viewer Software Version:\n\n1. n3DS_view (Old 3DS Viewer)\n2. new3DS_view(New 3DS Viewer)");

            int.TryParse(Console.ReadLine(), out int ch);
            switch (ch)
            {
                case 1:
                    Title_Offset = OTitle_Offset;
                    break;
                case 2:
                    Title_Offset = NTitle_Offset;
                    break;

                default:
                    Console.WriteLine("Invalid Choice.");
                    Console.ReadKey();
                    return;
            }

            if (!File.Exists(FileName))
            {
                Console.WriteLine("'English.lang' not Found!\n\n");
                Console.WriteLine("Please place this File in the same Folder as your Katsukity Capture Software with the 'english.lang' File!");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Current Window Title is: " + GetTittleName());

            Console.WriteLine("\nEnter your new title name: (leave empty to restore default name)");

            string titleName = Console.ReadLine();

            if (titleName == string.Empty)
            {
                Console.WriteLine("Set default title name...");
                SetDefaultName();
            }
            else
            {
                Console.WriteLine($"Set custom title name...");
                SetTitleName(titleName);
            }

            Console.WriteLine("\nDone!");
            Console.ReadLine();
        }



        static string GetTittleName()
        {
            byte[] data = File.ReadAllBytes(FileName);
            var title = Encoding.Unicode.GetString(data, Title_Offset, Title_Length);

            return title;
        }

        static void SetTitleName(string title)
        {
            byte[] new_title = Encoding.Unicode.GetBytes(title);

            if (new_title.Length > _defaultNameBlock.Length)
            {
                Console.WriteLine("Title name is to long, abort.");
                return;
            }

            while (new_title.Length != Title_Length)
                new_title = new_title.Concat(new byte[] { 0x00 }).ToArray();

            using (BinaryWriter writer = new BinaryWriter(File.Open(FileName, FileMode.Open, FileAccess.ReadWrite)))
            {
                writer.Seek(Title_Offset, SeekOrigin.Begin);
                writer.Write(new_title);
            }
        }

        static void SetDefaultName()
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(FileName, FileMode.Open, FileAccess.ReadWrite)))
            {
                writer.Seek(Title_Offset, SeekOrigin.Begin);
                writer.Write(_defaultNameBlock);
            }
        }
    }
}
