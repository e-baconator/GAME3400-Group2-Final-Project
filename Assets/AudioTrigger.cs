using UnityEngine;

public class AudioTriggerZone : MonoBehaviour
{
    public AudioSource clip;

    private void OnTriggerEnter(Collider colllider) 
    {
        if (colllider.CompareTag("Player")) 
        {
            if (clip != null && !clip.isPlaying)
            {
                clip.Play();
            }
        }
    }
}
