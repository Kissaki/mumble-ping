namespace System
{
    public class RandomLong
    {
        private readonly Random _r = new Random();

        public ulong NextULong() => (((ulong)_r.Next()) << 32) | ((uint)_r.Next());
    }
}
