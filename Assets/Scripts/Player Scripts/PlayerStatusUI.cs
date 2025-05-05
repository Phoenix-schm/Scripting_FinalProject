using TMPro;
using UnityEngine;

public class PlayerStatusUI : MonoBehaviour
{
    [SerializeField] private PlayerVariables playerVariables;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private GameObject hurtOverlay;
    private void Start()
    {
        healthText.text += playerVariables.health;
    }
    public void UpdatePlayerHealthText(int healthAmount)
    {
        healthText.text = "Health: ";
        healthText.text += healthAmount;
    }

    public void ShowHurtOverlay(bool setActive)
    {
        hurtOverlay.SetActive(setActive);
    }
}
