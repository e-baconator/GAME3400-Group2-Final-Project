using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private GameObject leftArm, rightArm;

    [SerializeField] public string itext;
    [SerializeField] private AudioClip iclip;
    private TextMeshProUGUI UIText;
    private AudioSource source;

    [SerializeField] private string obj;

    public bool activated = false;

    private void Start()
    {
        UIText = GameObject.FindGameObjectWithTag("UIText").GetComponent<TextMeshProUGUI>();
        leftArm = GameObject.FindGameObjectWithTag("LeftArm");
        rightArm = GameObject.FindGameObjectWithTag("RightArm");
        if (GetComponent<AudioSource>() != null)
        {
            source = GetComponent<AudioSource>();
            source.clip = iclip;
        }
    }

    public void Interact()
    {
        if (itext.Length > 0)
            StartCoroutine(DisplayText());

        if (iclip != null)
            source.Play();

        if (obj == "ID Card")
        {
            transform.parent = leftArm.transform;
            leftArm.GetComponent<ArmControl>().active = true;
            transform.localPosition = new Vector3(0, -0.68f, 0.033f);
            transform.localRotation = Quaternion.Euler(77.579f, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);
            activated = true;
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
