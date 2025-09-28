using Unity.VisualScripting;
using UnityEngine;

public class HitBoxDespawn : MyMonoBehaviour
{
    private float lifeTime = 0.3f;
    private float timer;

    protected override void OnEnable()
    {
        base.OnEnable();
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifeTime)
        {
            this.Despawn();
        }
    }

    protected virtual void Despawn()
    {
        BossMinotaurSkillSpawner.Instance.Despawn(this.transform);
    }
}
