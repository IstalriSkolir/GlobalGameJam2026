using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField, Header("Properties")]
    private float fadeModifer;
    [SerializeField]
    private float volumeTarget;
    [SerializeField]
    private AudioClip alterMusic;
    [SerializeField]
    private AudioClip battleMusic;
    [SerializeField]
    private bool fadeIn;
    [SerializeField]
    private bool fadeOut;

    [SerializeField, Header("Gameobjects & Compnents")]
    private AudioSource source;

    private float target;

    private void Start()
    {
        target = volumeTarget;
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeIn && source.volume < target)
        {
            source.volume += (Time.deltaTime * fadeModifer);
            if (source.volume >= target)
            {
                source.volume = target;
                fadeIn = false;
            }
        }
        else if (fadeOut && source.volume > target)
        {
            source.volume -= (Time.deltaTime * fadeModifer);
            if (source.volume <= target)
            {
                source.volume = target;
                fadeOut = false;
            }
        }
    }

    public void UpdateMusicMode(MusicMode mode)
    {
        switch (mode)
        {
            case MusicMode.Stop: stop(); break;
            case MusicMode.Alter: alter(); break;
            case MusicMode.Battle: battle(); break;
        }
    }

    private void stop()
    {
        fadeOut = true;
        target = 0;
    }

    private void alter()
    {
        //target = volumeTarget;
        source.volume = volumeTarget;
        source.clip = alterMusic;
        source.Play();
    }

    private void battle()
    {
        source.volume = volumeTarget;
        source.clip = battleMusic;
        source.Play();
    }
}
