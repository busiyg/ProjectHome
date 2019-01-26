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
    public Animator StoryAni;
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

    public void CleanTable() {
        //CleanBoss
        var BossList = GameObject.FindGameObjectsWithTag("Boss");
        var ThingList = GameObject.FindGameObjectsWithTag("Thing");

        if (BossList!=null&&BossList.Length>0) {
            foreach (var obj in BossList) {
                Destroy(obj);
            }
        }
      
        if (ThingList != null && ThingList.Length > 0) {
            foreach (var obj in ThingList) {
                Destroy(obj);
            }
        }

       
    }

    public static void ChangeLevel(int level) {
        MaskFadeInAndOut(() => {
            Instance.CleanTable();
          //  LoadBoss(level);
            SetBG(level);
            Instance.StoryAni.Play("Null");
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

    public void ChangeLevelT(int n) {


        string N ="S"+ n.ToString();
        print(N);
        MaskFadeInAndOut(()=> {
            StoryAni.Play(N);
        });
    }
}
