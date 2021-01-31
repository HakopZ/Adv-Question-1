﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Advance_Quesiton_1
{
    static class Soundex
    {
        

        static char GetCode(char x)
        {
            switch (x)
            {
                case 'b':
                case 'f':
                case 'p':
                case 'v':
                    return '1';
                case 'c':
                case 'g':
                case 'j':
                case 'q':
                case 's':
                case 'x':
                case 'z':
                    return '2';
                case 'd':
                case 't':
                    return '3';
                case 'l':
                    return '4';
                case 'm':
                case 'n':
                    return '5';
                case 'r':
                    return '6'; 
            }
            return '.';
        }

        public static string GetSoundex(string word)
        {
            word = word.ToLower();
            StringBuilder soundex = new StringBuilder();
            foreach(var character in word)
            {
                if(char.IsLetter(character))
                {
                    AddCodeToSoundex(soundex, character);
                }
            }
            soundex.Replace(".", "");
            if (soundex.Length < 4)
            {
                soundex.Append(new string('0', 4 - soundex.Length));
            }
            else
            {
                soundex.Length = 4;
            }
            return soundex.ToString();
        }

        static void AddCodeToSoundex(StringBuilder soundex, char character)
        {
            if(soundex.Length == 0)
            {
                soundex.Append(character);
            }
            else
            {
                char OneLetterCode = GetCode(character);
                char LastCode = soundex[soundex.Length - 1];
                if(OneLetterCode != LastCode)
                {
                    soundex.Append(OneLetterCode);
                }
            }
            
        }
    }
}
