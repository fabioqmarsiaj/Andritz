using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SubtitleTimeshift
{
    public class Shifter
    {

        async static public Task Shift(Stream input, Stream output, TimeSpan timeSpan, Encoding encoding, int bufferSize = 1024, bool leaveOpen = false)
        {
            var replacedGroups = "";
            RegexPattern NewRegex = new RegexPattern();
            var filePattern = NewRegex.GetRegexPattern();

            using (StreamReader readFile = new StreamReader(input, encoding, leaveOpen, bufferSize))
            using (StreamWriter replacedFile = new StreamWriter(output, encoding, bufferSize, leaveOpen))
            {
                

                await replacedFile.WriteLineAsync(
                        filePattern.Replace(readFile.ReadToEnd(), delegate (Match m)
                        {
                            var sequence = m.Groups["sequence"].Value;
                            var speechBeginningTime = m.Groups["speechBeginningTime"].Value;
                            var speechEndTime = m.Groups["speechEndTime"].Value;
                            var speechBeginningTimeFinal = DateTime.Parse(speechBeginningTime.Replace(",", ".")).Add(timeSpan);
                            var speechEndTimeFinal = DateTime.Parse(speechEndTime.Replace(",", ".")).Add(timeSpan);
                            var allValues = m.Value;

                            replacedGroups = allValues.Replace(
                               string.Format("{0}\r\n{1} --> {2}\r\n",
                                   sequence,
                                   speechBeginningTime,
                                   speechEndTime),
                               string.Format("{0}\r\n{1:HH\\:mm\\:ss\\.fff} --> {2:HH\\:mm\\:ss\\.fff}\r\n",
                                   sequence,
                                   speechBeginningTimeFinal,
                                   speechEndTimeFinal));

                            return replacedGroups;
                        }));
            }
        }
    }
}
