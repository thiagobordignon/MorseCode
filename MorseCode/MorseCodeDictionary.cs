using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorseCode
{
    public static class MorseCodeDictionary
    {
        public static Dictionary<String, String> morse = new Dictionary<String, String>()
        {
            {"A" , ".-"},
            {"B" , "-..."},
            {"C" , "-.-."},
            {"D" , "-.."},
            {"E" , "."},
            {"F" , "..-."},
            {"G" , "--."},
            {"H" , "...."},
            {"I" , ".."},
            {"J" , ".---"},
            {"K" , "-.-"},
            {"L" , ".-.."},
            {"M" , "--"},
            {"N" , "-."},
            {"O" , "---"},
            {"P" , ".--."},
            {"Q" , "--.-"},
            {"R" , ".-."},
            {"S" , "..."},
            {"T" , "-"},
            {"U" , "..-"},
            {"V" , "...-"},
            {"W" , ".--"},
            {"X" , "-..-"},
            {"Y" , "-.--"},
            {"Z" , "--.."},
            {"0" , "-----"},
            {"1" , ".----"},
            {"2" , "..---"},
            {"3" , "...--"},
            {"4" , "....-"},
            {"5" , "....."},
            {"6" , "-...."},
            {"7" , "--..."},
            {"8" , "---.."},
            {"9" , "----."},
        };

        public static string ConvertToMorse(String c)
        {
            if (morse.ContainsKey(c))
                return morse[c];
            return "";
        }
    }
}
