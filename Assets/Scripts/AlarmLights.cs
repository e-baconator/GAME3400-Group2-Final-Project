using UnityEngine;

public class AlarmLights : MonoBehaviour
{
    private float intensity = .1f;
    private bool increasing = true;

    // Update is called once per frame
    void Update()
    {
        if (increasing)
            intensity += Time.deltaTime * 11000;
        else
            intensity -= Time.deltaTime * 11000;

        if (intensity >= 15000)
            increasing = false;
        if (intensity <= .1f)
            increasing = true;

        GetComponent<Light>().intensity = intensity;
    }
}
