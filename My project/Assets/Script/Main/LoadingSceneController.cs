using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingSceneController : MonoBehaviour
{
    static string nextScene;

    [SerializeField] Image progressBar;
    [SerializeField] TextMeshProUGUI tipText;
    [SerializeField] int tipIndex = 0;
    public string[] tips;


    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName; 
        SceneManager.LoadScene("LoadingScene");
    }

    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        tipIndex = Random.Range(0, tips.Length);
        tipText.text = tips[tipIndex];
        Debug.Log("이번 숫자는.. " + tipIndex + "!!");

        float timer = 0f;
        while(!op.isDone)
        {
            yield return null; 

            if(op.progress < 0.9f)
            {                
                progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if(progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
