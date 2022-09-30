using UnityEngine;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public class BulletShell : MonoBehaviour
    {
        private const float BULLET_SHELL_LIFETIME = 3.0f;

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
            timeTotal = BulletShell.BULLET_SHELL_LIFETIME;
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
                timeTotal = BulletShell.BULLET_SHELL_LIFETIME;

                GameObject.Destroy(transform.gameObject);
            }
        }

        public override string ToString() => $"BulletShell ()";
    }
}
