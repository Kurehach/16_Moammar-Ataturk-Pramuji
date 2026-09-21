using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float kecepatan = 5f;
    private Vector2 arahGerak;
    private Rigidbody2D rb;

    InputAction moveAction;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
    }
    
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (moveAction != null)
        {
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            arahGerak = moveValue.normalized;
            rb.linearVelocity = arahGerak * kecepatan;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Koin"))
        {
            GameManager gameManager = FindAnyObjectByType<GameManager>();
            if (gameManager != null)
            {
                gameManager.AmbilKoin();
            }
            Destroy(other.gameObject);
        }
    }
}
