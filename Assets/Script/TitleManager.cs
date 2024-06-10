using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class TitleManager : MonoBehaviour
{
    public static TitleManager instance;

    private SaveNLoad theSaveNLoad;
    private DataManager thedata;
    public GameObject loading;



    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public void StartScene()
    {
        // 다른 씬으로 이동
        Debug.Log("스타트");
        StartCoroutine(StartCoroutine());
    }
    IEnumerator StartCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("GameScene");

        while (!operation.isDone)
        {
            loading.SetActive(true);
            yield return null;
        }
        Destroy(gameObject);
    }

    public void load()
    {
        // 다른 씬으로 이동
        Debug.Log("로드");
        StartCoroutine(LoadCoroutine());
    }

    IEnumerator LoadCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("GameScene");
        thedata = DataManager.instance;
        while (!operation.isDone)
        {
            thedata.DataLoad();
            loading.SetActive(true);
            yield return null;
        }
        
        thedata.DataLoad();
        Destroy(gameObject);
    }

    public void QuitGame()
    {
        // 게임 종료
        Application.Quit();
    }

    
}
