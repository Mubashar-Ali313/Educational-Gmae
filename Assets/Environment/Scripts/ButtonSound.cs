using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public Button targetButton;        // Button jahan script lagana hai
    public AudioSource audioSource;    // Audio Source component
    public AudioClip clickSound;       // Sound jo bajani hai

    void Start()
    {
        if (targetButton != null)
        {
            targetButton.onClick.AddListener(PlaySound);
        }
    }

    void PlaySound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
