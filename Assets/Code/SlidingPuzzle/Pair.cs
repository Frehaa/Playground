namespace Playground.SlidingPuzzle
{
    public struct Pair<T>
    {
        public T item1;
        public T item2;

        public Pair(T item1, T item2)
        {
            this.item1 = item1;
            this.item2 = item2;
        }
    }

    public struct Pair<T1, T2>
    {
        public T1 item1;
        public T2 item2;

        public Pair(T1 item1, T2 item2)
        {
            this.item1 = item1;
            this.item2 = item2;
        }
    }
}
