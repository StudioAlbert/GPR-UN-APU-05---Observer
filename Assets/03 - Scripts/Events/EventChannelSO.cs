using System;
using UnityEngine;

// An event channel: an asset the subject and the observers share, without ever
// knowing each other. One asset per event, one type per shape of announcement.
public abstract class EventChannelSO<T> : ScriptableObject
{
    [Tooltip("Log every announcement and who is listening to it.")]
    [SerializeField] private bool verbose;

    // "event": from the outside, only += and -= compile.
    public event Action<T> Raised;

    public void Raise(T value)
    {
        if (verbose) Debug.Log($"{name} = {value} -> {ChannelLog.Listeners(Raised)}", this);
        Raised?.Invoke(value);
    }
}
