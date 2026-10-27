using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Tout passe par ici, et tout y est en dur : l'étiquette du compteur et le
// panneau de fin sont des champs. Ajouter un système, c'est rouvrir ce fichier.
// C'est exactement le problème que la séance démonte.
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
        label = hud.rootVisualElement.Q<Label>("compteur");
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
        label.text = remaining == 0 ? "Tout ramassé !" : $"Gemmes restantes : {remaining}";
    }
}
