using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
	public const int FoodLayer = 8;

	public List<GameObject> prefabs;

	protected uint points;

	protected void Start()
	{
		points = 0;

		Generate();
	}

	protected void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Generate();
		}
	}

	protected void Generate()
	{
		int random_prefab = Random.Range(0, prefabs.Count);
		float x = Random.Range(-0.5f, 0.5f);
		float z = Random.Range(-0.5f, 0.5f);

		GameObject new_prefab = Instantiate(prefabs[random_prefab]) as GameObject;

		new_prefab.transform.parent = this.transform;
		new_prefab.transform.localPosition = new Vector3(x, 0.0f, z);
		new_prefab.transform.localRotation = Quaternion.identity;
		new_prefab.transform.localScale *= 0.5f;
		new_prefab.layer = FoodLayer;

		new_prefab.AddComponent<SphereCollider>();
		new_prefab.AddComponent<Rigidbody>();
	}

	public void AddPoint() {
		points += 1;

		Debug.LogFormat("Points: {0}", points);
	}
}
