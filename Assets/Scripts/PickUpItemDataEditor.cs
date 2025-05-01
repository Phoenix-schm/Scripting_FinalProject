using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PickUpItemData))]
public class PickUpItemDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var dataScript = target as PickUpItemData;
        bool isOtherDataHidden = false;

        using (new EditorGUI.DisabledGroupScope(isOtherDataHidden))
        {
            dataScript.itemId = EditorGUILayout.TextField("Item ID", dataScript.itemId);
            dataScript.displayName = EditorGUILayout.TextField("Display Name", dataScript.displayName);
            dataScript.icon = (Sprite)EditorGUILayout.ObjectField("Icon", dataScript.icon, typeof(Sprite), true);
            dataScript.prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", dataScript.prefab, typeof(GameObject), true);
            dataScript.amountInInventory = EditorGUILayout.IntField("Amount In Inventory", dataScript.amountInInventory);
        }
        dataScript.type = (PickUpItemData.PickUpTypes)EditorGUILayout.EnumPopup(dataScript.type);

        bool isHealthHidden = true;
        bool isAmmoAmountHidden = true;
        switch (dataScript.type)
        {
            case PickUpItemData.PickUpTypes.Health:
                isHealthHidden = false;
                using (new EditorGUI.DisabledGroupScope(isHealthHidden))
                {
                    dataScript.healAmount = EditorGUILayout.IntField("Heal Amount", dataScript.healAmount);
                }
                break;
            case PickUpItemData.PickUpTypes.Ammo:
                isAmmoAmountHidden = false;
                using (new EditorGUI.DisabledGroupScope(isAmmoAmountHidden))
                {
                    dataScript.ammoAmount = EditorGUILayout.IntField("Ammo Amount", dataScript.ammoAmount);
                }
                break;
            default:
                isHealthHidden = true;
                isAmmoAmountHidden = true;
                break;
        }
    }
}
