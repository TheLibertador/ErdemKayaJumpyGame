using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameStartPanel;
    [SerializeField] private GameObject gameFailPanel;
    [SerializeField] private GameObject gameWinPanel;
    [SerializeField] private TMP_Text diamondCountText;
    [SerializeField] private Image diamondIcon;
         
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    public void OnStartGameButtonPressed()
    {
        Debug.Log("button is presed");
        Time.timeScale = 1f;
        gameStartPanel.SetActive(false);
        diamondCountText.enabled = true;
        diamondIcon.enabled = true;
    }

    public void OnTryAgainButtonPressed()
    {
        GameManager.Instance.ReloadScene();
    }

    public void UpdateDiamondCountText(int num)
    {
        diamondCountText.text = num.ToString();
    }

    public void ActivateFailPanel()
    {
        gameFailPanel.SetActive(true);
    }

    public void ActivateGameWinPanel()
    {
        gameWinPanel.SetActive(true);
    }
}
