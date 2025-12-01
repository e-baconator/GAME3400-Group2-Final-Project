using System;
using System.Drawing;
using UnityEngine;

public class ArmControl : MonoBehaviour
{
    [SerializeField] private string key;

    private Transform arm;
    
    private float angle = -50;

    public bool raised = false;
    private bool moving = false;
    public bool active = true;

    private void Start()
    {
        arm = GetComponent<Transform>();
    }

    void Update()
    {
        arm.localRotation = Quaternion.Euler(angle, 0, 0);

        if (Input.GetKeyDown(key) && !moving && active)
        {
            moving = true;
        }

        if (moving)
        {
            if (!raised)
                angle -= Time.deltaTime * 180;
            else
                angle += Time.deltaTime * 180;
            angle = Mathf.Clamp(angle, -90, -50);
            if (angle == -50)
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
