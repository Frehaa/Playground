namespace Playground.SlidingPuzzle
{
    public static class Util
    {
        public static bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        public static bool IsOdd(int number)
        {
            return !IsEven(number);
        }
    }
}
