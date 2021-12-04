using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FortView : MonoBehaviour
{
    public Sprite damage0;
    public Sprite damage1;
    public Sprite damage2;
    public Sprite damage3;

    public SpriteRenderer spriteRenderer;

    public Image buttonCooldown;
    public Button shootButton;
    public TextMeshProUGUI healthText;

    private void Start()
    {
        spriteRenderer.sprite = damage0;
    }

    

    public void SetHealthText(int health)
    {
        healthText.text = health.ToString();
    }

    public void UpdateFortImage(int imageIndex)
    {
        switch (imageIndex)
        {
            case 0: spriteRenderer.sprite = damage0;
                    break;
            case 1:
                spriteRenderer.sprite = damage1;
                break;
            case 2:
                spriteRenderer.sprite = damage2;
                break;
            case 3:
                spriteRenderer.sprite = damage3;
                break;
            default:
                break;
        }
    }

    public void UpdateShootButton(float attackRate)
    {
        if(shootButton.interactable == false)
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
        if(On)
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
}
