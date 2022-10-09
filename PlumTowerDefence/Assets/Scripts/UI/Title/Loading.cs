using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] Slider progressBar;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync("DefenceScene");
        op.allowSceneActivation = false;
        float timer = 0.0f;
        progressBar.value = 1;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                progressBar.value = 1 - Mathf.Lerp(progressBar.value, op.progress, timer);
                if (progressBar.value >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                Debug.Log(timer);
                progressBar.value = 1 - Mathf.Lerp(progressBar.value, 1f, timer);
                if (progressBar.value == 1.0f||timer>5) 
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
