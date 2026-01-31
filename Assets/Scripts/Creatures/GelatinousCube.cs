using UnityEngine;

public class GelatinousCube : Boss
{
    [SerializeField, Header("Gelatinous Cube Properties")]
    private float jumpTickDelay;
    [SerializeField]
    private float jumpTimer;
    [SerializeField]
    private float minJumpDistance;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpRecovery;
    [SerializeField]
    private bool isJumping;
    [SerializeField]
    private float agentSpeed;
    [SerializeField]
    private float agentJumpSpeed;
    [SerializeField]
    private Vector3 jumpTarget;

    [SerializeField, Header("Gelatinous Cube Gameobjects & Components")]
    private Transform childTransform;
    [SerializeField]
    private Rigidbody childRigibody;

    internal override void Start()
    {
        base.Start();
        jumpTimer = jumpTickDelay;
        agent.speed = agentSpeed;
    }

    void Update()
    {

        if (!isJumping)
        {
            SetDestination(player.transform.position);
            jumpTimer -= Time.deltaTime;
            if (jumpTimer <= 0)
            {
                jumpTimer = jumpTickDelay;
                jump();
            }
        }
        if (isJumping && childTransform.position.y <= 0)
        {
            isJumping = false;
            childRigibody.isKinematic = true;
            childTransform.localPosition = new Vector3(0, 0, 0);
            agent.speed = agentSpeed;
            Invoke("setDestinationToPlayer", jumpRecovery);
        }
    }

    private void jump()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= minJumpDistance)
        {
            isJumping = true;
            jumpTarget = player.transform.position;
            SetDestination(jumpTarget);
            agent.speed = agentJumpSpeed;
            childRigibody.isKinematic = false;
            childRigibody.AddForce(0, jumpForce * distance, 0, ForceMode.Impulse);
        }
    }

    private void setDestinationToPlayer()
    {
        SetDestination(player.transform.position);
    }
}
