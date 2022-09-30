using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public static class SceneUtils
    {
        public static void Load(string sceneId)
        {
            SceneManager.LoadScene(sceneId, LoadSceneMode.Single);
        }

        public static void Load(string sceneId, UnityAction<Scene, Scene> onScene)
        {
            if (onScene != null)
            {
                onScene += (Scene a, Scene b) =>
                {
                    SceneManager.activeSceneChanged -= onScene;
                };

                SceneManager.activeSceneChanged += onScene;
            }

            SceneManager.LoadScene(sceneId, LoadSceneMode.Single);
        }
    }
}
