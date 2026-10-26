using System;
using UnityEngine;

// Un canal d'événement : un asset que le sujet et les observateurs partagent,
// sans jamais se connaître. Un asset par événement, un type par forme d'annonce.
public abstract class EventChannelSO<T> : ScriptableObject
{
    // « event » : dehors, seuls += et -= compilent.
    public event Action<T> Raised;

    public void Raise(T value) => Raised?.Invoke(value);
}
