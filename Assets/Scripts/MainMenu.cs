using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField, Header("Properties")]
    private float volumeIncreaseDuration;
    [SerializeField]
    private float volumeTarget;
    [SerializeField]
    private int playerCameraPriority;

    [SerializeField, Header("Gameobjects & Components")]
    private AudioSource source;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private CinemachineVirtualCamera playerCineCamera;
    [SerializeField]
    private PlayerController playerController;

    private void Start()
    {
        //StartCoroutine(increaseVolume(volumeIncreaseDuration, volumeTarget));
    }

    private IEnumerator increaseVolume(float duration, float target)
    {
        float stepTime = 0;
        while(stepTime < duration)
        {
            stepTime += Time.deltaTime;
            source.volume = Mathf.Lerp(0, target, stepTime / duration);
            yield return null;
        }
        if (source.volume != target) source.volume = target;
    }

    public void StartButtonPressed()
    {
        animator.SetTrigger("Start");
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }

    public void SetPlayerVCamPriority()
    {
        playerCineCamera.Priority = playerCameraPriority;
    }

    public void DisableMainMenu()
    {
        playerController.enabled = true;
        Destroy(this.gameObject);
    }
}
