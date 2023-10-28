using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : interactableBase
{
    public bool isLocked;

    private bool isOpen;
    private bool isOpening;

    void Start()
    {
        isOpen = false;
        isLocked = false;
    }

    public override void interact(GameObject player)
    {
        if (!isLocked)
        {
            if (isOpen && !isOpening)
            {
                isOpening = true;
                closeDoor();
            }
            else
            {
                isOpening = true;
                openDoor();
            }
        }
    }

    private void lockDoor()
    {
        isLocked = true;
    }

    private void unlockDoor()
    {
        isLocked = false;
    }

    private void openDoor()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        isOpen = true;
        isOpening = false;
    }

    private void closeDoor()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        isOpen = false;
        isOpening = false;
    }
}
