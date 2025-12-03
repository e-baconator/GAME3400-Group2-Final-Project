using UnityEngine;

public class AlarmLights : MonoBehaviour
{
    private float intensity = .1f;
    private bool increasing = true;

    // Update is called once per frame
    void Update()
    {
        if (increasing)
            intensity += Time.deltaTime * 6;
        else
            intensity -= Time.deltaTime * 6;

        if (intensity >= 10)
            increasing = false;
        if (intensity <= .1f)
            increasing = true;

        GetComponent<Light>().intensity = intensity;
    }
}
