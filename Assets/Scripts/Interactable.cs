using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string interactText;
    public string displayName;
    public bool isOpen;
    public abstract void Interact(PlayerInteraction player);
}
