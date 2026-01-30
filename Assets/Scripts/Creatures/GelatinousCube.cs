using UnityEngine;

public class GelatinousCube : Boss
{
    [SerializeField, Header("Gelatinous Cube Properties")]
    private bool playerInside;
    [SerializeField]
    private float damageTickDelay;
    [SerializeField]
    private float damageTimer;
    [SerializeField]
    private int damage;

    void Update()
    {
        SetDestination(player.transform.position);
        if (playerInside)
        {
            damageTimer -= Time.deltaTime;
            if (damageTimer <= 0)
            {
                damageTimer = damageTickDelay;
                playerHealth.UpdateHealthByValue(damage);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            damageTimer = damageTickDelay;
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            playerInside = false;
    }
}
