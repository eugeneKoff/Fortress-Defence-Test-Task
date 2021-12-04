using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyView : MonoBehaviour
{
    public Animator animator;

    public TextMeshProUGUI healthText;

    public event Action DeathAnimPlayed;

    public event Action<RaycastHit2D> TargetDetected;

    public event Action PathClear;

    public LayerMask layersToDetect;

    public Rigidbody2D rb;

    public void PlayDeathAnimation()
    {
        animator.SetTrigger("Death");
    }

    public void PlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    private void OnDeathAnimPlayed()
    {
        DeathAnimPlayed?.Invoke();
    }

    public void SetHealthText(int health)
    {
        healthText.text = health.ToString();
    }

    public void Move(float speed)
    {
        //transform.Translate(Vector2.right * Time.deltaTime * speed);

        //transform.position += (Vector3) Vector2.right * Time.deltaTime * speed;

        rb.MovePosition(rb.position + Vector2.right * Time.fixedDeltaTime * speed);
    }

    public void DetectEnemy(float distance)
    {
        print("detecting...");

        RaycastHit2D hit;

        hit = Physics2D.Linecast(rb.position, rb.position + Vector2.right * distance, layersToDetect);

        Collider2D col = Physics2D.OverlapCircle(transform.position, distance, layersToDetect);


        if (hit)
        {
            TargetDetected?.Invoke(hit);
        }
        else
        {
            PathClear?.Invoke();
        }


    }
}
