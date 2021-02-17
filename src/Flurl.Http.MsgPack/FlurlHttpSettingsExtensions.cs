using System;
using Flurl.Http.Configuration;

namespace Flurl.Http.MsgPack
{
    public static class FlurlHttpSettingsExtensions
    {
        private static MsgPackSettings _msgPackSettings = new MsgPackSettings();
        private static readonly Lazy<MsgPackSerializer> _msgPackSerializer = new Lazy<MsgPackSerializer>(() => new MsgPackSerializer(_msgPackSettings));

        public static MsgPackSerializer MsgPackSerializer(this FlurlHttpSettings flurlHttpSettings)
        {
            return _msgPackSerializer.Value;
        }

        public static MsgPackSerializer MsgPackSerializer(this FlurlHttpSettings flurlHttpSettings, MsgPackSettings msgPackSettings)
        {
            _msgPackSettings = msgPackSettings;

            return new MsgPackSerializer(msgPackSettings);
        }
    }
}