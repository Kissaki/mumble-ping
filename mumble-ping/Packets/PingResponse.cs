using System;
using System.IO;

namespace KCode.MumblePing.Packets
{
    [Serializable]
    class PingResponse
    {
        public static PingResponse Parse(byte[] bytes)
        {
            return new PingResponse
            {
                VersionMajor = bytes.ReadUInt16(0),
                VersionMinor = bytes[2],
                VersionPatch = bytes[3],
                RequestId = bytes.ReadUInt64(4),
                ConnectedUserCount = bytes.ReadUInt32(12),
                MaxUserCount = bytes.ReadUInt32(16),
                AllowedBandwith = bytes.ReadUInt32(20),
            };
        }

        public ushort VersionMajor;
        public byte VersionMinor;
        public byte VersionPatch;
        public string Version => $"{VersionMajor}.{VersionMinor}.{VersionPatch}";
        public ulong RequestId;
        public uint ConnectedUserCount;
        public uint MaxUserCount;
        /// <summary>
        /// Per-user upper bandwidth limit, in bits per second (bps)
        /// </summary>
        public uint AllowedBandwith;

        private PingResponse()
        {
        }

        public override string ToString() => $"Version {Version}, Users/Slots: {ConnectedUserCount}/{MaxUserCount}, Bandwidth: {AllowedBandwith} bps";
        public string ToStringFull() => $"Version {Version}, RequestId: {RequestId}, Users/Slots: {ConnectedUserCount}/{MaxUserCount}, Bandwidth: {AllowedBandwith} bps";

        public byte[] ToBytes()
        {
            using var s = new MemoryStream();
            using var bw = new BinaryWriter(s);
            bw.Write(VersionMajor.ToBytes());
            bw.Write(VersionMinor.ToBytes());
            bw.Write(VersionPatch.ToBytes());
            bw.Write(RequestId.ToBytes());
            bw.Write(ConnectedUserCount.ToBytes());
            bw.Write(MaxUserCount.ToBytes());
            bw.Write(AllowedBandwith.ToBytes());
            return s.ToArray();
        }

        public string ToStringBytes()
        {
            return "PingRequest: " + string.Join("-", ToBytes());
        }
    }
}
