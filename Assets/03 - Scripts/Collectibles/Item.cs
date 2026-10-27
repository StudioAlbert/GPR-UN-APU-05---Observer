using UnityEngine;

// The coupled version: the item knows the manager, calls it by name, and can do
// nothing without it. That direction of dependency is the one we invert.
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
