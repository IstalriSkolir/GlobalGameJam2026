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
    [SerializeField]
    private float playerReducedSpeed;
    [SerializeField]
    private float playerReducedDashSpeed;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerSpeed = player.topSpeed;
        playerDashSpeed = player.topDashSpeed;
        playerReducedSpeed = playerSpeed * 1; //speedModifier;
        playerReducedDashSpeed = playerDashSpeed * 1; // speedModifier;     Commented out, can fix later if time allows
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "Player")
    //    {
    //        player.m_Speed = playerReducedSpeed;
    //        player.dashSpeed = playerReducedDashSpeed;
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            player.m_Speed = playerReducedSpeed;
            player.dashSpeed = playerReducedDashSpeed;
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
