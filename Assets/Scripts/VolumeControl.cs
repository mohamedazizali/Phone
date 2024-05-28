using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;

    void Start()
    {
        // Ensure that volumeSlider and audioSource are assigned in the Inspector
        if (volumeSlider == null || audioSource == null)
        {
            Debug.LogError("VolumeSlider or AudioSource is not assigned!");
            return;
        }

        // Set the slider value to the current volume
        volumeSlider.value = audioSource.volume;
    }

    public void UpdateVolume()
    {
        // Update the volume of the audio source based on the slider value
        audioSource.volume = volumeSlider.value;
    }
}
