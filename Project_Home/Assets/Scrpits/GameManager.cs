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
    public List<GameObject> Bullets;
    public Image mask;
    public Animator StoryAni;
    public PlayerController Player;
    public GameObject PlayerPrefab;
    public SpriteRenderer finishBG;
    public int CurrentLevel;
    private void Awake() {
        Instance = this;
    }
    void Start () {
        //ChangeLevel(1);
       ChangeLevel(1);
    }

    public static GameManager GetInstance() {
        return Instance;
    }

    public void InitPlayer() {
        if (Player==null) {
            Player = Instantiate(PlayerPrefab).GetComponent<PlayerController>();
        }
    }

    //淡入淡出
    public void MaskFadeInAndOut(System.Action CallBack) {
        mask.DOFade(1, 0.5f).OnComplete(() => {
            mask.DOFade(0, 0.5f);
            if (CallBack!=null) {
                CallBack.Invoke();
            }    
        });  
    }

    //清理
    public void CleanTable() {
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

    //切换关卡
    public void ChangeLevel(int level) {
        MaskFadeInAndOut(() => {
            InitPlayer();
            Instance.CurrentLevel = level;
            Instance.CleanTable();
            SetBG(level);
            ChangeBullet(level);
            Instance.StoryAni.Play("Null");
            LoadBoss(level);
        });
    }

    //切换Boss
    public static void LoadBoss(int level) {
        foreach (var obj in Instance.Bosses) {
            if (level == int.Parse(obj.name)) {
                Instantiate(obj);
            }
        }
    }

    //切换子弹
    public static void ChangeBullet(int level) {
        foreach (var obj in Instance.Bullets) {
            if (level == int.Parse(obj.name)) {
                Instance.Player.BulletPrefab = obj;
            }
        }
    }

    //背景部分
    public void ShowdFinishBG() {
        finishBG.DOFade(1,3).OnComplete(()=> {
            StoryAni.Play("Ani3");
        });
    }

    public static void SetBG(int key) {
        foreach (var obj in Instance.BGSprites) {
            if (key==int.Parse(obj.name)) {
                Instance.BGRenderer.sprite = obj;
            }
        }
    }

    public void FinishLevel() {
        string N = "Ani" + CurrentLevel.ToString();
        MaskFadeInAndOut(() => {
            StoryAni.Play(N);
        });
    }

    public void ChangeLevelT(int n) {
        string N ="S"+ n.ToString();
        print(N);
        MaskFadeInAndOut(()=> {
            StoryAni.Play(N);
        });
    }

    public void GameOver() {
        Destroy(Player.gameObject);
        UIManager.GetInstance().ShowGameOver();
    }

    public void Restart() {
        ChangeLevel(CurrentLevel);
    }
}
