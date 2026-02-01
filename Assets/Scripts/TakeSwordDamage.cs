using UnityEngine;
using UnityEngine.Events;

public class TakeSwordDamage : Creature
{
    [SerializeField, Header("Properties")]
    private bool requireFire;
    [SerializeField]
    private GameObject deathEffectPrefab;
    [SerializeField]
    private GameObject swordFlames;

    public UnityEvent m_MyEvent;

    private void Start()
    {
        swordFlames = GameObject.FindGameObjectWithTag("SwordFlames");
    }

    public override void UpdateHealthByValue(int change, bool decrease = true)
    {
        if (!requireFire || swordFlames.activeSelf)
            base.UpdateHealthByValue(change, decrease);
    }

    void Update()
    {
        if (swordFlames == null)
        {
            try
            {
                swordFlames = GameObject.FindGameObjectWithTag("SwordFlames");
            }
            catch {
                
            }
        }
    }

    internal override void death()
    {
        m_MyEvent.Invoke();

        if (deathEffectPrefab != null)
            Instantiate(deathEffectPrefab, transform.position, deathEffectPrefab.transform.rotation);
        Destroy(gameObject);
    }
}
