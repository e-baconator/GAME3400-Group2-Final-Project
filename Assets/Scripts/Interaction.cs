using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Interaction : MonoBehaviour
{
    private Interactable current;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && current != null && !current.activated)
        {
            if (current != null)
                current.Interact();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject g = other.gameObject;
        Interactable c = g.GetComponent<Interactable>();
        if (c != null && !c.activated)
            current = c;
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject g = other.gameObject;
        Interactable c = g.GetComponent<Interactable>();
        if (c != null)
            current = null;
    }
}

