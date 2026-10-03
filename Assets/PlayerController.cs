using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 5f;

    Rigidbody rb;
    bool isGrounded;
    bool jumpQueued;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            jumpQueued = true;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0f, v);
        move = Vector3.ClampMagnitude(move, 1f) * speed;

        rb.linearVelocity = new Vector3(
            move.x,
            rb.linearVelocity.y,
            move.z
        );

        if (jumpQueued)
        {
            rb.AddForce(
                Vector3.up * jumpForce,
                ForceMode.Impulse
            );

            jumpQueued = false;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.GetContact(0).normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}