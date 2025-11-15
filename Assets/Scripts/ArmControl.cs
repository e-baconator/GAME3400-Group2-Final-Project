using System.Drawing;
using UnityEngine;

public class ArmControl : MonoBehaviour
{
    [SerializeField] private int mouseButton;
    private Transform arm;
    private bool raised, moving = false;
    private float angle = -55;

    private void Start()
    {
        arm = GetComponent<Transform>();
    }

    void Update()
    {
        arm.localRotation = Quaternion.Euler(angle, 0, 0);

        if (Input.GetMouseButtonDown(mouseButton) && moving == false)
        {
            moving = true;
        }

        if (moving)
        {
            if (!raised)
                angle -= Time.deltaTime * 180;
            else
                angle += Time.deltaTime * 180;
            angle = Mathf.Clamp(angle, -90, -55);
            if (angle == -55)
            {
                raised = false;
                moving = false;
            }

            if (angle == -90)
            {
                raised = true;
                moving = false;
            }
        }
    }
}
