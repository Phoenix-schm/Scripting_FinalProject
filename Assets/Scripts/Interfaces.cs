using UnityEngine;

public interface IInteractable
{
    void Interact(PlayerInteraction player);
}

public interface IPickUpable
{
    public void PickUp(PlayerInteraction player);
}

public interface IRemovable
{
    public void RemoveItem(PlayerVariables player, PickUpItemData itemData);
}

public interface IUsable
{
    public void Use(PlayerVariables player);
}