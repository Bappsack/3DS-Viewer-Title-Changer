using System;
using System.IO;
using System.Linq;
using System.Text;

namespace _3DS_Viewer_Title_Changer
{
    public static class Keity
    {
        private static string FileName => Directory.GetCurrentDirectory() + "\\english.lang";
        public static void Menu()
        {
            Console.Clear();

            Console.WriteLine(
              "Select Katsukity/Keity Software Version:\n\n" +
              "1. OLD 3DS (n3ds_view)\n" +
              "2. New 3DS (new3DS_view)");

            int.TryParse(Console.ReadLine(), out int ch);
            switch (ch)
            {
                case 1:
                    Offsets.Keity.Title_Offset = Offsets.Keity.OTitle_Offset;
                    break;
                case 2:
                    Offsets.Keity.Title_Offset = Offsets.Keity.NTitle_Offset;
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
            var title = Encoding.Unicode.GetString(data, Offsets.Keity.Title_Offset, Offsets.Keity.Title_Length);

            return title;
        }

        static void SetTitleName(string title)
        {
            byte[] new_title = Encoding.Unicode.GetBytes(title);

            if (new_title.Length > Offsets.Keity.DefaultTitle.Length)
            {
                Console.WriteLine("Title name is to long, abort.");
                return;
            }

            while (new_title.Length != Offsets.Keity.Title_Length)
                new_title = new_title.Concat(new byte[] { 0x00 }).ToArray();

            using (BinaryWriter writer = new BinaryWriter(File.Open(FileName, FileMode.Open, FileAccess.ReadWrite)))
            {
                writer.Seek(Offsets.Keity.Title_Offset, SeekOrigin.Begin);
                writer.Write(new_title);
            }
        }

        static void SetDefaultName()
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(FileName, FileMode.Open, FileAccess.ReadWrite)))
            {
                writer.Seek(Offsets.Keity.Title_Offset, SeekOrigin.Begin);
                writer.Write(Offsets.Keity.DefaultTitle);
            }
        }
    }
}
