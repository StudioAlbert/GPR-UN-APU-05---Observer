using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Observateur des items, puis sujet à son tour — de trois façons différentes,
// une par ligne du tableau « laquelle choisir ? ».
public class ItemManager : MonoBehaviour
{
    [Header("Les items à ramasser, posés à la main")]
    [SerializeField] private List<Item> items = new List<Item>();

    [Header("3 · Canaux : les autres systèmes écoutent sans nous connaître")]
    [SerializeField] private IntEventChannelSO remainingChannel;
    [SerializeField] private VoidEventChannelSO itemCollectedChannel;

    [Header("2 · UnityEvent : ce que l'Inspector câble")]
    [SerializeField] private UnityEvent allCollected;
    [SerializeField] private UnityEvent reset;

    private int remaining;

    void Awake() => remaining = items.Count;

    // 1 · event Action : on s'abonne à chaque item, par une méthode nommée.
    void OnEnable()
    {
        foreach (Item item in items) item.Collected += OnItemCollected;
    }

    void OnDisable()
    {
        foreach (Item item in items) item.Collected -= OnItemCollected;
    }

    // La première annonce part dans Start, pas dans OnEnable : à ce moment-là
    // tout le monde s'est abonné, et personne ne rate le compte de départ.
    void Start() => remainingChannel.Raise(remaining);

    private void OnItemCollected(Item item)
    {
        remaining--;

        itemCollectedChannel.Raise();
        remainingChannel.Raise(remaining);

        if (remaining == 0) allCollected.Invoke();
    }

    // Appelée directement par le bouton de l'UI : c'est le jeu qui pilote le jeu.
    // Ce qu'elle rend public, c'est l'action — pas le compte, ni la liste.
    public void ResetItems()
    {
        foreach (Item item in items) item.Restore();
        remaining = items.Count;

        reset.Invoke();
        remainingChannel.Raise(remaining);
    }
}
