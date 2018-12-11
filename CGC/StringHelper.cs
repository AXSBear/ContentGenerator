using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace CGC
{
    public static class StringHelper
    {
        /// <summary>
        ///     Custom formatter, fill placeholders ["parameter"] in string
        /// </summary>
        /// <param name="str">Source string with placehlders</param>
        /// <param name="keyValue">Placeholder-value mapping</param>
        /// <returns></returns>
        public static string CustomFormat(this string str, Dictionary<string,string> keyValue)
        {
            Regex reg = new Regex(@"(\[)([^]]+)(\])",RegexOptions.IgnoreCase);
            MatchCollection matchCollection = reg.Matches(str);
            
            StringBuilder resultString = new StringBuilder(str);

            int indexCorrection = 0;
            
            foreach (Match match in matchCollection)
            {
                Group replaceKey = match.Groups[2];
                string value;
                try
                {
                    keyValue.TryGetValue(replaceKey.Value, out value);
                }
                catch (Exception e)
                {
                    continue;
                }
                
                if (value == null) continue;
                
                int startindex = match.Index - indexCorrection;
                resultString.Remove(startindex, match.Length);
                resultString.Insert(startindex, value);
                indexCorrection += match.Length - value.Length;
            }
            return resultString.ToString();
        } 
    }
}