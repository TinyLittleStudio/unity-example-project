using UnityEngine;

using static UnityEngine.ParticleSystem;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public static class ParticleUtils
    {
        public static void Play(string particleId, float x, float y)
        {
            if (ManagerUtils.Instantiate($"System/Particle/{particleId}", out ParticleSystem particleSystem))
            {
                MainModule mainModule = particleSystem.main;
                mainModule.stopAction = ParticleSystemStopAction.Destroy;

                particleSystem.gameObject.transform.position = new Vector3(x, y, 0.0f);
                particleSystem.IsEnabled(true);
            }
        }
    }
}
