using TMPro;
using UnityEngine;

// L'UI ne demande rien à personne : elle attend qu'on lui annonce le compte.
public class CounterLabel : MonoBehaviour
{
    [SerializeField] private IntEventChannelSO remainingChannel;
    [SerializeField] private TMP_Text label;

    void OnEnable() => remainingChannel.Raised += Show;

    void OnDisable() => remainingChannel.Raised -= Show;

    private void Show(int remaining)
    {
        label.text = remaining == 0 ? "Tout ramassé !" : $"Gemmes restantes : {remaining}";
    }
}
