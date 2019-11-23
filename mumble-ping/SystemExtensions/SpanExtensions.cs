namespace System
{
    public static class SpanExtensions
    {
        public static ReadOnlySpan<byte> FixEndian(this Span<byte> span) { if (BitConverter.IsLittleEndian) span.Reverse(); return span; }
    }
}
