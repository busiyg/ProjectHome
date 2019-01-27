using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour {
    private static UIManager Instance;
    public static UIManager GetInstance() {
        return Instance;
    }

    public GameObject GameOverPage;
    public GameObject StartGamePage;
    // Use this for initialization

    private void Awake() {
        Instance = this;
    }

    public void ShowGameOver() {
        GameOverPage.SetActive(true);
    }

    public void HideStartGamePage() {
        StartGamePage.GetComponent<CanvasGroup>().DOFade(0,0.5f);
        StartGamePage.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnStartGame() {
        GameManager.GetInstance().ChangeLevelT(1);
        HideStartGamePage();
    }

    public void OnRestart() {
        GameManager.GetInstance().Restart();
        GameOverPage.SetActive(false);
        
    }

}
