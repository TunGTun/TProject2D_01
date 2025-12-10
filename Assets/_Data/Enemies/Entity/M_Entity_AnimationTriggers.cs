using System.Collections;
using UnityEngine;

public class M_Entity_AnimationTriggers : MonoBehaviour
{
    private M_Entity entity;
    

    protected  virtual void Awake()
    {
        entity = GetComponentInParent<M_Entity>();

    }

    private void CurrentStateTrigger()
    {
        entity.CurrentStateAnimationTrigger();
        
    }



    public virtual void AttackTrigger()
    {

    }
}
