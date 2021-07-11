namespace AD.FunctionalExtensions
{
    public readonly struct Unit
    {
        static readonly Unit value = default;

        public static ref readonly Unit Value => ref value;

        public override int GetHashCode() => 0;

        public override bool Equals(object? obj) =>
            obj switch
            {
                Unit => true,
                _ => false
            };

        public override string ToString() => "()";

#pragma warning disable IDE0060 // Remove unused parameter
        public static bool operator ==(in Unit a, in Unit b) => true;
        public static bool operator !=(in Unit a, in Unit b) => false;
#pragma warning restore IDE0060 // Remove unused parameter
    }
}
