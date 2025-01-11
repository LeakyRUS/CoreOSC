namespace CoreOSC.Types
{
    using System.Collections.Generic;
    using System.Linq;

    public class BlobConverter : IConverter<byte[]>
    {
        private readonly BytesConverter bytesConverter = new BytesConverter();
        private readonly IntConverter intConverter = new IntConverter();

        public IEnumerable<DWord> Deserialize(IEnumerable<DWord> dWords, out byte[] value)
        {
            intConverter.Deserialize(dWords.Take(1), out int length);
            bytesConverter.Deserialize(dWords.Skip(1).Take((length + 3) / 4), out IEnumerable<byte> paddedValue);
            value = paddedValue.Take(length).ToArray();
            return dWords.Skip(1 + (length + 3) / 4);
        }

        public IEnumerable<DWord> Serialize(byte[] value)
        {
            return intConverter.Serialize(value.Count())
                .Concat(bytesConverter.Serialize(value));
        }
    }
}