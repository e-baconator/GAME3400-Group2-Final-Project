using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.ProBuilder.Shapes;

public class Interactable : MonoBehaviour
{
    [SerializeField] private BoxCollider enableObject;
    [SerializeField] private Transform moveable;
    [SerializeField] public string itext;
    [SerializeField] private AudioClip[] iclips;
    private TextMeshProUGUI UIText;
    private AudioSource source;
    private ArmControl leftArm, rightArm;

    [SerializeField] private string obj;

    private float angle, babySound;

    [SerializeField] private bool trigger;
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
        babySound = 15;
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
                enableObject.enabled = true;
            }
            else
            {
                source.clip = iclips[1];
                source.Play();
            }
        }

        if (obj == "Gas Mask" && !activated)
        {
            activated = true;
            source.Play();
            StartCoroutine(NextLine(1, 1, 1, 0));
            transform.localScale = new Vector3(0, 0, 0);
        }

        if (obj == "Baby Alien" && !activated)
        {
            if (!rightArm.raised)
            {
                StartCoroutine(DisplayText());
                rightArm.active = true;
                source.Play();
            }
            else
            {
                source.clip = iclips[1];
                source.Play();
                transform.parent = rightArm.transform;
                transform.localPosition = new Vector3(-0.0638479963f, -0.251049995f, -0.233089998f);
                transform.localRotation = Quaternion.Euler(304.517181f, 291.663849f, 217.653107f);
                transform.localScale = new Vector3(0.189969525f, 0.189969525f, 0.189969525f);
                carrying = true;
                activated = true;
                StartCoroutine(NextLine(2, 1, 2, 0));
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
                StartCoroutine(NextLine(1, 1, 1, 0));
            }
        }

        if (obj == "Elevator Button")
        {
            source.Play();
            activated = true;
            StartCoroutine(NextLine(1, 1, 1, 0));
        }

        if (obj == "Vent")
        {
            if (carrying)
            {
                transform.parent = null;
                carrying = false;
            }
        }

        if (obj == "Cry Trigger")
        {
            source.Play();
            StartCoroutine(NextLine(2, 2, 1, 2));
            activated = true;
        }

        if (obj == "Roar Trigger")
        {
            source.Play();
            StartCoroutine(NextLine(4, 1, 1, 0));
            activated = true;
        }

        if (obj == "Ending Trigger")
        {
            source.Play();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().standing = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().fadeOut = true;
            StartCoroutine(NextLine(5, 1, 1, 0));
            activated = true;
            carrying = false;
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

        if (obj == "Baby Alien" && carrying)
        {
            babySound -= Time.deltaTime;
            if (babySound <= 0)
            {
                source.clip = iclips[Random.Range(0, 1)];
                source.Play();
                babySound = 15;
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

    private IEnumerator NextLine(int t, int n, int s, int d)
    {
        while (n > 0)
        {
            yield return new WaitForSeconds(t);
            source.pitch = 1;
            source.clip = iclips[s];
            source.Play();
            s++;
            n--;
            t += d;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (trigger && !activated && other.gameObject.CompareTag("MainCamera"))
        {
            Interact();
        }
    }
}
