using UnityEngine;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public static class LayerUtils
    {
        public const float BASE_VELOCITY = 0.05f;
        public const float BASE_VELOCITY_MULTIPLIER = 0.025f;

        public static Vector2 ToVelocity(this Layer layer)
        {
            float x = 0.0f - ((int)layer) * LayerUtils.BASE_VELOCITY_MULTIPLIER + LayerUtils.BASE_VELOCITY;
            float y = 0.025f;

            switch (layer)
            {
                case Layer.LAYER_0:
                case Layer.LAYER_1:
                    x = 0.075f;
                    y = 0.075f;
                    break;

                case Layer.LAYER_2:
                    x = 0.050f;
                    y = 0.050f;
                    break;

                case Layer.LAYER_3:
                    x = 0.000f;
                    y = 0.000f;
                    break;

                default:
                    break;
            }

            return new Vector2(x, y);
        }
    }
}
