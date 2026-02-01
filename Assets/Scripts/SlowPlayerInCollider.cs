using UnityEngine;

public class SlowPlayerInCollider : MonoBehaviour
{
    [SerializeField, Header("Properties")]
    private float speedModifier;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float playerDashSpeed;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerSpeed = player.m_Speed;
        playerDashSpeed = player.dashSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player.m_Speed *= speedModifier;
            player.dashSpeed *= speedModifier;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            player.m_Speed = playerSpeed;
            player.dashSpeed = playerDashSpeed;
        }
    }
}
