using System.IO;
using UnityEngine;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public static class CollectableUtils
    {
        public const string DIRECTORY = "Content/Collectable/";

        public static Collectable Drop(float x, float y)
        {
            float probability;

            probability = Random.Range(0.0f, 100.0f);

            if (probability >= 00.0f && probability <= 30.0f)
            {
                return CollectableUtils.Drop(x, y, CollectableType.AMMUNITION);
            }

            if (probability >= 30.0f && probability <= 70.0f)
            {
                return CollectableUtils.Drop(x, y, CollectableType.AMMUNITION_SMALL);
            }

            return null;
        }

        public static Collectable Drop(float x, float y, CollectableType collectableType)
        {
            if (ManagerUtils.Instantiate<Collectable>(Path.Combine(CollectableUtils.DIRECTORY, $"{collectableType}"), out Collectable collectable))
            {
                collectable.transform.position = new Vector2(x, y);
                collectable.IsEnabled(true);

                return collectable;
            }

            return null;
        }

        public static bool Is(this CollectableType collectableType, CollectableType collectableTypeToCompare)
        {
            return collectableType == collectableTypeToCompare;
        }
    }
}