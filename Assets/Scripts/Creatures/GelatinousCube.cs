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
    [SerializeField]
    private Animator jellyAnimator;

    [SerializeField, Header("Gelatinous Cube Gameobjects & Components")]
    private Transform childTransform;
    [SerializeField]
    private Transform parentTransform;
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

    public void SetParentTransformVariable(Transform parent)
    {
        parentTransform = parent;
    }

    internal override void death()
    {
        FirstBossFightEnd parentScript = parentTransform.gameObject.GetComponent<FirstBossFightEnd>();
        if (explosion != null)
        {
            GameObject newBoss = Instantiate(explosion, transform.position, transform.rotation);
            if (newBoss.transform.childCount == 2)
            {
                GelatinousCube cube1 = newBoss.transform.GetChild(0).gameObject.GetComponent<GelatinousCube>();
                GelatinousCube cube2 = newBoss.transform.GetChild(1).gameObject.GetComponent<GelatinousCube>();
                cube1.SetParentTransformVariable(parentTransform);
                cube2.SetParentTransformVariable(parentTransform);
                parentScript.UpdateBossesList(cube1, true);
                parentScript.UpdateBossesList(cube2, true);
            }
            //GelatinousCube[] scripts = newBoss.GetComponentsInChildren<GelatinousCube>();
            //parentScript.UpdateBossesList(scripts, true);
        }
        transform.SetParent(null);
        parentScript.UpdateBossesList(this, false);
        //parentScript.UpdateBossesList(new GelatinousCube[]{this}, false);
        Destroy(gameObject);
    }
}
