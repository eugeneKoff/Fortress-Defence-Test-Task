using System;
using System.Collections.Generic;
using UnityEngine;

public class ModelWrapper
{
    public int maxHealth;

    public int currentHealth;

    public int speed;

    public int damage;

    public float attackRate;

    public float detectionDistance;

    public event Action Death;
    public event Action<int> HealthChange;


    public ModelWrapper(Model model)
    {
        maxHealth = model.maxHealth;
        currentHealth = model.maxHealth;

        speed = model.speed;

        damage = model.damage;

        attackRate = model.attackRate;

        detectionDistance = model.detectionDistance;
    }

    public void SetNewHealth(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;

            Death?.Invoke();
        }

        HealthChange?.Invoke(currentHealth);
    }

    public void UnsubscribeDeath()
    {
        Death = null;

    }
}
