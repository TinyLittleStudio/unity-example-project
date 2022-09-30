using UnityEngine;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public static class DirectionUtils
    {
        public static Vector2 ToVector(this Direction direction)
        {
            switch (direction)
            {
                case Direction.EAST:
                    return new Vector2(+1.0f, 0.0f);

                case Direction.NORTH:
                    return new Vector2(0.0f, +1.0f);

                case Direction.SOUTH:
                    return new Vector2(0.0f, -1.0f);

                case Direction.WEST:
                    return new Vector2(-1.0f, 0.0f);

                default:
                    break;
            }

            return Vector2.zero;
        }
    }
}
