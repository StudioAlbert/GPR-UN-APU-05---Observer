using System;
using UnityEngine;

// Le sujet. Il annonce qu'on l'a ramassé, et c'est tout ce qu'il fait :
// il ne connaît ni le compteur, ni le son, ni le panneau de fin.
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

    // L'item se cache tout seul en se faisant ramasser ; il se remontre tout seul.
    public void Restore() => gameObject.SetActive(true);
}
