using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossReset : MonoBehaviour
{
    [SerializeField, Header("Player")]
    private Vector3 playerRespawnLocation;
    [SerializeField]
    private PlayerHealth playerHealth;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private int playerMaxHealth;

    [SerializeField, Header("First Boss Gameobjects & Components")]
    private GameObject gelatinousCubePrefab;
    [SerializeField]
    private GameObject bossParent;
    [SerializeField]
    private Vector3 firstBossLocalPosition;
    [SerializeField]
    private GameObject firstTeleporter;
    [SerializeField]
    private GameObject gelatinousCubeTrigger;

    [SerializeField, Header("Second Boss Gameobject & Components")]
    private GameObject mechanicalArcherPrefab;
    [SerializeField]
    private GameObject currentMechanicalArcher;
    [SerializeField]
    private Vector3 secondBossLocalPosition;
    [SerializeField]
    private GameObject secondTeleporter;
    [SerializeField]
    private List<Transform> secondBossWaypoints;
    [SerializeField]
    private GameObject arrowBossArena;
    [SerializeField]
    private GameObject mechanicalArcherTrigger;

    private void Start()
    {
        playerMaxHealth = playerHealth.MaxHealth;
    }

    public void UpdatePlayerRespawnLocation(Vector3 newRespawn)
    {
        playerRespawnLocation = newRespawn;
    }

    public void ResetBosses()
    {
        resetPlayer();
        resetFirstBoss();
        resetSecondBoss();
    }

    private void resetPlayer()
    {
        playerHealth.health = playerMaxHealth;
        playerHealth.transform.position = playerRespawnLocation;
    }

    private void resetFirstBoss()
    {
        bossParent.GetComponent<FirstBossFightEnd>().ClearBossesList();
        firstTeleporter.SetActive(false);
        GameObject cube = Instantiate(gelatinousCubePrefab, new Vector3(0, 0, 0), Quaternion.identity, bossParent.transform);
        cube.transform.localPosition = firstBossLocalPosition;
        gelatinousCubeTrigger.GetComponent<EnableObjectWhenPlayerCollides>().ResetGelatinousCube(cube);
    }

    private void resetSecondBoss()
    {
        if (currentMechanicalArcher != null)
            Destroy(currentMechanicalArcher);
        secondTeleporter.SetActive(false);
        currentMechanicalArcher = Instantiate(mechanicalArcherPrefab, new Vector3(0, 0, 0), Quaternion.identity, arrowBossArena.transform);
        MechanicalArcher archer = currentMechanicalArcher.GetComponent<MechanicalArcher>();
        archer.SetPortalReference(secondTeleporter);
        archer.SetWaypoints(secondBossWaypoints);
        currentMechanicalArcher.transform.localPosition = secondBossLocalPosition;
        mechanicalArcherTrigger.GetComponent<EnableObjectWhenPlayerCollides>().ResetGelatinousCube(currentMechanicalArcher);
    }
}
