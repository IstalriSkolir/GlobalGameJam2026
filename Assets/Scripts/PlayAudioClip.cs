using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioClip : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> clips;
    [SerializeField]
    private AudioSource source;

    public void PlayRandomAudioClip(bool audioOverride = false)
    {
        int index = Random.Range(0,clips.Count);
        source.clip = clips[index];
        if (!source.isPlaying || audioOverride)
            source.Play();
    }

    public void PlayAudioClipAtIndex(int index, bool audioOverride = false)
    {
        if (index >= 0 &&  index < clips.Count && (!source.isPlaying || audioOverride))
        {
            source.clip = clips[index];
            source.Play();
        }
    }
}
