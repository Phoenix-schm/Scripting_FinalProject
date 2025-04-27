using UnityEngine;

[CreateAssetMenu(fileName = "PickUpItem", menuName = "Scriptable Objects/PickUp Item Data")]
public class PickUpItemData : ScriptableObject
{
    public string itemId;
    public string displayName;
    public Sprite icon;
    public GameObject prefab;

    public int iconGridWidth;
    public int iconGridHeight;
}
