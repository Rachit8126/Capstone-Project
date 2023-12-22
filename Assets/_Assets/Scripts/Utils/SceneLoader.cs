using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Capstone
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance;

        [SerializeField] private GameObject loadingCanvas;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            
            DontDestroyOnLoad(gameObject);
        }

        public void LoadSceneAsync(int buildIndex)
        {
            StartCoroutine(LoadSceneWithIEnumerator(buildIndex));
        }

        private IEnumerator LoadSceneWithIEnumerator(int buildIndex)
        {
            loadingCanvas.SetActive(true);
            AsyncOperation op = SceneManager.LoadSceneAsync(buildIndex);

            while (!op.isDone)
            {
                yield return null;
            }

            loadingCanvas.SetActive(false);
        }
    }
}
