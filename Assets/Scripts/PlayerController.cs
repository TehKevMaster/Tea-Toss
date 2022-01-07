using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    #region Variables
    Rigidbody2D body;

    [SerializeField] float horizontalInput;
    [SerializeField] float verticalInput;
    [SerializeField] float moveLimiter = 0.7f;

    [SerializeField] float speed = 20.0f;

    #endregion

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        GrabMovementInputValues();
    }

    void FixedUpdate()
    {
        MovePlayer();

    }

    void GrabMovementInputValues()
    {
        // Gives a value between -1 and 1
        horizontalInput = Input.GetAxisRaw("Horizontal"); // -1 is left, 1 is right
        verticalInput = Input.GetAxisRaw("Vertical"); // -1 is down, 1 is up
    }

    void MovePlayer()
    {
        if (horizontalInput != 0 && verticalInput != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontalInput *= moveLimiter;
            verticalInput *= moveLimiter;
        }

        body.velocity = new Vector2(horizontalInput * speed, verticalInput * speed);
    }

}
