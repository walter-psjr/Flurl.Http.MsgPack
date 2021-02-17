using MessagePack;
using MessagePack.Resolvers;
using System.IO;

namespace Flurl.Http.MsgPack
{
    public class MsgPackSerializer
    {
        private readonly MsgPackSettings _msgPackSettings;

        public MsgPackSerializer(MsgPackSettings msgPackSettings)
        {
            _msgPackSettings = msgPackSettings;
        }

        public byte[] Serialize(object data)
        {
            var serializerOptions = GetMessagePackSerializerOptions();

            if (data != null)
            {
                var type = data.GetType();

                return MessagePackSerializer.Serialize(type, data, serializerOptions);
            }

            return MessagePackSerializer.Serialize(data, serializerOptions);
        }

        public T Deserialize<T>(Stream stream)
        {
            var serializerOptions = GetMessagePackSerializerOptions();

            return MessagePackSerializer.Deserialize<T>(stream, serializerOptions);
        }

        private MessagePackSerializerOptions GetMessagePackSerializerOptions()
        {
            var serializerOptions = MessagePackSerializerOptions.Standard;

            if (_msgPackSettings.UseLZ4Compression)
                serializerOptions = serializerOptions.WithCompression(MessagePackCompression.Lz4Block);

            if (!_msgPackSettings.EnableCustomResolver)
                serializerOptions = serializerOptions.WithResolver(TypelessContractlessStandardResolver.Instance);

            return serializerOptions;
        }
    }
}