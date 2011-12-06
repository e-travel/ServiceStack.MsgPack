using System;
using System.Collections.Generic;
using MsgPack;
using NUnit.Framework;

namespace ServiceStack.MessagePack.Tests.Unit
{
    [TestFixture]
    public class CapabilitiesTests
    {
        [Test]
        public void String_Works()
        {
            var packer = new CompiledPacker();
            packer.Pack(new StringClass { Test = "Test" });
        }

        [Test]
        public void Int_Works()
        {
            var packer = new CompiledPacker();
            packer.Pack(new IntClass { Test = 32 });
        }

        [Test(Description = "MsgPack cannot work with .NET DateTime")]
        public void DateTime_DoesntWork()
        {
            var packer = new CompiledPacker();
            Assert.Throws<AccessViolationException>(
                () => packer.Pack(new DateTimeClass { Test = DateTime.Now }));
        }

        [Test]
        public void Guid_Works()
        {
            var packer = new CompiledPacker();
            var res = packer.Pack(new GuidClass {Test = Guid.NewGuid()});
            Assert.Greater(res.Length,0);
        }

        [Test]
        public void Dictionary_Works()
        {
            var packer = new CompiledPacker(true);
            var res = packer.Pack(new Dictionary<string,string>{{"a","b"},{"c","d"}} as IDictionary<string,string>);
            Assert.Greater(res.Length, 0);
        }

        public class StringClass
        {
            public string Test { get; set; }
        }

        public class IntClass
        {
            public int Test { get; set; }
        }

        public class DateTimeClass
        {
            public DateTime Test { get; set; }
        }

        public class GuidClass
        {
            public Guid Test { get; set; }
        }
    }
}
