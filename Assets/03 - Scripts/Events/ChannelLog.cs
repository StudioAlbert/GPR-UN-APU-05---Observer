using System;
using System.Linq;
using UnityEngine;

// What a verbose channel prints. Shared by the two channels because the thing
// worth printing is the same whatever the announcement carries: who is still
// listening. A destroyed subscriber stays in the invocation list, and that is
// the bug you cannot find by reading the code.
public static class ChannelLog
{
    public static string Listeners(Delegate handler)
    {
        Delegate[] subscribers = handler?.GetInvocationList() ?? Array.Empty<Delegate>();
        if (subscribers.Length == 0) return "nobody";

        return subscribers.Length + " : " + string.Join(", ", subscribers.Select(Name));
    }

    private static string Name(Delegate subscriber)
    {
        var owner = subscriber.Target as UnityEngine.Object;
        return (owner == null ? "<destroyed>" : owner.name) + "." + subscriber.Method.Name;
    }
}
