using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectsController : MyMonoBehaviour
{
	[Header("Dash")]
	[SerializeField] private GameObject dashPrefab;
	[SerializeField] private Transform effectsParent; // nếu null sẽ là object này
	[SerializeField] private int dashPoolSize = 8;
	[SerializeField] private Vector3 dashOffset = Vector3.zero; // position offset (ví dụ 0,0,0)
	[SerializeField] private float continuousSpawnRate = 0.03f; // spawn mỗi 0.03s khi dash liên tục

	private SimplePool dashPool;
	private Coroutine dashRoutine;

	protected override void Awake()
	{
		base.Awake();
		if (effectsParent == null) effectsParent = this.transform;
		dashPool = new SimplePool(dashPrefab, dashPoolSize, effectsParent, this);
	}

	// Spawn 1 lần (use when you want single burst at start)
	public void PlayDashOnce(Vector2 dir)
	{
		if (dashPrefab == null) return;
		Vector3 pos = transform.position + dashOffset;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		dashPool.Spawn(pos, Quaternion.Euler(0f, 0f, angle));
	}

	// Spawn liên tục trong duration (use for streak effect)
	public void PlayDashContinuous(Vector2 dir, float duration)
	{
		if (dashPrefab == null) return;
		StopDashContinuous();
		dashRoutine = StartCoroutine(DashContinuousCoroutine(dir, duration));
	}

	public void StopDashContinuous()
	{
		if (dashRoutine != null)
		{
			StopCoroutine(dashRoutine);
			dashRoutine = null;
		}
	}

	// --- coroutine ---
	private IEnumerator DashContinuousCoroutine(Vector2 dir, float duration)
	{
		float t = 0f;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		while (t < duration)
		{
			Vector3 pos = transform.position + dashOffset;
			dashPool.Spawn(pos, Quaternion.Euler(0f, 0f, angle));
			t += continuousSpawnRate;
			yield return new WaitForSeconds(continuousSpawnRate);
		}
		dashRoutine = null;
	}

	// --- Simple pool inner class ---
	private class SimplePool
	{
		private GameObject prefab;
		private Queue<GameObject> items = new Queue<GameObject>();
		private Transform parent;
		private MonoBehaviour owner;

		public SimplePool(GameObject prefab, int initialCount, Transform parent, MonoBehaviour owner)
		{
			this.prefab = prefab;
			this.parent = parent;
			this.owner = owner;
			if (prefab == null) return;
			for (int i = 0; i < initialCount; i++)
			{
				GameObject go = GameObject.Instantiate(prefab, parent);
				go.SetActive(false);
				items.Enqueue(go);
			}
		}

		public void Spawn(Vector3 pos, Quaternion rot)
		{
			if (prefab == null) return;
			GameObject go;
			if (items.Count > 0) go = items.Dequeue();
			else go = GameObject.Instantiate(prefab, parent);

			go.transform.position = pos;
			go.transform.rotation = rot;
			go.SetActive(true);

			// Play particle if has ParticleSystem on root
			ParticleSystem ps = go.GetComponent<ParticleSystem>();
			float totalTime = 0.5f;
			if (ps != null)
			{
				var main = ps.main;
				float lifeMax = GetLifetimeMax(main.startLifetime);
				totalTime = main.duration + lifeMax + 0.05f;
				ps.Clear();
				ps.Play();
			}

			owner.StartCoroutine(ReturnAfter(go, totalTime));
		}

		private static float GetLifetimeMax(ParticleSystem.MinMaxCurve c)
		{
			switch (c.mode)
			{
				case ParticleSystemCurveMode.Constant: return c.constant;
				case ParticleSystemCurveMode.TwoConstants: return c.constantMax;
				default: return c.constant;
			}
		}

		private IEnumerator ReturnAfter(GameObject go, float time)
		{
			yield return new WaitForSeconds(time);
			go.SetActive(false);
			items.Enqueue(go);
		}
	}
}
