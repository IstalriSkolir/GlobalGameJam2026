using UnityEngine;

public class TakeSwordDamage : Creature
{
    [SerializeField, Header("Properties")]
    private bool requireFire;
    [SerializeField]
    private GameObject deathEffectPrefab;
    [SerializeField]
    private GameObject swordFlames;

    private void Start()
    {
        swordFlames = GameObject.FindGameObjectWithTag("SwordFlames");
    }

    public override void UpdateHealthByValue(int change, bool decrease = true)
    {
        if (!requireFire || swordFlames.activeSelf)
            base.UpdateHealthByValue(change, decrease);
    }

    internal override void death()
    {
        if (deathEffectPrefab != null)
            Instantiate(deathEffectPrefab, transform.position, deathEffectPrefab.transform.rotation);
        Destroy(gameObject);
    }
}
