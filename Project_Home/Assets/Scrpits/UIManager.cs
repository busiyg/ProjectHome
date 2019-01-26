using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour {
    private static UIManager Instance;
    public static UIManager GetInstance() {
        return Instance;
    }

    public GameObject GameOverPage;
    // Use this for initialization

    private void Awake() {
        Instance = this;
    }

    public void ShowGameOver() {
        GameOverPage.SetActive(true);
    }

    public void Restart() {
        GameManager.GetInstance().Restart();
        GameOverPage.SetActive(false);
        
    }

}
