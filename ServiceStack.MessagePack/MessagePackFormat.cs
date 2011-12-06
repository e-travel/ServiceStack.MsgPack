using System;
using System.IO;
using System.Linq;
using System.Reflection;
using MsgPack;
using ServiceStack.ServiceHost;

namespace ServiceStack.WebHost.Endpoints.Formats.MessagePack
{
    /// <summary>
    /// Format methods for MessagePack content type
    /// </summary>
    public class MessagePackFormat
    {
        public const string ContentType = "application/x-msgpack";

        /// <summary>
        /// Initializes and register serializers
        /// </summary>
        /// <param name="appHost">The app host object to register with.</param>
        public static void Register(IAppHost appHost)
        {
            var serializer = new MessagePackSerializer();

            appHost.ContentTypeFilters.Register(ContentType,
                (requestContext, response, stream) => serializer.SerializeToStream(response, stream),
                serializer.DeserializeFromStream);
        }
    }
}
