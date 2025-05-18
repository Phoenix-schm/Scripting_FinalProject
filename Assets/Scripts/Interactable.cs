using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string displayName;
    public string interactText;
    public abstract void Interact(PlayerInteraction player);
}
