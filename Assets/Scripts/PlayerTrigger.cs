using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField, Header("Properties")]
    private PlayerTriggerType type;
    [SerializeField]
    private bool selfDestructAfterTrigger;
    [SerializeField]
    private float selfDestructDelay;
    [SerializeField]
    private string Arg1;
    [SerializeField]
    private GameObject ArgObj1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (type)
            {
                case PlayerTriggerType.Destroy_This_Object: destroyThisObject(); break;
                case PlayerTriggerType.Play_Animation: playAnimation(); break;
                case PlayerTriggerType.Set_Active_True: setActiveTrue(); break;
            }
            if (selfDestructAfterTrigger)
                Invoke("destroyThisObject", selfDestructDelay);
        }
    }

    private void destroyThisObject()
    {
        Destroy(gameObject);
    }

    private void playAnimation()
    {
        GetComponent<Animator>().SetTrigger(Arg1);
    }

    private void setActiveTrue()
    {
        ArgObj1.SetActive(true);
    }
}
