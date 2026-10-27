using UnityEngine;
using UnityEngine.UIElements;

// L'exact inverse du compteur, posé juste à côté de lui : le bouton tient une
// référence et appelle une méthode publique. C'est légitime — l'UI du jeu a le
// droit de piloter le jeu. Le compteur, lui, n'a aucune raison de connaître
// ItemManager : il écoute un canal. Les deux cas, sur le même écran.
[RequireComponent(typeof(UIDocument))]
public class ResetButton : MonoBehaviour
{
    [SerializeField] private ItemManager itemManager;

    private Button button;

    void OnEnable()
    {
        button = GetComponent<UIDocument>().rootVisualElement.Q<Button>("recommencer");
        button.clicked += itemManager.ResetItems;
    }

    void OnDisable() => button.clicked -= itemManager.ResetItems;
}
