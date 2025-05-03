using UnityEngine;

public interface IInteractable
{
    void Interact(PlayerInteraction player);
}

public interface IUsable
{
    public void Use(PlayerVariables player);
}