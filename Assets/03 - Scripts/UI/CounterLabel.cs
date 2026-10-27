using UnityEngine;
using UnityEngine.UIElements;

// L'UI ne demande rien à personne : elle attend qu'on lui annonce le compte.
[RequireComponent(typeof(UIDocument))]
public class CounterLabel : MonoBehaviour
{
    [SerializeField] private IntEventChannelSO remainingChannel;

    private Label label;

    void OnEnable()
    {
        label = GetComponent<UIDocument>().rootVisualElement.Q<Label>("compteur");
        remainingChannel.Raised += Show;
    }

    void OnDisable() => remainingChannel.Raised -= Show;

    private void Show(int remaining)
    {
        label.text = remaining == 0 ? "Tout ramassé !" : $"Gemmes restantes : {remaining}";
    }
}
