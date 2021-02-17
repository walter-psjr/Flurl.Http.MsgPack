namespace Flurl.Http.MsgPack
{
    public class MsgPackSettings
    {
        public bool EnableCustomResolver { get; set; } = false;
        public bool UseLZ4Compression { get; set; } = false;
    }
}