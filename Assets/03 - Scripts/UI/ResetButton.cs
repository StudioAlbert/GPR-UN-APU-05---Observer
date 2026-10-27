using UnityEngine;
using UnityEngine.UIElements;

// The exact opposite of the counter, sitting right next to it: the button holds
// a reference and calls a public method. That is legitimate — the game's UI is
// allowed to drive the game. The counter has no reason at all to know
// ItemManager: it listens to a channel. Both cases, on the same screen.
[RequireComponent(typeof(UIDocument))]
public class ResetButton : MonoBehaviour
{
    [SerializeField] private ItemManager itemManager;

    private Button button;

    void OnEnable()
    {
        button = GetComponent<UIDocument>().rootVisualElement.Q<Button>("restart");
        button.clicked += itemManager.ResetItems;
    }

    void OnDisable() => button.clicked -= itemManager.ResetItems;
}
