namespace CoreOSC.Types
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class StringConverter : IConverter<string>
    {
        public IEnumerable<DWord> Deserialize(IEnumerable<DWord> dWords, out string value)
        {
            if (!dWords.Any())
            {
                value = "";
                return dWords;
            }

            var next = new string(Encoding.UTF8.GetChars(dWords.First().Bytes)) // not tested
                .Replace("\0", string.Empty);
            if (dWords.First().Bytes.Any(b => b == 0))
            {
                // Terminator found
                value = next;
                return dWords.Skip(1);
            }
            else
            {
                var nextDWords = Deserialize(dWords.Skip(1), out string nextValue);
                value = next + nextValue;
                return nextDWords;
            }
        }

        public IEnumerable<DWord> Serialize(string value)
        {
            var utf8Bytes = Encoding.UTF8.GetBytes(value);
            return Serialize(utf8Bytes);
        }

        private IEnumerable<DWord> Serialize(IEnumerable<byte> utf8Bytes)
        {
            var takenBytes = utf8Bytes.Take(4);
            var dWord = new DWord(takenBytes);

            if (takenBytes.Count() < 4)
            {
                return new[] { dWord };
            }
            else
            {
                var nextChars = utf8Bytes.Skip(4);
                return new[] { dWord }.Concat(Serialize(nextChars));
            }
        }
    }
}