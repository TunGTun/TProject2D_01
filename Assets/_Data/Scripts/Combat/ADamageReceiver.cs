using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public abstract class ADamageReceiver : MyMonoBehaviour
{
    public abstract void OnDamageReceived(int damage);
}
