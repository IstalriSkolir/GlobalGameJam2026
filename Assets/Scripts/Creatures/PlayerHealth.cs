using UnityEngine;
using UnityEngine.UI;

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

    public CanvasGroup healthUI;

    [SerializeField, Header("Player Health Gameobjects & Components")]
    private PlayerController playerController;
    [SerializeField]
    private GameObject bossResetObj;

    private BossReset reset;

    private void Start()
    {
        regenTimer = regenDelayTime;
        reset = bossResetObj.GetComponent<BossReset>();
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

        healthUI.alpha = 1 - (health * 0.01f);
    }

    internal override void death()
    {
        RunBossReset();
    }

    public void RunBossReset()
    {
        reset.ResetBosses();
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
