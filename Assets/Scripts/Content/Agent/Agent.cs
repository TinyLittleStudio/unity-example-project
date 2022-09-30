using UnityEngine;
using UnityEngine.UI;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public class Agent : MonoBehaviour
    {
        private const float HEALTH_REGENERATION_RATE = 0.1f;
        private const float HEALTH_REGENERATION_RATE_VELOCITY = 1.0f;

        private const float HEALTH_UI_LERP = 4.0f;

        [Header("Settings")]

        [SerializeField]
        private float health;

        [SerializeField]
        private float healthTotal;

        [Space(10)]

        [SerializeField]
        private Image bar;

        [SerializeField]
        private Image barInfill;

        private void Start()
        {
            StartHealth();
        }

        private void StartHealth()
        {
            if (bar != null)
            {
                bar.IsEnabled(false);
            }

            if (barInfill != null)
            {
                barInfill.IsEnabled(false);
            }
        }

        private void Update()
        {
            UpdateHealth();
        }

        private void UpdateHealth()
        {
            if (health < healthTotal)
            {
                health += Agent.HEALTH_REGENERATION_RATE * (Time.deltaTime * Agent.HEALTH_REGENERATION_RATE_VELOCITY);
            }

            if (bar != null)
            {
                bar.IsEnabled(health > 0.0f);
            }

            if (barInfill != null)
            {
                float fillAmount = health / healthTotal;

                fillAmount = Mathf.Lerp(barInfill.fillAmount, fillAmount, Time.deltaTime * Agent.HEALTH_UI_LERP);

                if (fillAmount < 0.0f)
                {
                    fillAmount = 0.0f;
                }

                if (fillAmount > 1.0f)
                {
                    fillAmount = 1.0f;
                }

                barInfill.fillAmount = fillAmount;

                barInfill.IsEnabled(health > 0.0f);
            }
        }

        public void InflictDamage()
        {
            InflictDamage(1.0f);
        }

        public void InflictDamage(float damage)
        {
            damage = Mathf.Abs(damage);

            health = health - damage;

            health = Mathf.Clamp(health, 0.0f, healthTotal);

            if (health <= 0.0f)
            {
                Die();
            }
        }

        public void Die()
        {
            float x = transform.position.x;
            float y = transform.position.y;

            CollectableUtils.Drop(x, y);

            GameObject.Destroy(transform.root.gameObject);
        }

        public override string ToString() => $"Agent ()";
    }
}
