public class DevBoss : Boss
{
    internal override void death()
    {
        
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
