using UnityEngine;

public class BossMinotaurCtrl : BaseBossCtrl
{
    protected override void LoadCollider2D()
    {
        base.LoadCollider2D();
        this.bossCollider2D.size = new Vector2(1.2f, 2.5f);
    }

    //protected override string GetObjectTypeString()
    //{
    //    return this.gameObject.name;
    //}
}
