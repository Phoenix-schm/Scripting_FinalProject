using TMPro;
using UnityEngine;
using System;
using UnityEngine.Events;
using System.Collections;

public class PlayerStatusUI : MonoBehaviour
{
    [SerializeField] private PlayerVariables playerVariables;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private GameObject hurtOverlay;

    public UnityEvent onPlayerDeath;

    private void Start()
    {
        healthText.text += playerVariables.health;
    }

    public void UpdatePlayerHealthText()
    {
        // make it so that when the player is damaged the health updates
        healthText.text = "Health: ";
        healthText.text += playerVariables.health;

        if (playerVariables.health <= 0)
        {
            onPlayerDeath?.Invoke();
        }
    }

    public void ShowHurtOverlay()
    {
        hurtOverlay.SetActive(true);
        StartCoroutine("ShowHurtOverlayTimer", 2);
    }

    IEnumerator ShowHurtOverlayTimer(int timer)
    {
        yield return new WaitForSeconds(timer);
        hurtOverlay.SetActive(false);
    }
}
