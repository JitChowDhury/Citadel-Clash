using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HealthSystem : MonoBehaviour
{

    public event EventHandler OnDamaged;
    public event EventHandler OnDied;
    [SerializeField] private int healthAmountMax;
    private int healthAmount;

    void Awake()
    {
        healthAmount = healthAmountMax;
    }
    //damage
    public void Damage(int damageAmount)
    {
        healthAmount -= damageAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);
        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (isDead())
        {
            OnDied?.Invoke(this, EventArgs.Empty);
        }

    }
    //if is dead
    public bool isDead()
    {
        return healthAmount == 0;
    }

    public int GetHealthAmount()
    {
        return healthAmount;
    }

    public float GetHealthAmountNormalized()
    {
        return (float)healthAmount / healthAmountMax;
    }
    public bool isFullHealth()
    {
        return healthAmount == healthAmountMax;
    }

    internal void setHealthAmountMax(int healthAmountMax, bool updateHealthAmount)
    {
        this.healthAmountMax = healthAmountMax;
        if (updateHealthAmount) healthAmount = healthAmountMax;
    }
}
