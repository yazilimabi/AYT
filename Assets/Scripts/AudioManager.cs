using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{
    Trash,
    Bullet,
    WaveFinish,
    Lose,
    Spawn,
    Pickup,
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] m_audios;
    List<AudioSource> m_sources = new List<AudioSource>();
    [SerializeField] float m_volume = 0.3f;

    static AudioManager instance = null;

    public static AudioManager Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType(typeof(AudioManager)) as AudioManager;

            return instance;
        }
    }

    public void Start() {
        for(int i = 0; i < m_audios.Length; i++) {
            AudioSource temp = gameObject.AddComponent<AudioSource>();
            m_sources.Add(temp);
            temp.playOnAwake = false;
            temp.clip = m_audios[i];
            temp.volume = m_volume;
        }
    }
    void triggerAudio(int index) {
        if (index >= m_sources.Count) return; 
        m_sources[index].Play();
    }
    public static void Play(AudioType type){
        Instance.triggerAudio((int)type);
    }
}