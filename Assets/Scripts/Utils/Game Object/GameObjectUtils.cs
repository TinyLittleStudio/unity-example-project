using UnityEngine;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public static class GameObjectUtils
    {
        public static bool IsEnabled(this GameObject gameObject)
        {
            if (gameObject is not null)
            {
                return gameObject.activeSelf;
            }

            return false;
        }

        public static bool IsEnabled(this GameObject gameObject, bool isEnabled)
        {
            if (gameObject is not null)
            {
                if (gameObject.activeSelf != isEnabled)
                {
                    gameObject.SetActive(isEnabled);
                }

                return gameObject.activeSelf;
            }

            return false;
        }
    }
}
