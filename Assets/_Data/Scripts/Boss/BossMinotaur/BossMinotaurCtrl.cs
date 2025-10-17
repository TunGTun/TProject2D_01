using UnityEngine;

public class BossMinotaurCtrl : BaseBossCtrl
{
    protected override void LoadCollider2D()
    {
        base.LoadCollider2D();
        this.bossCollider2D.size = new Vector2(1.8f, 3.5f);
        this.bossCollider2D.offset = new Vector2(0f, 0.1f);
    }

    //protected override string GetObjectTypeString()
    //{
    //    return this.gameObject.name;
    //}
}
