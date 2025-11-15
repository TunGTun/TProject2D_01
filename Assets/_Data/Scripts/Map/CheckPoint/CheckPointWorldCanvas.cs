using UnityEngine;

public class CheckPointWorldCanvas : MyMonoBehaviour
{
    [SerializeField] protected Transform activeGuide;
    public Transform ActiveGuide => activeGuide;

    [SerializeField] protected Transform soulLinkGuide;
    public Transform SoulLinkGuide => soulLinkGuide;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadActiveGuide();
        this.LoadSoulLinkGuide();
    }

    protected virtual void LoadActiveGuide()
    {
        if (this.activeGuide != null) return;
        this.activeGuide = GameObject.Find("ActiveGuide").transform;
        Debug.Log(transform.name + ": LoadActiveGuide", gameObject);
    }

    protected virtual void LoadSoulLinkGuide()
    {
        if (this.soulLinkGuide != null) return;
        this.soulLinkGuide = GameObject.Find("SoulLinkGuide").transform;
        Debug.Log(transform.name + ": LoadSoulLinkGuide", gameObject);
    }
}
