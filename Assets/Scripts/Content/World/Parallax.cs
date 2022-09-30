using UnityEngine;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public class Parallax
    {
        private const int LAYER_COUNT = 4;
        private const int LAYER_DEPTH = 9;

        private float x;
        private float y;

        private ParallaxObject[,] parallaxObjects;

        public Parallax()
        {
            this.x = 0.0f;
            this.y = 0.0f;

            this.parallaxObjects = new ParallaxObject[Parallax.LAYER_COUNT, Parallax.LAYER_DEPTH];

            for (int i = 0; i < Parallax.LAYER_COUNT; i++)
            {
                for (int j = 0; j < Parallax.LAYER_DEPTH; j++)
                {
                    if (ManagerUtils.Instantiate<ParallaxObject>($"Content/World/LAYER_{j}", out ParallaxObject parallaxObject))
                    {
                        parallaxObjects[i, j] = parallaxObject;
                    }
                }
            }
        }

        public void Update()
        {
            Update(x, y);
        }

        public void Update(float x, float y)
        {
            if (parallaxObjects == null)
            {
                return;
            }

            if (parallaxObjects != null)
            {
                for (int i = 0; i < Parallax.LAYER_COUNT; i++)
                {
                    for (int j = 0; j < Parallax.LAYER_DEPTH; j++)
                    {
                        ParallaxObject parallaxObject = parallaxObjects[i, j];

                        if (parallaxObject == null)
                        {
                            continue;
                        }

                        if (parallaxObject.SpriteRenderer == null)
                        {
                            continue;
                        }

                        if (parallaxObject.SpriteRenderer.sprite == null)
                        {
                            continue;
                        }

                        Vector2 velocity = parallaxObject.GetLayer().ToVelocity();

                        float w = parallaxObject.SpriteRenderer.sprite.bounds.size.x;
                        float h = parallaxObject.SpriteRenderer.sprite.bounds.size.y;

                        float outsetX = Mathf.RoundToInt(x / w) * w;
                        float outsetY = Mathf.RoundToInt(y / h) * h;

                        float offsetX = -(x * velocity.x);
                        float offsetY = -(y * velocity.y);

                        offsetX = offsetX % w;
                        offsetY = offsetY % h;

                        parallaxObject.transform.position = new Vector3(
                            outsetX + offsetX - (i * w) + ((Parallax.LAYER_COUNT / 2.0f - 0.5f) * w),
                            outsetY + offsetY,
                            0 - i);
                    }
                }
            }

            this.x = x;
            this.y = y;
        }

        public float GetX()
        {
            return this.x;
        }

        public float SetX(float x)
        {
            return this.x = x;
        }

        public float GetY()
        {
            return this.y;
        }

        public float SetY(float y)
        {
            return this.y = y;
        }

        public override string ToString() => $"Parallax ()";
    }
}
