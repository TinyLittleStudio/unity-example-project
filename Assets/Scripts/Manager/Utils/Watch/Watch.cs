namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public class Watch
    {
        private float time;
        private float timeTotal;

        private WatchUtils.OnWatch onWatch;

        private Watch(float time, float timeTotal)
        {
            this.time = time;
            this.timeTotal = timeTotal;

            this.onWatch = null;

            Manager.GetManager().Inject(OnTick);
        }

        private Watch(float time, float timeTotal, WatchUtils.OnWatch onWatch)
        {
            this.time = time;
            this.timeTotal = timeTotal;

            this.onWatch = onWatch;

            Manager.GetManager().Inject(OnTick);
        }

        private void OnTick(int tick)
        {
            OnTickWatch(tick);
        }

        private void OnTickWatch(int tick)
        {
            if (!IsFinished)
            {
                time += Settings.TICK_TIME;

                if (time >= timeTotal)
                {
                    if (onWatch != null)
                    {
                        onWatch.Invoke(tick, true);
                    }

                    time = 0.0f;
                    timeTotal = 0.0f;

                    Manager.GetManager().Deject(OnTick);

                    IsFinished = true;
                }

                if (onWatch != null)
                {
                    onWatch.Invoke(tick, false);
                }
            }
        }

        public void Stop()
        {
            if (!IsFinished)
            {
                IsFinished = true;
            }
        }

        public bool IsFinished { get; private set; }

        public override string ToString() => $"Watch (IsFinished: {IsFinished})";

        public static Watch NewWatch(float time)
        {
            return new Watch(0.0f, time);
        }

        public static Watch NewWatch(float time, WatchUtils.OnWatch onWatch)
        {
            return new Watch(0.0f, time, onWatch);
        }
    }
}
