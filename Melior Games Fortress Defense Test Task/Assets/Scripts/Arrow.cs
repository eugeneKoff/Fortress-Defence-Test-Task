using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private int _damage;

    private Transform _target;

    public int speed;

    private Vector3 direction;
    public Rigidbody2D rb;

    public void SetUpArrow(int damage, Transform target)
    {
        _damage = damage;

        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        else {
            _target = target;
        }

       

    }

    

    private void Update()
    {

        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        direction = (_target.position - transform.position).normalized;

        RotateTowardsTarget();
        
    }

    private void FixedUpdate()
    {
        MoveToTarget();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Controller enemyController;

            if (collision.transform.parent.TryGetComponent(out enemyController))
            {
                enemyController.ReceiveDamage(_damage);


                print("damage: " + _damage);
            }
            Destroy(gameObject);


        }

    }
    

    private void MoveToTarget()
    {
        rb.MovePosition(transform.position + direction * Time.fixedDeltaTime * speed);

    }

    private void RotateTowardsTarget()
    {
        var offset = 90f;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));

    }
}
