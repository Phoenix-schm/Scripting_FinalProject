using UnityEngine;

[CreateAssetMenu(fileName = "PickUpItem", menuName = "Scriptable Objects/Enemy Data")]

public class EnemyData : ScriptableObject
{
    public int maxHealth;
    public int health;

    public int speed;
    public int damage;

    public GameObject monsterPrefab;
    public GameObject dropLoot;
    public int dropAmount;

    public EnemyTier tier;
}

public enum EnemyTier
{
    LowTier,
    MediumTier,
    HighTier
}
