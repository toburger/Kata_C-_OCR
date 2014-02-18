﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata_OCR.Library
{
    static class DigitsParser
    {
        public static Digit[] ParseDigits(string accountNumber, int length = 9)
        {
            string[] lines = accountNumber.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            return (from digitAsString in
                        from j in Enumerable.Range(0, length)
                        let lls = from line in lines
                                  select from i in Enumerable.Range(0, length)
                                         select line.Substring(i * 3, 3)
                        let lla = lls.Select(ls => ls.ToArray()).ToArray()
                        select lla[0][j] + lla[1][j] + lla[2][j]
                    select new Digit(digitAsString)).ToArray();
        }

        public static int[] GetNearestMatch(string digit)
        {
            return DigitsTable.Instance.Where(kvp => GetMatchCountOf(kvp.Key, digit, 8))
                                       .Select(kvp => kvp.Value)
                                       .ToArray();
        }

        private static bool GetMatchCountOf(string s1, string s2, int matchCount)
        {
            return GetMatches(s1, s2).Where(m => m)
                                     .Count() == matchCount;
        }

        private static IEnumerable<bool> GetMatches(string s1, string s2)
        {
            if (s1.Length != s2.Length)
                throw new ArgumentException("The two strings provided must be of the same size.");

            return s1.Zip(s2, (c1, c2) => c1 == c2);
        }
    }
}
