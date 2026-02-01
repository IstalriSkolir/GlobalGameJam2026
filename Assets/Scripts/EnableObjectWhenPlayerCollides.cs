using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectWhenPlayerCollides : MonoBehaviour
{
    [SerializeField]
    private GameObject gelatinousCube;
    [SerializeField]
    private bool isCubeActive;
    [SerializeField]
    private MusicManager music;

    private void Start()
    {
        //music = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isCubeActive)
        {
            isCubeActive = true;
            gelatinousCube.SetActive(true);
        }
    }

    public void ResetGelatinousCube(GameObject boss)
    {
        gelatinousCube = boss;
        isCubeActive = false;
    }
}
