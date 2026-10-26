using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Version couplée : le gestionnaire connaît l'étiquette et le panneau de fin.
// Chaque réaction qu'on voudra ajouter sera un champ de plus dans ce fichier.
public class ItemManager : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private TMP_Text label;
    [SerializeField] private GameObject panneauFin;

    private int remaining;

    void Awake() => remaining = items.Count;

    void Start() => Afficher();

    public void ItemRamasse(Item item)
    {
        remaining--;
        Afficher();

        if (remaining == 0) panneauFin.SetActive(true);
    }

    private void Afficher()
    {
        label.text = remaining == 0 ? "Tout ramassé !" : $"Gemmes restantes : {remaining}";
    }
}
