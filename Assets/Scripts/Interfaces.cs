using UnityEngine;

public interface IInteractable
{
    void Interact(PlayerInteraction player);
}

public interface IPickUpable
{
    public void PickUpItem(PlayerInteraction player);
}
