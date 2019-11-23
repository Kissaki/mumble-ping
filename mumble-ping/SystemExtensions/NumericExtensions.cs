namespace System
{
    public static class NumericExtensions
    {
        public static byte[] ToBytes(this byte value) => BitConverter.GetBytes(value).AsSpan().FixEndian().ToArray();
        public static byte[] ToBytes(this ushort value) => BitConverter.GetBytes(value).AsSpan().FixEndian().ToArray();
        public static byte[] ToBytes(this uint value) => BitConverter.GetBytes(value).AsSpan().FixEndian().ToArray();
        public static byte[] ToBytes(this ulong value) => BitConverter.GetBytes(value).AsSpan().FixEndian().ToArray();
    }
}
