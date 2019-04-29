using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;

namespace SubtitleTimeshift.Tests
{
    [TestClass]
    public class ShifterTest
    {
        [TestMethod]
        async public Task TestShiftSubtitle()
        {
            var inputPath = @"C:\Users\Fabio\source\repos\graph\SubtitleTimeshift.Tests\The.Matrix.1999.BluRay.720p.Malay.srt";
            var outputPath = @"C:\Users\Fabio\source\repos\graph\SubtitleTimeshift.Tests\The.Matrix.1999.BluRay.720p.Malay - Copy.srt";
            var assertPath = @"C:\Users\Fabio\source\repos\graph\SubtitleTimeshift.Tests\The.Matrix.1999.BluRay.720p.Malay - Assert.srt";

            var timeSpan = TimeSpan.FromMilliseconds(123.0);
            var encoding = System.Text.Encoding.UTF8;

            using (var inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            using (var outputStream = new FileStream(outputPath, FileMode.Truncate, FileAccess.ReadWrite))
            {
                await Shifter.Shift(inputStream, outputStream, timeSpan, encoding);
            }

            using (var outputStream = new FileStream(outputPath, FileMode.Open, FileAccess.Read))
            using (var outputReader = new StreamReader(outputPath, encoding, true))
            using (var assertStream = new FileStream(assertPath, FileMode.Open, FileAccess.Read))
            using (var assertReader = new StreamReader(assertStream, encoding, true))
            {
                var assertLine = default(string);
                var outputLine = default(string);

                while(null != (assertLine = await assertReader.ReadLineAsync()))
                {
                    Assert.IsFalse(outputReader.EndOfStream);
                    outputLine = await outputReader.ReadLineAsync();
                    Assert.AreEqual(assertLine, outputLine);
                }
            }
        }
    }
}
