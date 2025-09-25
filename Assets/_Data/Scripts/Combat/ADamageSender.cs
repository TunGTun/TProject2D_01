using System.Collections.Generic;
using UnityEngine;

public abstract class ADamageSender : MyMonoBehaviour
{
    private List<ADamageReceiver> observers = new List<ADamageReceiver>();

    public void RegisterObserver(ADamageReceiver receiver)
    {
        if (!observers.Contains(receiver))
            observers.Add(receiver);
    }

    public void UnregisterObserver(ADamageReceiver receiver)
    {
        if (observers.Contains(receiver))
            observers.Remove(receiver);
    }

    public void NotifyObservers()
    {
        if (observers.Count == 0) return;
        foreach (ADamageReceiver receiver in observers)
        {
            receiver.OnDamageReceived();
        }
    }

    public void ClearObservers()
    {
        observers.Clear();
    }
}
