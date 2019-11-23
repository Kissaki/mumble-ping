using System;
using System.IO;

namespace KCode.MumblePing.Packets
{
    class PingRequest
    {
        public static PingRequest Create() => new PingRequest();

        public readonly uint RequestType = 0;
        public readonly ulong RequestId = new RandomLong().NextULong();

        private PingRequest()
        {
        }

        public byte[] ToBytes()
        {
            using var s = new MemoryStream();
            using var bw = new BinaryWriter(s);
            bw.Write(RequestType.ToBytes());
            bw.Write(RequestId.ToBytes());
            return s.ToArray();
        }
    }
}
