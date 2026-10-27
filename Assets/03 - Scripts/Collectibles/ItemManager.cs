using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Everything goes through here, and everything in it is hard-wired: the counter
// label and the end panel are fields. Adding a system means reopening this file.
// That is exactly the problem the session takes apart.
public class ItemManager : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();

    [SerializeField] private UIDocument hud;
    [SerializeField] private GameObject endPanel;

    private Label label;
    private int remaining;

    void Awake() => remaining = items.Count;

    void Start()
    {
        label = hud.rootVisualElement.Q<Label>("counter");
        Show();
    }

    public void ItemCollected(Item item)
    {
        remaining--;
        Show();

        if (remaining == 0) endPanel.SetActive(true);
    }

    public void ResetItems()
    {
        foreach (Item item in items) item.Restore();
        remaining = items.Count;

        endPanel.SetActive(false);
        Show();
    }

    private void Show()
    {
        label.text = remaining == 0 ? "All collected!" : $"Gems left: {remaining}";
    }
}
