using System;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType { Range, Melee };

public class EnemyController : Controller
{
    public Model enemyModel;

    public EnemyView enemyView;

    public float _elapsedTime;

    private bool _isAttacking;
    private bool _isDying;

    public EnemyType enemyType;

    private ModelWrapper _modelWrapper;


    // Start is called before the first frame update
    void Awake()
    {
        _modelWrapper = new ModelWrapper(enemyModel);

        enemyView.SetHealthText(_modelWrapper.currentHealth);

        _elapsedTime = enemyModel.attackRate;

        _isAttacking = false;
        _isDying = false;
}

    private void OnEnable()
    {
        _modelWrapper.Death += Death;

        _modelWrapper.HealthChange += ChangeHealth;

        enemyView.DeathAnimPlayed += DestroyUnit;

        enemyView.TargetDetected += Attack;

        enemyView.PathClear += CancelAttack;

    }

    private void Update()
    {
        if (!_isDying)
        {
            _elapsedTime += Time.deltaTime;

            

            if (!_isAttacking)
            {
                enemyView.Move(enemyModel.speed);

            }
        }
        
    }

    private void FixedUpdate()
    {
        if (!_isDying)
        {
            enemyView.DetectEnemy(enemyModel.detectionDistance);
        }
    }


    private void Death()
    {

        enemyView.gameObject.tag = "Untagged";

        _isDying = true;
        enemyView.PlayDeathAnimation();

        _modelWrapper.UnsubscribeDeath();
    }

    private void ChangeHealth(int health)
    {
        enemyView.SetHealthText(health);
    }

    private void DestroyUnit()
    {
        Destroy(gameObject);
    }

    

    public virtual void Attack(RaycastHit2D hit)
    {
        if(_elapsedTime >= _modelWrapper.attackRate)
        {

            print("attack");

            _isAttacking = true;

            enemyView.PlayAttackAnimation();

            switch (enemyType)
            {
            case EnemyType.Range:
                //do smth
                break;
            case EnemyType.Melee:

                    if(hit.transform!= null)
                    {
                        Controller controller = hit.transform.gameObject.GetComponentInParent<Controller>();

                        if(controller!= null)
                        {
                            controller.ReceiveDamage(_modelWrapper.damage);
                        }
                    }
                break;
            default:
                break;
            }

            _elapsedTime = 0;
        }

        

    }

    private void CancelAttack()
    {
        _isAttacking = false;
    }

    public override void ReceiveDamage(int damage)
    {
        if (!_isDying)
        {
            _modelWrapper.SetNewHealth(damage);

        }
    }

    public void SubscribeDeath(Action method) {
        _modelWrapper.Death += method;
    }
}
