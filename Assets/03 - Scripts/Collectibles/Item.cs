using System;
using UnityEngine;

// The subject. It announces that it has been picked up, and that is all it does:
// it knows nothing of the counter, the sound, or the end panel.
[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    public event Action<Item> Collected;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Collected?.Invoke(this);
        gameObject.SetActive(false);
    }

    // The item hides itself when picked up, so it shows itself again on its own.
    public void Restore() => gameObject.SetActive(true);
}
