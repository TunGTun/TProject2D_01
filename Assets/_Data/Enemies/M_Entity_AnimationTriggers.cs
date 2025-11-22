using System.Collections;
using UnityEngine;

public class M_Entity_AnimationTriggers : MonoBehaviour
{
    private M_Entity entity;
    public CircleCollider2D attackCollider;

    private void Awake()
    {
        entity = GetComponentInParent<M_Entity>();
        attackCollider = GameObject.Find("SkeletonAttackCollider").GetComponent<CircleCollider2D>();
        attackCollider.enabled = false;
    }

    private void CurrentStateTrigger()
    {
        entity.CurrentStateAnimationTrigger();
        
    }

    private IEnumerator ColliderCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        attackCollider.enabled = false;
    }

    public void AttackTrigger()
    {
        attackCollider.enabled = true;
        StartCoroutine(ColliderCoroutine());
    }
}
