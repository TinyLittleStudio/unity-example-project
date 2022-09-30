using UnityEngine;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public class World
    {
        private const float SPAWN_TIMER = 1.0f;

        private const float PARALLAX_OFFSET_X = 0.0f;
        private const float PARALLAX_OFFSET_Y = 2.0f;

        private float x;
        private float y;

        private float time;
        private float timeTotal;

        private float distance;

        public World()
        {
            this.x = 0.0f;
            this.y = 0.0f;

            this.time = 0.0f;
            this.timeTotal = 0.0f;

            this.distance = 0.0f;

            Manager.GetManager().Inject(OnTick);

            Handle.GetHandle().Inject(IDs.EVENT_ID__SCENE_MENU, OnEventSceneMenu);
            Handle.GetHandle().Inject(IDs.EVENT_ID__SCENE_GAME, OnEventSceneGame);
        }

        private void OnTick(int tick)
        {
            if (!Manager.GetManager().IsGame())
            {
                return;
            }

            if (Player.Current == null)
            {
                Player.Current = PlayerUtils.Instantiate();

                Watch.NewWatch(0.25f, (int tick, bool isFinished) =>
                {
                    if (isFinished)
                    {
                        if (Player.Current != null)
                        {
                            Player.Current.IsEnabled(true);
                        }
                    }
                });
            }

            if (Player.Current != null)
            {
                if (Player.Current.IsEnabled())
                {
                    x = Player.Current.transform.position.x - World.PARALLAX_OFFSET_X;
                    y = Player.Current.transform.position.y - World.PARALLAX_OFFSET_Y;
                }
            }

            OnTickWorld(tick);
            OnTickWorldDistance(tick);
        }

        private void OnTickWorld(int tick)
        {
            time += Settings.TICK_TIME;

            if (time >= timeTotal)
            {
                float x = World.PARALLAX_OFFSET_X + this.x;
                float y = World.PARALLAX_OFFSET_Y + this.y;

                if (MathUtils.Numbers.GetRandom(0, 1) == 0)
                {
                    x = x + Camera.main.aspect * (Camera.main.orthographicSize * 2.0f);
                }
                else
                {
                    x = x - Camera.main.aspect * (Camera.main.orthographicSize * 2.0f);
                }

                int count = MathUtils.Numbers.GetRandom(0, 4);

                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (ManagerUtils.Instantiate<Agent>(IDs.PREFAB_ID__AGENT_ENEMY, out Agent agent))
                        {
                            float rx = MathUtils.NumbersWithDecimals.GetRandom(0.0f, 1.0f) - 0.5f;
                            float ry = MathUtils.NumbersWithDecimals.GetRandom(0.0f, 1.0f) - 0.5f;

                            agent.transform.position = new Vector2(x + rx, y + ry);
                        }
                    }
                }

                time = 0.0f;
                timeTotal = World.SPAWN_TIMER;
            }
        }

        private void OnTickWorldDistance(int tick)
        {
            if (Parallax != null)
            {
                Parallax.Update(x, y);
            }

            float distance = x;
            float distanceToTravel = (Camera.main.aspect * (Camera.main.orthographicSize * 2.0f));

            bool hasTraveledEast;
            bool hasTraveledWest;

            hasTraveledEast = distance >= this.distance + distanceToTravel;
            hasTraveledWest = distance <= this.distance - distanceToTravel;

            if (hasTraveledEast || hasTraveledWest)
            {
                float x = World.PARALLAX_OFFSET_X + distance;
                float y = World.PARALLAX_OFFSET_Y + this.y;

                if (hasTraveledEast)
                {
                    x = x + distanceToTravel;
                }

                if (hasTraveledWest)
                {
                    x = x - distanceToTravel;
                }

                SpawnAgent001(x, y);
                SpawnAgent002(x, y);

                this.distance = distance;
            }
        }

        private void SpawnAgent001(float x, float y)
        {
            int count = MathUtils.Numbers.GetRandom(2, 4);

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (ManagerUtils.Instantiate<Agent>(IDs.PREFAB_ID__AGENT_001, out Agent agent))
                    {
                        float rx = MathUtils.NumbersWithDecimals.GetRandom(0.0f, 1.0f) - 0.5f;
                        float ry = MathUtils.NumbersWithDecimals.GetRandom(0.0f, 1.0f) - 0.5f;

                        agent.transform.position = new Vector2(x + rx, y + ry);
                    }
                }
            }
        }

        private void SpawnAgent002(float x, float y)
        {
            int count = MathUtils.Numbers.GetRandom(1, 4);

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (ManagerUtils.Instantiate<Agent>(IDs.PREFAB_ID__AGENT_002, out Agent agent))
                    {
                        float rx = MathUtils.NumbersWithDecimals.GetRandom(0.0f, 1.0f) - 0.5f;
                        float ry = MathUtils.NumbersWithDecimals.GetRandom(0.0f, 1.0f) - 0.5f;

                        agent.transform.position = new Vector2(x + rx, y + ry);
                    }
                }
            }
        }

        private void OnEventSceneMenu(Event e)
        {
            if (e == null)
            {
                return;
            }

            Parallax = null;
        }

        private void OnEventSceneGame(Event e)
        {
            if (e == null)
            {
                return;
            }

            Parallax = new Parallax();
        }

        public Parallax Parallax { get; private set; }

        public override string ToString() => $"World ()";
    }
}
