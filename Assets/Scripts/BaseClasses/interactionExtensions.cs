using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class interactionExtensions
{
    public static bool IsInteractable(this RaycastHit2D hit)
    {
        return hit.transform.GetComponent<interactableBase>();
    }

    public static void Interact(this RaycastHit2D hit, GameObject player)
    {
        hit.transform.GetComponent<interactableBase>().interact(player);
    }
}
