using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    private Rigidbody rb;

    public float lookSense = 1.5f;
    private float moveSpeed = 4f;
    private Vector2 look;
    private Vector3 move;

    private float horizontalInput, verticalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        look.x = transform.eulerAngles.y;
        look.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        look.x += Input.GetAxisRaw("Mouse X") * lookSense;
        look.y += Input.GetAxisRaw("Mouse Y") * lookSense;
        look.y = Mathf.Clamp(look.y, -90, 90);

        cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0);
        transform.rotation = Quaternion.Euler(0, look.x, 0);

        horizontalInput = Input.GetAxisRaw("Horizontal") * moveSpeed;
        verticalInput = Input.GetAxisRaw("Vertical") * moveSpeed;
        move = transform.forward * verticalInput + transform.right * horizontalInput;
        move.y = rb.linearVelocity.y;
        rb.linearVelocity = move;
    }
}
