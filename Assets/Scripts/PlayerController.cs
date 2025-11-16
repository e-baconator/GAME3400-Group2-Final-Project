using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject arms;
    [SerializeField] private Image fadeIn;

    [SerializeField] private Transform cameraTransform;
    private Rigidbody rb;
    private CapsuleCollider cc;

    public float lookSense = 1.5f;
    private float moveSpeed = 5.5f;
    private Vector2 look;
    private Vector3 move;

    private float horizontalInput, verticalInput;

    private bool standing, wakeUp, sitUp, standUp;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        look.x = transform.eulerAngles.y;
        look.y = 90;
        fadeIn.enabled = true;
        StartCoroutine(getUp());
    }

    // Update is called once per frame
    void Update()
    {
        if (standing)
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

        if (wakeUp)
        {
            fadeIn.color = new Color(0, 0, 0, fadeIn.color.a - Time.deltaTime / 2.5f);
        }

        if (sitUp)
        {
            look.y -= Time.deltaTime * 120;
            transform.rotation = Quaternion.Euler(-look.y, look.x, 0);
        }

        if (standUp)
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * 1f, transform.position.z);
    }

    private IEnumerator getUp()
    {
        yield return new WaitForSeconds(1);
        wakeUp = true;
        yield return new WaitForSeconds(2.5f);
        wakeUp = false;
        yield return new WaitForSeconds(3);
        sitUp = true;
        yield return new WaitForSeconds(.75f);
        sitUp = false;
        transform.rotation = Quaternion.Euler(0, look.x, 0);
        yield return new WaitForSeconds(1.5f);
        standUp = true;
        yield return new WaitForSeconds(1.5f);
        standUp = false;
        rb.isKinematic = false;
        cc.enabled = true;
        arms.SetActive(true);
        standing = true;
    }
}
