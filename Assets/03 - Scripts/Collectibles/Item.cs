using UnityEngine;

// Version couplée : l'item tient une référence sur le gestionnaire et l'appelle
// lui-même. Ça marche, et c'est précisément ce qu'on va démonter.
[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    [SerializeField] private ItemManager manager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        manager.ItemRamasse(this);
        gameObject.SetActive(false);
    }
}
