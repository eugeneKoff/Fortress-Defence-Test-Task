using System;
using System.Collections.Generic;
using UnityEngine;

public class FortController : Controller
{
    public Model unitModel;

    public FortView fortView;

    public float _elapsedTime;

    public GameObject arrowPrefab;

    public Transform shootingPoint;

    public GameObject[] enemies;

    public event Action FortDefeated;

    private ModelWrapper _modelWrapper;

    // Start is called before the first frame update
    void Awake()
    {
        _modelWrapper = new ModelWrapper(unitModel);


        fortView.SetHealthText(_modelWrapper.currentHealth);

        _elapsedTime = _modelWrapper.attackRate;
    }

    private void OnEnable()
    {
        _modelWrapper.Death += Death;

        _modelWrapper.HealthChange += UpdateHPVisual;

    }



    // Update is called once per frame
    void Update()
    {
        fortView.UpdateShootButton(unitModel.attackRate);
    }

    private void Death()
    {
        FortDefeated?.Invoke();

    }

    private void UpdateHPVisual(int health)
    {
        fortView.SetHealthText(health);

        float hpPercent = (float)_modelWrapper.currentHealth / _modelWrapper.maxHealth;

        if(hpPercent >= .8f)
        {
            fortView.UpdateFortImage(0);
        }
        else if(hpPercent < .8f && hpPercent >= .5f)
        {
            fortView.UpdateFortImage(1);
        }
        else if(hpPercent < .5f & hpPercent >= .1f)
        {
            fortView.UpdateFortImage(2);
        }
        else
        {
            fortView.UpdateFortImage(3);
        }

    }

    public override void ReceiveDamage(int damage)
    {
        _modelWrapper.SetNewHealth(damage);
    }

    public void ShootArrow()
    {
        fortView.ToggleShootButton(false);

        Arrow arrow = Instantiate(arrowPrefab, shootingPoint.position, Quaternion.identity).GetComponent<Arrow>();

            arrow.SetUpArrow(_modelWrapper.damage, FindClosestEnemy());


           
    }

    private Transform FindClosestEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float tempDistanceA = 100f;
        GameObject tempEnemy = null;

        foreach(var enemy in enemies)
        {
            float tempDistanceB = Vector2.Distance(fortView.transform.position, enemy.transform.position);
            if (tempDistanceB < tempDistanceA)
            {
                tempDistanceA = tempDistanceB;
                tempEnemy = enemy;
            }
        }

        return tempEnemy.transform;
    }
}
