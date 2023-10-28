using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public int crouchSpeed;
    public int defaultSpeed;
    public int health;
    public int jumpForce;
    public int runningSpeed;
    public float groundRadius;
    public LayerMask ground;
    public Rigidbody2D rigidBody;
    public Transform groundCheck;
    public weaponBase activeWeapon;

    private bool canSprint;
    private bool crouchPressed;
    private bool interactPressed;
    private bool jumpPressed;
    private bool isLookingRight;
    private bool reloadPressed;
    private bool firePressed;
    private bool sprintPressed;
    private bool onGround;
    private int movementSpeed;
    private float interactionRadius;
    private float direction;
    private float rotationZ;
    private BoxCollider2D boxCollider;
    private Vector3 facingRight;
    private Vector3 facingLeft;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();

        facingRight = transform.localScale;
        facingLeft = transform.localScale;
        facingLeft.x = facingLeft.x * -1;
        interactionRadius = 1f;
        rotationZ = 0;
    }

    void Update()
    {
        direction = Input.GetAxis("Horizontal");
        crouchPressed = Input.GetButton("Crouch");
        jumpPressed = Input.GetButton("Jump");
        sprintPressed = Input.GetButton("Sprint");
        firePressed = Input.GetButton("Fire1");

        interactPressed = Input.GetButtonDown("Interact");
        reloadPressed = Input.GetButtonDown("Reload");

        isLookingRight = (rotationZ > -90 && rotationZ < 90);
        canSprint = sprintPressed && ((direction > 0 && isLookingRight) || (direction < 0 && !isLookingRight));

        onGround = Physics2D.OverlapCircle(groundCheck.position, groundRadius, ground);

        handleAttack();
        handleInteraction();
        handleReload();
    }

    void FixedUpdate()
    {
        handleJump();
        handleMovement();
    }

    void handleAttack()
    {
        Vector3 directionOfMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rotationZ = Mathf.Atan2(directionOfMouse.y, directionOfMouse.x) * Mathf.Rad2Deg;
        activeWeapon.transform.rotation = Quaternion.AngleAxis(rotationZ, Vector3.forward);

        if (firePressed)
        {
            activeWeapon.shoot();
        }
    }

    void handleInteraction()
    {
        if (interactPressed)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, interactionRadius, Vector2.zero);

            if (hits.Length > 0)
            {
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.IsInteractable())
                    {
                        hit.Interact(gameObject);
                        return;
                    }
                }
            }
        }
    }

    void handleMovement()
    {
        Vector3 targetVelocity = new Vector2(direction * movementSpeed, rigidBody.velocity.y);
        rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref targetVelocity, 0.03f);

        if (isLookingRight)
        {
            transform.localScale = facingRight;
        }
        else
        {
            transform.localScale = facingLeft;
        }

        if (onGround)
        {
            if (canSprint)
            {
                movementSpeed = runningSpeed;
            }
            else
            {
                movementSpeed = defaultSpeed;
            }

            if (crouchPressed)
            {
                movementSpeed = crouchSpeed;
                boxCollider.enabled = false;
            }
            else
            {
                boxCollider.enabled = true;
            }
        }
    }

    void handleJump()
    {
        if (jumpPressed && onGround)
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void handleReload()
    {
        if (reloadPressed)
        {
            activeWeapon.reload();
        }
    }

    void die(){
        // Do some stuff
        Debug.Log("Player died");
    }

    void takeDamage(int damage)
    {
        if (health - damage > 0)
        {
            // Apply armour damage
            health -= damage;
        }
        else
        {
            die();
        }
    }

}
