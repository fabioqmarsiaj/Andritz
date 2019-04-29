using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SubtitleTimeshift
{
    public class Shifter
    {
        

        private static Regex unit = new Regex(
            @"(?<sequence>\d+)\r\n(?<start>\d{2}\:\d{2}\:\d{2},\d{3}) --\> (?<end>\d{2}\:\d{2}\:\d{2},\d{3})\r\n(?<text>[\s\S]*?\r\n\r\n)",
              RegexOptions.ECMAScript);

        async static public Task Shift(Stream input, Stream output, TimeSpan timeSpan, Encoding encoding, int bufferSize = 1024, bool leaveOpen = false)
        {
            int sequence = 0;


            using (StreamReader file = new StreamReader(input, encoding, leaveOpen, bufferSize))
            {
                
                using (StreamWriter destFile = new StreamWriter(output, encoding, bufferSize, leaveOpen))
                {

                    await destFile.WriteLineAsync(

                        unit.Replace(file.ReadToEnd(), delegate (Match m)
                        {
                            return m.Value.Replace(
                                string.Format("{0}\r\n{1} --> {2}\r\n",
                                    m.Groups["sequence"].Value,
                                    m.Groups["start"].Value,
                                    m.Groups["end"].Value),
                                string.Format("{0}\r\n{1:HH\\:mm\\:ss\\.fff} --> {2:HH\\:mm\\:ss\\.fff}\r\n",
                                sequence++,
                                    DateTime.Parse(m.Groups["start"].Value.Replace(",", ".")).Add(timeSpan),
                                    DateTime.Parse(m.Groups["end"].Value.Replace(",", ".")).Add(timeSpan)));
                                
                        }));                  
                }
                
            }
        }

    }
}
