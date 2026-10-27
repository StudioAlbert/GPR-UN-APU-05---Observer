using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Observer of the items, then subject in its turn — in three different ways,
// one per row of the "which one should I pick?" table.
public class ItemManager : MonoBehaviour
{
    [Header("The items to collect, wired by hand")]
    [SerializeField] private List<Item> items = new List<Item>();

    [Header("3 - Channels: other systems listen without knowing us")]
    [SerializeField] private IntEventChannelSO remainingChannel;
    [SerializeField] private VoidEventChannelSO itemCollectedChannel;

    [Header("2 - UnityEvent: what the Inspector wires")]
    [SerializeField] private UnityEvent allCollected;
    [SerializeField] private UnityEvent reset;

    private int remaining;

    void Awake() => remaining = items.Count;

    // 1 - event Action: we subscribe to every item, with a named method.
    void OnEnable()
    {
        foreach (Item item in items) item.Collected += OnItemCollected;
    }

    void OnDisable()
    {
        foreach (Item item in items) item.Collected -= OnItemCollected;
    }

    // The first announcement goes out in Start, not in OnEnable: by then
    // everyone has subscribed, and nobody misses the starting count.
    void Start() => remainingChannel.Raise(remaining);

    private void OnItemCollected(Item item)
    {
        remaining--;

        itemCollectedChannel.Raise();
        remainingChannel.Raise(remaining);

        if (remaining == 0) allCollected.Invoke();
    }

    // Called straight from the UI button: the game driving the game.
    // What it exposes is the action — not the count, not the list.
    public void ResetItems()
    {
        foreach (Item item in items) item.Restore();
        remaining = items.Count;

        reset.Invoke();
        remainingChannel.Raise(remaining);
    }
}
