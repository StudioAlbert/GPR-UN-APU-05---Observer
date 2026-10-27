using UnityEngine;

// Added afterwards, without opening Item or ItemManager: all it takes is
// listening to the same channel. That is the workshop's success criterion.
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO itemCollectedChannel;
    [SerializeField] private AudioClip pickupClip;

    private AudioSource source;

    void Awake() => source = GetComponent<AudioSource>();

    void OnEnable() => itemCollectedChannel.Raised += PlayPickup;

    void OnDisable() => itemCollectedChannel.Raised -= PlayPickup;

    private void PlayPickup() => source.PlayOneShot(pickupClip);
}
