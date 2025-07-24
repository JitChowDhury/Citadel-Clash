using System;
using UnityEngine;

public class TowerHealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;

    private Transform barTransform;
    private Transform seperatorContainer;

    void Awake()
    {
        barTransform = transform.Find("bar");


    }

    void Start()
    {
        seperatorContainer = transform.Find("seperatorContainer");
        ConstructHealthBarSeperators();
        healthSystem.OnDamaged += healthSystem_OnDamaged;
        healthSystem.OnHealed += healthSystem_OnHealed;
        healthSystem.OnHealthAmountMaxChanged += healthSystem_OnHealthAmountMaxChanged;

        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void healthSystem_OnHealthAmountMaxChanged(object sender, EventArgs e)
    {
        ConstructHealthBarSeperators();
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
    private void ConstructHealthBarSeperators()
    {

        Transform seperatorTemplate = seperatorContainer.Find("seperatorTemplate");
        seperatorTemplate.gameObject.SetActive(false);

        foreach (Transform seperatorTransform in seperatorContainer)
        {
            if (seperatorTransform == seperatorTemplate) continue;
            Destroy(seperatorTransform.gameObject);
        }
        int healthAmountPerSeperator = 10;
        float barSize = 3f;
        float barOneHealthAmountSize = barSize / healthSystem.GetHealthAmountMax();

        int healthSeperatorCount = Mathf.FloorToInt(healthSystem.GetHealthAmountMax() / healthAmountPerSeperator);
        for (int i = 1; i < healthSeperatorCount; i++)
        {
            Transform seperatorTransform = Instantiate(seperatorTemplate, seperatorContainer);
            seperatorTransform.gameObject.SetActive(true);
            seperatorTransform.localPosition = new Vector3(barOneHealthAmountSize * i * healthAmountPerSeperator, 0, 0);
        }
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
