using UnityEngine;

// La version couplée : l'item connaît le gestionnaire, l'appelle par son nom,
// et ne sait rien faire sans lui. C'est ce sens de dépendance qu'on inverse.
[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    [SerializeField] private ItemManager manager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        manager.ItemCollected(this);
        gameObject.SetActive(false);
    }

    public void Restore() => gameObject.SetActive(true);
}
