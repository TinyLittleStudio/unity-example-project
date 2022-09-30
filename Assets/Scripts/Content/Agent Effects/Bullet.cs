using UnityEngine;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        private const float BULLET_LIFETIME = 12.0f;

        private const float BULLET_VELOCITY = 16.0f;

        [Header("Settings")]

        [SerializeField]
        private float time;

        [SerializeField]
        private float timeTotal;

        [Space(10)]

        [SerializeField]
        private Vector2 velocity;

        private void Awake()
        {
            AwakeRigidbody2D();
        }

        private void AwakeRigidbody2D()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            StartBullet();
        }

        private void StartBullet()
        {
            time = 0.0f;
            timeTotal = Bullet.BULLET_LIFETIME;
        }

        private void Update()
        {
            UpdateBullet();
            UpdateBulletTransform();
        }

        private void UpdateBullet()
        {
            time += Time.deltaTime;

            if (time >= timeTotal)
            {
                time = 0.0f;
                timeTotal = Bullet.BULLET_LIFETIME;

                GameObject.Destroy(transform.gameObject);
            }
        }

        private void UpdateBulletTransform()
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, (velocity.x > 0.0f ? -90.0f : +90.0f));
        }

        private void FixedUpdate()
        {
            FixedUpdateBullet();
        }

        private void FixedUpdateBullet()
        {
            if (Rigidbody2D != null)
            {
                Rigidbody2D.velocity = velocity * Bullet.BULLET_VELOCITY;
            }
        }

        private void OnTriggerEnter2D(Collider2D collider2d)
        {
            OnTriggerEnter2DBullet(collider2d);
        }

        private void OnTriggerEnter2DBullet(Collider2D collider2d)
        {
            if (collider2d == null)
            {
                return;
            }

            if (collider2d.gameObject == null)
            {
                return;
            }

            Agent agent = collider2d.gameObject.GetComponent<Agent>();

            if (agent != null)
            {
                agent.InflictDamage(2.0f);

                GameObject.Destroy(transform.gameObject);
            }
            else
            {
                GameObject.Destroy(transform.gameObject);
            }
        }

        public Vector2 GetVelocity()
        {
            return this.velocity;
        }

        public Vector2 SetVelocity(Vector2 velocity)
        {
            return this.velocity = velocity;
        }

        public Rigidbody2D Rigidbody2D { get; private set; }

        public override string ToString() => $"Bullet ()";
    }
}
