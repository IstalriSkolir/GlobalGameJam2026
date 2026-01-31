using UnityEngine.Events;

public class DevBoss : Boss
{

    public UnityEvent m_MyEvent;

    internal override void death()
    {
        m_MyEvent.Invoke();
    }

    void Update()
    {
        if (health <= 0)
        {
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
