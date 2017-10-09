using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{

    private AsyncOperation async;
    public GameObject LoadingUi;
    public Slider Slider;
    private string SceneName;

    public void LoadNextScene(string scene)
    {
        SceneName = scene;
        LoadingUi.SetActive(true);
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync(SceneName);

        while (!async.isDone)
        {
            Slider.value = async.progress;
            yield return null;
        }
    }
}