using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;
namespace AnimaresTest.Managers
{
    public class SceneManager : MonoBehaviour
    {
        public UnityEvent<string> _onSceneLoaded;
        public UnityEvent<string> _onSceneUnloaded;

        private void OnEnable()
        {
            UnitySceneManager.sceneLoaded += OnSceneLoaded;
            UnitySceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnDisable()
        {
            UnitySceneManager.sceneLoaded -= OnSceneLoaded;
            UnitySceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        public void LoadScene(string scene)
        {
            if (!CheckSceneLoaded(scene))
            {
                UnitySceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            }
        }

        public bool CheckSceneLoaded(string scene)
        {
            return UnitySceneManager.GetSceneByName(scene).isLoaded;
        }

        public void UnloadScene(string scene)
        {
            if (CheckSceneLoaded(scene))
            {
                UnitySceneManager.UnloadSceneAsync(scene);
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _onSceneLoaded?.Invoke(scene.name);
        }

        private void OnSceneUnloaded(Scene scene)
        {
            _onSceneUnloaded?.Invoke(scene.name);
        }

    }
}
