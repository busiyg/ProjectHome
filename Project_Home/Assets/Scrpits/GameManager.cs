using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour {
    private static GameManager Instance;
    // Use this for initialization
    public SpriteRenderer BGRenderer;
    public List<Sprite> BGSprites;
    public List<GameObject> Bosses;
    public Image mask;
    private void Awake() {
        Instance = this;
    }
    void Start () {
		
	}

    public static void MaskFadeInAndOut(System.Action CallBack) {
        Instance.mask.DOFade(1, 0.5f).OnComplete(() => {
            Instance.mask.DOFade(0, 0.5f);
            if (CallBack!=null) {
                CallBack.Invoke();
            }    
        });
      
    }

	// Update is called once per frame
	void Update () {
		
	}

    public static void ChangeLevel(int level) {
        MaskFadeInAndOut(() => {
            SetBG(level);
            LoadBoss(level);
        });
    }

    public static void LoadBoss(int level) {
        foreach (var obj in Instance.Bosses) {
            if (level == int.Parse(obj.name)) {
                Instantiate(obj);
            }
        }
    }



    public static void SetBG(int key) {
        foreach (var obj in Instance.BGSprites) {
            if (key==int.Parse(obj.name)) {
                Instance.BGRenderer.sprite = obj;
            }
        }
    }
}
