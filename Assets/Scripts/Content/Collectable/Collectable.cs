using UnityEngine;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Collectable : MonoBehaviour
    {
        [Header("Settings")]

        [SerializeField]
        private CollectableType collectableType;

        private void Awake()
        {
            AwakeCollectable();
        }

        private void AwakeCollectable()
        {
            CircleCollider2D = GetComponent<CircleCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collider2d)
        {
            OnTriggerEnter2DCollectable(collider2d);
        }

        private void OnTriggerEnter2DCollectable(Collider2D collider2d)
        {
            if (collider2d == null)
            {
                return;
            }

            if (collider2d.gameObject == null)
            {
                return;
            }

            if (collider2d.gameObject.CompareTag(IDs.TAG_ID__PLAYER))
            {
                switch (collectableType)
                {
                    case CollectableType.AMMUNITION:
                        OnCollectAmmunition();
                        break;

                    case CollectableType.AMMUNITION_SMALL:
                        OnCollectAmmunitionSmall();
                        break;

                    default:
                        break;
                }
            }
        }

        private void OnCollectAmmunition()
        {
            if (!CollectableType.AMMUNITION.Is(collectableType))
            {
                return;
            }

            float x = transform.position.x;
            float y = transform.position.y;

            if (Player.Current != null)
            {
                Player.Current.SetAmmunition(Player.Current.GetAmmunition() + 5);
            }

            ParticleUtils.Play(null, x, y);

            Event.Fire(
                new Event(IDs.EVENT_ID__USER_INTERFACE)
            );

            Event.Fire(
                new Event(IDs.EVENT_ID__COLLECTABLE)
                    .WithArgument<Collectable>(IDs.EVENT_ARGUMENT_ID__COLLECTABLE, this)
                    .WithArgument<CollectableType>(IDs.EVENT_ARGUMENT_ID__COLLECTABLE_TYPE, collectableType)
            );

            collectableType = CollectableType.NONE;

            GameObject.Destroy(transform.root.gameObject);
        }

        private void OnCollectAmmunitionSmall()
        {
            if (!CollectableType.AMMUNITION_SMALL.Is(collectableType))
            {
                return;
            }

            float x = transform.position.x;
            float y = transform.position.y;

            if (Player.Current != null)
            {
                Player.Current.SetAmmunition(Player.Current.GetAmmunition() + 2);
            }

            ParticleUtils.Play(null, x, y);

            Event.Fire(
                new Event(IDs.EVENT_ID__USER_INTERFACE)
            );

            Event.Fire(
                new Event(IDs.EVENT_ID__COLLECTABLE)
                    .WithArgument<Collectable>(IDs.EVENT_ARGUMENT_ID__COLLECTABLE, this)
                    .WithArgument<CollectableType>(IDs.EVENT_ARGUMENT_ID__COLLECTABLE_TYPE, collectableType)
            );

            collectableType = CollectableType.NONE;

            GameObject.Destroy(transform.root.gameObject);
        }

        public CircleCollider2D CircleCollider2D { get; private set; }

        public override string ToString() => $"Collectable ()";
    }
}
