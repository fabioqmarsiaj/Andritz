using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SubtitleTimeshift
{
    public class RegexPattern
    {
        public RegexPattern()
        {
        }
        public Regex GetRegexPattern()
        {
            Regex Pattern = new Regex(
            @"(?<sequence>\d+)\r\n(?<speechBeginningTime>\d{2}\:\d{2}\:\d{2},\d{3}) --\> (?<speechEndTime>\d{2}\:\d{2}\:\d{1,2},\d{3})\r\n(?<speech>[\s\S]*?\r\n\r\n)",
              RegexOptions.ECMAScript);

            return Pattern;
        }
    } 
    
    
}
