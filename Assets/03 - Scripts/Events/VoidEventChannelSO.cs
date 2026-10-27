using System;
using UnityEngine;

// Same thing, for an announcement that carries nothing: EventChannelSO<void>
// does not exist in C#, so that channel has to be written on its own.
[CreateAssetMenu(menuName = "Observer/Void Event Channel", fileName = "NewVoidEventChannel")]
public class VoidEventChannelSO : ScriptableObject
{
    [Tooltip("Log every announcement and who is listening to it.")]
    [SerializeField] private bool verbose;

    public event Action Raised;

    public void Raise()
    {
        if (verbose) Debug.Log($"{name} -> {ChannelLog.Listeners(Raised)}", this);
        Raised?.Invoke();
    }
}
