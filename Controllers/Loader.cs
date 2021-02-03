using System.Collections;
using System.Collections.Generic;
using Enums;
using Interfaces;
using Managers;
using Models.Screens;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class Loader : MonoBehaviour
    {
        #region Fields

        [Header("Options")]
        [SerializeField] private List<GameObject> m_managers = null;
        [SerializeField] private List<SceneType> m_scenes = null;
        [SerializeField] private BaseScreen m_screen;

        [SerializeField] private bool m_unloadSceneOnComplete = false;

        #endregion



        #region Unity Lifecycle

        private void Awake()
        {
            LoadManagers();
            LoadScreen();
            StartCoroutine(LoadScenesAsync());
        }

        #endregion



        #region Private Methods

        private void LoadManagers()
        {
            for (int i = 0; i < m_managers.Count; i++)
            {
                SpawnManager(m_managers[i]);
            }
        }


        private void LoadScreen()
        {
            if(m_screen == null) return;

            ScreensManager.Instance.ShowScreen(m_screen.GetType());
        }


        private void SpawnManager(GameObject manager)
        {
            GameObject managerObject = Instantiate(manager, transform);
            managerObject.transform.localPosition = Vector3.zero;
            managerObject.GetComponent<IManager>().Initialize();
        }

        
        private IEnumerator LoadScenesAsync()
        {
            for (int i = 0; i < m_scenes.Count; i++)
            {
                int sceneBuildIndex = (int)m_scenes[i];

                AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Additive);

                yield return asyncOperation;
            }

            if (m_unloadSceneOnComplete)
            {
                yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            }
        }

        #endregion
    }
}
