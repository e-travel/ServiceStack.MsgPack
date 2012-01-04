using System;
using System.IO;
using System.Linq;
using System.Reflection;
using MsgPack;

namespace ServiceStack.WebHost.Endpoints.Formats.MessagePack
{
    public class MessagePackSerializer
    {
        private readonly MethodInfo _packMethod;
        private readonly MethodInfo _unpackMethod;
        private readonly CompiledPacker _packer;

        public MessagePackSerializer()
        {
            _packMethod = typeof(CompiledPacker)
                .GetMethods()
                .First(x =>
                    x.IsGenericMethodDefinition &&
                    x.GetParameters().Length == 2 &&
                    x.GetParameters()[0].ParameterType == typeof(Stream) &&
                    x.Name == "Pack");
            _unpackMethod = typeof (CompiledPacker)
                .GetMethods()
                .First(x =>
                       x.IsGenericMethodDefinition &&
                       x.GetParameters().Length == 1 &&
                       x.GetParameters()[0].ParameterType == typeof (Stream) &&
                       x.Name == "Unpack");

            _packer = new CompiledPacker();
        }

        public void SerializeToStream(object value, Stream stream)
        {
            if (value == null) return;
            var method = _packMethod.MakeGenericMethod(value.GetType());
            method.Invoke(_packer, new[] { stream, value });
        }

        public object DeserializeFromStream(Type type, Stream stream)
        {
            var method = _unpackMethod.MakeGenericMethod(type);
            return method.Invoke(_packer, new[] { stream });
        }
    }
}
