using System;
using System.Collections.Generic;
using System.Text;

namespace Miray_v2
{
    class Sifreleme
    {
        public static string sifrelemes(string text, int key)
        {
            char[] x = text.ToCharArray();
            string sifrenmisyazi = null;
            foreach (char item in x)
            {
                sifrenmisyazi += Convert.ToChar(item + key);
            }
            return sifrenmisyazi;

        }
        public static string cozum(string text, int key)
        {
            char[] x = text.ToCharArray();
            string cozulmusyazi = null;
            foreach (char item in x)
            {
                cozulmusyazi += Convert.ToChar(item - key);
            }
            return cozulmusyazi;
        }
    }
}
