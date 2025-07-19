using System;
using UnityEngine;

public class TowerHealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;

    private Transform barTransform;

    void Awake()
    {
        barTransform = transform.Find("bar");

    }

    void Start()
    {
        healthSystem.OnDamaged += healthSystem_OnDamaged;
        healthSystem.OnHealed += healthSystem_OnHealed;

        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void healthSystem_OnHealed(object sender, EventArgs e)
    {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void healthSystem_OnDamaged(object sender, EventArgs e)
    {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void UpdateBar()
    {
        barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(), 1, 1);
    }

    private void UpdateHealthBarVisible()
    {
        if (healthSystem.isFullHealth()) gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}
