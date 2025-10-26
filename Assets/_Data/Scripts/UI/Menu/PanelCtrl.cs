using UnityEngine;

public class PanelCtrl : MyMonoBehaviour
{
    [SerializeField] protected GameObject menuGroup;
    public GameObject MenuGroup => menuGroup;

    [SerializeField] protected GameObject selectGame;
    public GameObject SelectGame => selectGame;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMenuGroup();
        this.LoadSelectGame();
    }

    protected virtual void LoadMenuGroup()
    {
        if (this.menuGroup != null) return;
        this.menuGroup = GameObject.Find("MenuGroup");
        Debug.Log(transform.name + ": LoadMenuGroup", gameObject);
    }

    protected virtual void LoadSelectGame()
    {
        if (this.selectGame != null) return;
        this.selectGame = GameObject.Find("SelectGame");
        Debug.Log(transform.name + ": LoadSelectGame", gameObject);
    }
}
