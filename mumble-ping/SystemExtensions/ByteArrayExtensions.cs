namespace System
{
    public static class ByteArrayExtensions
    {
        public static ushort ReadUInt16(this byte[] bytes, int index) => BitConverter.ToUInt16(FixEndian(bytes.AsSpan(index, sizeof(ushort))));
        public static uint ReadUInt32(this byte[] bytes, int index) => BitConverter.ToUInt32(FixEndian(bytes.AsSpan(index, sizeof(uint))));
        public static ulong ReadUInt64(this byte[] bytes, int index) => BitConverter.ToUInt64(FixEndian(bytes.AsSpan(index, sizeof(ulong))));

        private static ReadOnlySpan<byte> FixEndian(Span<byte> span) { if (BitConverter.IsLittleEndian) span.Reverse(); return span; }
    }
}
