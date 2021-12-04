using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UnitView : MonoBehaviour
{
    public Image buttonCooldown;
    public Button shootButton;
    public TextMeshProUGUI healthText;
    public BoxCollider2D col;
    public Animator animator;

    public void PlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    public void SetHealthText(int health)
    {
        healthText.text = health.ToString();
    }

    public void UpdateShootButton(float attackRate)
    {
        if (shootButton.interactable == false)
        {

            buttonCooldown.fillAmount -= Time.deltaTime * attackRate;
            if (buttonCooldown.fillAmount <= 0)
            {
                ToggleShootButton(true);
            }
        }

    }

    public void ToggleShootButton(bool On)
    {
        if (On)
        {
            shootButton.interactable = true;
            buttonCooldown.enabled = false;
        }
        else
        {
            shootButton.interactable = false;
            buttonCooldown.enabled = true;
            buttonCooldown.fillAmount = 1;
        }
    }

    public void ToggleGameUnit(bool On)
    {
        if (On)
        {
            shootButton.gameObject.SetActive(true);

            col.enabled = true;

        }
        else
        {
            shootButton.gameObject.SetActive(false);

            col.enabled = false;

        }
    }

}
