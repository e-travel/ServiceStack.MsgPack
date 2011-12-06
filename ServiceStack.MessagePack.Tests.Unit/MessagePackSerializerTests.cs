using System.IO;
using NUnit.Framework;
using ServiceStack.WebHost.Endpoints.Formats.MessagePack;

namespace ServiceStack.MessagePack.Tests.Unit
{
    [TestFixture]
    public class MessagePackSerializerTests
    {
        [Test]
        public void SerializeToStream_Works()
        {
            var ser = new MessagePackSerializer();
            var stream = new MemoryStream();
            ser.SerializeToStream(new []{"test1"},stream);
            Assert.Greater(stream.Length,0);
        }

        [Test]
        public void Deserialize_Works()
        {
            var ser = new MessagePackSerializer();
            var stream = new MemoryStream(new byte[] {145, 165, 116, 101, 115, 116, 49});
            var result = ser.DeserializeFromStream(typeof (string[]), stream);
            Assert.IsNotNull(result);
        }
    }
}
