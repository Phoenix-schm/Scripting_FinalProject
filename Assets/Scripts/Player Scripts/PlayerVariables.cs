using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "Player")]
public class PlayerVariables : ScriptableObject
{
    [Header("Player Stats")]
    public int maxHealth;
    public int health;
    public int speed;
}
