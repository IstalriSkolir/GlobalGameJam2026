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
