using UnityEngine;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public static class MathUtils
    {
        public static class Numbers
        {
            public static int GetRandom(int min, int max)
            {
                return Random.Range(min, max + 1);
            }
        }

        public static class NumbersWithDecimals
        {
            public static float GetRandom(float min, float max)
            {
                return Random.Range(min, max);
            }
        }
    }
}
