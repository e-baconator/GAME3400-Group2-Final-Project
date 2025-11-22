using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;

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

    public bool activated = false;

    private void Start()
    {
        UIText = GameObject.FindGameObjectWithTag("UIText").GetComponent<TextMeshProUGUI>();
        leftArm = GameObject.FindGameObjectWithTag("LeftArm").GetComponent<ArmControl>();
        rightArm = GameObject.FindGameObjectWithTag("RightArm").GetComponent<ArmControl>();
        if (GetComponent<AudioSource>() != null)
            source = GetComponent<AudioSource>();
        else
            source = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        if (iclips.Length != 0)
            source.clip = iclips[0];
    }

    public void Interact()
    {
        if (obj == "ID Card")
        {
            transform.parent = leftArm.transform;
            leftArm.active = true;
            transform.localPosition = new Vector3(0, -0.68f, 0.033f);
            transform.localRotation = Quaternion.Euler(77.579f, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);
            activated = true;
            StartCoroutine(DisplayText());
        }

        if (obj == "Locker")
        {
            if (leftArm.raised)
            {
                moveable.localRotation = Quaternion.Euler(90, 150, 0);
                moveable.localPosition = new Vector3(0.61f, 0, -0.309f);
                enableObject.SetActive(true);
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
    }

    private IEnumerator DisplayText()
    {
        UIText.text = itext;
        yield return new WaitForSeconds(5);
        if (UIText.text == itext)
            UIText.text = "";
    }
}
