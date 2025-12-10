
using System.Collections;
using UnityEngine;

public class M_SoulReward : MyMonoBehaviour
{

    [Header("Soul Reward")]
    public int minSoul = 5;
    public int maxSoul = 15;

    [Header("Lifetime (seconds)")]
    public float lifeTime = 8f;

    private bool isCollected = false;
    protected override void Start()
    {
        base.Start();
        this.StartCoroutine(DespawnRoutine());
    }

    public void SetReward(int min, int max)
    {
        minSoul = min;
        maxSoul = max;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCollected) return; 

        if (!collision.CompareTag("Player")) return;
        isCollected = true;

        int amount = Random.Range(minSoul, maxSoul + 1);


        CharCtrl.Instance.CharData.AddMoney(amount);


        FXSpawner.Instance.Despawn(this.transform);
    }

    private IEnumerator DespawnRoutine()
    {
        yield return new WaitForSeconds(lifeTime);
        FXSpawner.Instance.Despawn(this.transform);
    }
}
