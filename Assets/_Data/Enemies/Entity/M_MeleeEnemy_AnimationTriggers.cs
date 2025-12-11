using System.Collections;
using UnityEngine;

public class M_MeleeEnemy_AnimationTriggers : M_Entity_AnimationTriggers
{
    public CircleCollider2D attackCollider;
    override protected void Awake()
    {
        base.Awake();
       // attackCollider = GameObject.Find("SkeletonAttackCollider").GetComponent<CircleCollider2D>();
        attackCollider.enabled = false;
    }
    private IEnumerator ColliderCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        attackCollider.enabled = false;
    }
    override public void AttackTrigger()
    {
        attackCollider.enabled = true;
        StartCoroutine(ColliderCoroutine());
    }
}
