using System;
using System.IO;
using System.Linq;
using System.Text;

namespace _3DS_Viewer_Title_Changer
{
    public static class Loopy
    {
        private static string FileName3DS => Directory.GetCurrentDirectory() + "\\3ds_capture.exe";
        private static string FileNameDS => Directory.GetCurrentDirectory() + "\\ds_capture.exe";
        private static string FileName = string.Empty;
        public static void Menu()
        {
            Console.Clear();

            Console.WriteLine(
              "Select Loopy Software Version:\n\n" +
              "1. 3DS (3ds_capture)\n" +
              "2. DS (ds_capture)");

            int.TryParse(Console.ReadLine(), out int ch);
            switch (ch)
            {
                case 1:
                    Offsets.Loopy.Title_Offset = Offsets.Loopy._3DS_Title_Combined_Offset;
                    Offsets.Loopy.Title_Length = Offsets.Loopy._3DS_Title_Combined_Length;
                    Offsets.Loopy.DefaultTitle = Offsets.Loopy._3DS_DefaultTitle;
                    FileName = FileName3DS;
                    break;
                case 2:
                    Offsets.Loopy.Title_Offset = Offsets.Loopy.DS_Title_Combined_Offset;
                    Offsets.Loopy.Title_Length = Offsets.Loopy.DS_Title_Combined_Length;
                    Offsets.Loopy.DefaultTitle = Offsets.Loopy.DS_DefaultTitle;
                    FileName = FileNameDS;
                    break;

                default:
                    Console.WriteLine("Invalid Choice.");
                    Console.ReadKey();
                    return;
            }


            if (!File.Exists(FileName))
            {
                Console.WriteLine($"'{Path.GetFileName(FileName)}' not Found!\n\n");
                Console.WriteLine($"Please place this File in the same Folder as your Katsukity Capture Software with the '{Path.GetFileName(FileName)}' File!");
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
            var title = Encoding.Unicode.GetString(data, Offsets.Loopy.Title_Offset, Offsets.Loopy.Title_Length);

            return title;
        }

        static void SetTitleName(string title)
        {
            byte[] new_title = Encoding.Unicode.GetBytes(title);

            if (new_title.Length > Offsets.Loopy.DefaultTitle.Length)
            {
                Console.WriteLine("Title name is to long, abort.");
                return;
            }

            while (new_title.Length != Offsets.Loopy.Title_Length)
                new_title = new_title.Concat(new byte[] { 0x00 }).ToArray();

            using (BinaryWriter writer = new BinaryWriter(File.Open(FileName, FileMode.Open, FileAccess.ReadWrite)))
            {
                writer.Seek(Offsets.Loopy.Title_Offset, SeekOrigin.Begin);
                writer.Write(new_title);
            }
        }

        static void SetDefaultName()
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(FileName, FileMode.Open, FileAccess.ReadWrite)))
            {
                writer.Seek(Offsets.Loopy.Title_Offset, SeekOrigin.Begin);
                writer.Write(Offsets.Loopy.DefaultTitle);
            }
        }
    }
}
