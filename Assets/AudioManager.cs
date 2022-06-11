using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;


    [SerializeField] private AudioSource bgm;

    [SerializeField] private List<AudioSource> sfxClips;

    private void Awake()
    {
        instance = this;
    }

    public void PlayBGM()
    {
        bgm.Stop();
        bgm.Play();
    }

    public void StopBGM()
    {
        bgm.Stop();
    }

    public void PlaySFX(int index)
    {
        sfxClips[index].Stop();
        sfxClips[index].Play();
    }
}
