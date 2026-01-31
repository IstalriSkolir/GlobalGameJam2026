using UnityEngine;

public class PlayerHealth : Creature
{
    [SerializeField, Header("Player Health Properties")]
    private float regenDelayTime;
    [SerializeField]
    private float regenTimer;
    [SerializeField]
    private bool regenActive;
    [SerializeField]
    private float regenTickDelay;
    [SerializeField]
    private float regenTick;
    [SerializeField]
    private int regenValue;
    [SerializeField]
    private PlayerController playerController;

    private void Start()
    {
        regenTimer = regenDelayTime;
    }

    private void Update()
    {
        if (!regenActive)
        {
            if (regenTimer > 0) regenTimer -= Time.deltaTime;
            else regenActive = true;
        }
        else
        {
            regenTick -= Time.deltaTime;
            if (regenTick <= 0)
            {
                regenTick = regenTickDelay;
                UpdateHealthByValue(regenValue, false);
            }
        }
    }

    internal override void death()
    {
        
    }

    public override void UpdateHealthByValue(int change, bool decrease = true)
    {
        if (decrease && !playerController.blocking)
        {
            base.UpdateHealthByValue(change, decrease);
            regenActive = false;
            regenTimer = regenDelayTime;
            regenTick = regenTickDelay;
        }
        else if(!decrease)
        {
            base.UpdateHealthByValue(change, decrease);
        }
    }
}
