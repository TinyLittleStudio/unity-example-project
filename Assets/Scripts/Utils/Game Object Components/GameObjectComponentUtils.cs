using UnityEngine;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public static class GameObjectComponentUtils
    {
        public static bool IsEnabled(this Component component)
        {
            if (component is not null)
            {
                if (component.gameObject is not null)
                {
                    return component.gameObject.activeSelf;
                }
            }

            return false;
        }

        public static bool IsEnabled(this Component component, bool isEnabled)
        {
            if (component is not null)
            {
                if (component.gameObject is not null)
                {
                    if (component.gameObject.activeSelf != isEnabled)
                    {
                        component.gameObject.SetActive(isEnabled);
                    }

                    return component.gameObject.activeSelf;
                }
            }

            return false;
        }
    }
}
