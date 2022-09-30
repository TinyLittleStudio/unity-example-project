using UnityEngine;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ParallaxObject : MonoBehaviour
    {
        [Header("Settings")]

        [SerializeField]
        private Layer layer;

        private void Awake()
        {
            AwakeSpriteRenderer();
        }

        private void AwakeSpriteRenderer()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        public Layer GetLayer()
        {
            return this.layer;
        }

        public Layer SetLayer(Layer layer)
        {
            return this.layer = layer;
        }

        public SpriteRenderer SpriteRenderer { get; private set; }

        public override string ToString() => $"ParallaxObject ()";
    }
}
