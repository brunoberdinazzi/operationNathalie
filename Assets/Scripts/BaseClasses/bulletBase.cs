using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class bulletBase : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected LayerMask layerEnemy;
    protected Vector2 direction;
    protected float bulletSpeed;
    protected float bulletTime;
}