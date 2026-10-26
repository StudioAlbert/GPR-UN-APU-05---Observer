using UnityEngine;

// Ajouté après coup, sans ouvrir ni Item ni ItemManager : il lui suffit
// d'écouter le même canal. C'est le critère de réussite de l'atelier.
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
