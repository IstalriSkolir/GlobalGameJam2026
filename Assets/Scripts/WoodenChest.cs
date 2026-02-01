using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenChest : MonoBehaviour
{
    [SerializeField]
    private bool isOpen;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private PlayAudioClip audio;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isOpen)
        {
            isOpen = true;
            animator.SetTrigger("Open");
        }
    }

    public void PlayRandomOpenAudio()
    {
        audio.PlayRandomAudioClip();
    }
}
