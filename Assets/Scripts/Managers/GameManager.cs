using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject player;

    private int gemCount = 0;
    private int gameDuration = 180;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 0;
    }
    private IEnumerator GameTimer()
    {
        float elapsedTime = 0f;

        while (elapsedTime < gameDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        UIManager.Instance.ActivateGameWinPanel();
    }
    public void IncreaseGemCount()
    {
        gemCount++;
    }

    public void GameFailed()
    {
        Time.timeScale = 0f;
        UIManager.Instance.ActivateFailPanel();
    }

    public int GetGemCount()
    {
        return gemCount;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
