using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    private static AudioManager Instance;
    public List<Audios> audios=new List<Audios>();
    public List<Audios> BGMaudios = new List<Audios>();
    public AudioSource audioSource;
    public AudioSource BGMSource;


    private void Awake() {
        Instance = this;
    }
    // Use this for initialization
    void Start() {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update() {

    }


    public static void PlayAudioClip(int Key) {
        foreach (var obj in Instance.audios) {
            if (Key == obj.key) {
                Instance.audioSource.clip = obj.Clip;
                Instance.audioSource.Play();
            }
        }
    }

    public static void PlayBGM(int Key) {
        foreach (var obj in Instance.BGMaudios) {
            if (Key == obj.key) {
                Instance.audioSource.clip = obj.Clip;
                Instance.audioSource.Play();
            }
        }
    }

    public static void PlayerAudio(int key) {

    }

    [System.Serializable]
    public class Audios {
        public AudioClip Clip;
        public int key;
    }

}


