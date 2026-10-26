using System;
using UnityEngine;

// Même chose, pour une annonce qui ne transporte rien : EventChannelSO<void>
// n'existe pas en C#, donc ce canal-là s'écrit à part.
[CreateAssetMenu(menuName = "Observer/Void Event Channel", fileName = "NewVoidEventChannel")]
public class VoidEventChannelSO : ScriptableObject
{
    public event Action Raised;

    public void Raise() => Raised?.Invoke();
}
