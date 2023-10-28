using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistol : weaponBase
{
    public projetil bullet;
    public Transform bulletSpawnPoint;
    public float fireRate;

    private float nextFire;

    void Start()
    {
        isReloading = false;
        magazineMaxSize = 10;
        currentMagazine = magazineMaxSize;
        totalAmmo = 999;
        damage = 10;
        spread = .5f;
        nextFire = 0f;
        fireRate = .25f;
    }

    public override void shoot()
    {
        /*
        * fireRate is the quantity of time between each shot
        * so, to set fireRate, we need to check first, how many shots a weapon will shoot in a second
        * and then, calculate the quantity of time between each shot in milliseconds
        * Example: if a weapon shoots 4 bullets in a second, then fireRate = 1 / 4 = .25f
        */
        if(!isReloading && Time.time > nextFire && currentMagazine > 0)
        {
            // Update the time when our player can fire next
            nextFire = Time.time + fireRate;
            // Create spawnPoint on the prefab
            bulletSpawnPoint = transform;
            // Calculate offset to aplly on the rotation of the bullet
            Vector3 offset = new Vector3(0.5f, 0.5f, 0f);
            Instantiate(bullet, bulletSpawnPoint.position + offset, bulletSpawnPoint.rotation);

            currentMagazine--;
        }
    }

    public override void reload()
    {
        isReloading = true;
        int difference = magazineMaxSize - currentMagazine;
        if (difference > 0 && totalAmmo >= difference)
        {
            currentMagazine += difference;
            totalAmmo -= difference;
        }
        isReloading = false;
    }
}
