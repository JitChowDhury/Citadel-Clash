using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private HealthSystem healthSystem;
    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDied += healthSystem_OnDied;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            healthSystem.Damage(999);
        }
    }

    private void healthSystem_OnDied(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }
}
