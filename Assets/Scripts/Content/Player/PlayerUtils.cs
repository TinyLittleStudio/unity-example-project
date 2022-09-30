using UnityEngine;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public static class PlayerUtils
    {
        public static Player Instantiate()
        {
            return PlayerUtils.Instantiate(0.0f, 0.0f);
        }

        public static Player Instantiate(float x, float y)
        {
            if (ManagerUtils.Instantiate<Player>(IDs.PREFAB_ID__PLAYER, out Player player))
            {
                player.IsEnabled(false);

                player.transform.position = new Vector2(x, y + 0.025f);
                player.transform.rotation = Quaternion.identity;

                return player;
            }

            return null;
        }
    }
}
