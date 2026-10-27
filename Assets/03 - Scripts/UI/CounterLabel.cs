using UnityEngine;
using UnityEngine.UIElements;

// The UI asks nobody for anything: it waits to be told the count.
[RequireComponent(typeof(UIDocument))]
public class CounterLabel : MonoBehaviour
{
    [SerializeField] private IntEventChannelSO remainingChannel;

    private Label label;

    void OnEnable()
    {
        label = GetComponent<UIDocument>().rootVisualElement.Q<Label>("counter");
        remainingChannel.Raised += Show;
    }

    void OnDisable() => remainingChannel.Raised -= Show;

    private void Show(int remaining)
    {
        label.text = remaining == 0 ? "All collected!" : $"Gems left: {remaining}";
    }
}
