using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class weaponBase : MonoBehaviour
{
    protected bool isReloading;
    protected int currentMagazine;
    protected int magazineMaxSize;
    protected int totalAmmo;

    protected int damage;
    protected float spread;
    
    public abstract void shoot();

    public abstract void reload();
}