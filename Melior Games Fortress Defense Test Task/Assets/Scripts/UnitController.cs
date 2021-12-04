using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : Controller
{
    public Model unitModel;

    public UnitView unitView;

    public float _elapsedTime;

    public GameObject arrowPrefab;

    public Transform shootingPoint;

    public GameObject[] enemies;

    private ModelWrapper _modelWrapper;

    // Start is called before the first frame update
    void Awake()
    {
        _modelWrapper = new ModelWrapper(unitModel);


        unitView.SetHealthText(_modelWrapper.currentHealth);

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
        unitView.UpdateShootButton(unitModel.attackRate);
    }

    private void Death()
    {
        Destroy(gameObject);

    }

    private void UpdateHPVisual(int health)
    {
        unitView.SetHealthText(health);

       

    }

    public override void ReceiveDamage(int damage)
    {
        _modelWrapper.SetNewHealth(damage);
    }

    public void ShootArrow()
    {
        unitView.ToggleShootButton(false);

        Arrow arrow = Instantiate(arrowPrefab, shootingPoint.position, Quaternion.identity).GetComponent<Arrow>();

        arrow.SetUpArrow(_modelWrapper.damage, FindClosestEnemy());

        unitView.PlayAttackAnimation();

    }

    private Transform FindClosestEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float tempDistanceA = 100f;
        GameObject tempEnemy = null;

        foreach (var enemy in enemies)
        {
            float tempDistanceB = Vector2.Distance(unitView.transform.position, enemy.transform.position);
            if (tempDistanceB < tempDistanceA)
            {
                tempDistanceA = tempDistanceB;
                tempEnemy = enemy;
            }
        }

        return tempEnemy.transform;
    }

    public void ToggleGameUnit(bool On)
    {
        unitView.ToggleGameUnit(On);
    }
}
