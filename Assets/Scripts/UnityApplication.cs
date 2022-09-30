using UnityEngine;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public class UnityApplication : MonoBehaviour
    {
        private static bool IsInstalled { get; set; } = false;

        private void Awake()
        {
            AwakeApplication();

            IsInstalled = true;
        }

        private void AwakeApplication()
        {
            if (!IsInstalled)
            {
                Manager.GetManager().Launch();
            }
        }

        private void Update()
        {
            UpdateApplication();
        }

        private void UpdateApplication()
        {
            if (IsInstalled)
            {
                Manager.GetManager().Update();
            }
        }

        public override string ToString() => $"UnityApplication (IsInstalled: {IsInstalled})";
    }
}
