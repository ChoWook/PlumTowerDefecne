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
            progressBar.value = 1 - Mathf.Lerp(0, 1f, timer * op.progress);

            if (op.progress < 0.9f)
            {
                progressBar.value = 1 - Mathf.Lerp(0, 1, timer * op.progress);
            }
            else
            {
                if (progressBar.value == 1.0f||timer>3) 
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
