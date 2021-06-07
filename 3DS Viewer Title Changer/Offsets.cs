﻿using System;

namespace Offsets
{
    public static class Loopy
    {
        public static int Title_Offset = 0;
        public static int Title_Length = 0;
        public static byte[] DefaultTitle = Array.Empty<byte>();

        public static int _3DS_Title_Combined_Offset = 0x26E68;
        public static int _3DS_Title_Combined_Length = 0x16;
        public static byte[] _3DS_DefaultTitle ={
            0x33, 0x00, 0x44, 0x00, 0x53, 0x00, 0x20, 0x00, 0x43, 0x00, 0x61, 0x00, 0x70, 0x00, 0x74, 0x00, 0x75, 0x00, 0x72, 0x00, 0x65, 0x00
        };

        /*
        public static int _3DS_Title_Top_Offset = 0x26F50;
        public static int _3DS_Title_Top_Length = 0x1E;

        public static int _3DS_Title_Bottom_Offset = 0x26F9C;
        public static int _3DS_Title_Bottom_Length = 0x24;
        */

        public static int DS_Title_Combined_Offset = 0x22B90;
        public static int DS_Title_Combined_Length = 0x14;
        public static byte[] DS_DefaultTitle = {
            0x44, 0x00, 0x53, 0x00, 0x20, 0x00, 0x43, 0x00, 0x61, 0x00, 0x70, 0x00, 0x74, 0x00, 0x75, 0x00, 0x72, 0x00, 0x65, 0x00
        };

        /*
        public static int DS_Title_Top_Offset = 0x22C70;
        public static int DS_Title_Top_Length = 0x1B;

        public static int DS_Title_Bottom_Offset = 0x22CBC;
        public static int DS_Title_Bottom_Length = 0x21;

        public static int DS_GBA_Title_Bottom_Offset = 0x22C58;
        public static int DS_GBA_Title_Bottom_Length = 0x15;
        */
    }

    public static class Keity
    {
        public static int Title_Offset = 0;
        public static int Title_Length = 0x3E;

        public static int OTitle_Offset = 0x64EA;
        public static int NTitle_Offset = 0x70EA;

        public static byte[] DefaultTitle = {
    0x6E, 0x00, 0x6F, 0x00, 0x6E, 0x00, 0x73, 0x00, 0x74, 0x00, 0x64, 0x00,
    0x20, 0x00, 0x33, 0x00, 0x6C, 0x00, 0x69, 0x00, 0x6E, 0x00, 0x65, 0x00,
    0x20, 0x00, 0x44, 0x00, 0x69, 0x00, 0x66, 0x00, 0x66, 0x00, 0x20, 0x00,
    0x53, 0x00, 0x69, 0x00, 0x67, 0x00, 0x6E, 0x00, 0x61, 0x00, 0x6C, 0x00,
    0x20, 0x00, 0x76, 0x00, 0x69, 0x00, 0x65, 0x00, 0x77, 0x00, 0x65, 0x00,
    0x72
};
    }
}
