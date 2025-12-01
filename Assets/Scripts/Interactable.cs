using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject enableObject;

    [SerializeField] private Transform moveable;
    [SerializeField] public string itext;
    [SerializeField] private AudioClip[] iclips;
    private TextMeshProUGUI UIText;
    private AudioSource source;
    private ArmControl leftArm, rightArm;

    [SerializeField] private string obj;

    private float angle;

    public bool activated = false;
    private bool carrying, openDoor;

    private void Start()
    {
        UIText = GameObject.FindGameObjectWithTag("UIText").GetComponent<TextMeshProUGUI>();
        leftArm = GameObject.FindGameObjectWithTag("LeftArm").GetComponent<ArmControl>();
        rightArm = GameObject.FindGameObjectWithTag("RightArm").GetComponent<ArmControl>();
        if (GetComponent<AudioSource>() != null)
            source = GetComponent<AudioSource>();
        else
            source = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        if (moveable != null)
            angle = moveable.localRotation.y;
    }

    public void Interact()
    {
        if (iclips.Length != 0)
            source.clip = iclips[0];

        if (obj == "ID Card")
        {
            transform.parent = leftArm.transform;
            leftArm.active = true;
            transform.localPosition = new Vector3(0.0130000003f, -0.617999971f, 0.0140000004f);
            transform.localRotation = Quaternion.Euler(357.414185f, 344.142914f, 282.855072f);
            transform.localScale = new Vector3(1.4f, 1, 1.4f);
            activated = true;
            StartCoroutine(DisplayText());
        }

        if (obj == "Locker")
        {
            if (leftArm.raised)
            {
                source.Play();
                openDoor = true;
                activated = true;
                //enableObject.SetActive(true);
            }
            else
            {
                print("Gotta get my ID Card fr");
                //source.Play();
            }
        }

        if (obj == "Gas Mask")
        {
            //source.Play();
            Destroy(gameObject);
        }

        if (obj == "Baby")
        {
            if (!rightArm.raised)
            {
                StartCoroutine(DisplayText());
                rightArm.active = true;
            }
            else
            {
                transform.parent = rightArm.transform;
                transform.localPosition = new Vector3(0, -0.68f, 0.033f);
                transform.localScale = new Vector3(1, 1, 1);
                carrying = true;
            }
        }

        if (obj == "Door")
        {
            if (leftArm.raised && !activated)
            {
                openDoor = true;
                activated = true;
                source.Play();
            }
        }

        if (obj == "Locked Door")
        {
            if (leftArm.raised && !activated)
            {
                source.Play();
                activated = true;
            }
        }
    }

    private void Update()
    {
        if (obj == "Door" && openDoor)
        {
            moveable.localRotation = Quaternion.Euler(0, angle, 0);
            angle -= Time.deltaTime * 135;
            if (angle <= -105)
            {
                openDoor = false;
            }
        }

        if (obj == "Locker" && openDoor)
        {
            moveable.localRotation = Quaternion.Euler(-90, angle, 0);
            angle += Time.deltaTime * 225;
            if (angle >= 180)
            {
                openDoor = false;
            }
        }
    }

    private IEnumerator DisplayText()
    {
        UIText.text = itext;
        yield return new WaitForSeconds(5);
        if (UIText.text == itext)
            UIText.text = "";
    }
}
