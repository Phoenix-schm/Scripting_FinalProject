using UnityEngine;

public interface IInteractable
{
    void Interact(PlayerInteraction player);
}

public interface IPickUpable
{
    public void PickUp(PlayerVariables player);
}

public interface IRemovable
{
    public void RemoveItem(PlayerVariables player);
}
