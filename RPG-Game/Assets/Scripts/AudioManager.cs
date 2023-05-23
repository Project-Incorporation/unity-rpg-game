using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public AudioSource[] sfx;
    public AudioSource[] bgm;


    public static AudioManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        PlayRandomMusic();
    }

    public void PlaySFX(int soundToPlay)
    {
        if(soundToPlay < sfx.Length)
        {
            sfx[soundToPlay].Play();
        }

    }

    public void PlayBGM(int musicToPlay)
    {
        if (!bgm[musicToPlay].isPlaying) 
        {
            StopMusic();

            if(musicToPlay < bgm.Length)
            {
                bgm[musicToPlay].Play();
            }
        }
    }

    public void StopMusic()
    {
        for(int i = 0; i < bgm.Length;  i++) 
        {
            bgm[i].Stop();
        }
    }

    public void PlayRandomMusic()
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(1, bgm.Length);
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayBGM(randomNumber);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            StopMusic();
        }
    }
}
