using UnityEngine;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public class MuzzleFlash : MonoBehaviour
    {
        private const float MUZZLE_FLASH_LIFETIME = 0.1f;

        [Header("Settings")]

        [SerializeField]
        private float time;

        [SerializeField]
        private float timeTotal;

        private void Start()
        {
            StartBulletShell();
        }

        private void StartBulletShell()
        {
            time = 0.0f;
            timeTotal = MuzzleFlash.MUZZLE_FLASH_LIFETIME;
        }

        private void Update()
        {
            UpdateBulletShell();
        }

        private void UpdateBulletShell()
        {
            time += Time.deltaTime;

            if (time >= timeTotal)
            {
                time = 0.0f;
                timeTotal = MuzzleFlash.MUZZLE_FLASH_LIFETIME;

                GameObject.Destroy(transform.gameObject);
            }
        }

        public override string ToString() => $"MuzzleFlash ()";
    }
}
