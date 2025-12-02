using UnityEngine;

public class BabyAlienAudio : MonoBehaviour
{
   public AudioSource pickupAudio;
   public AudioSource dropAudio;

    public void PlayPickupSound()
    {
        if (pickupAudio != null)
        {
            pickupAudio.Play();
        }
    }

    public void PlayDropSound()
    {
        if (dropAudio != null)
        {
            dropAudio.Play();
        }
    }
}
