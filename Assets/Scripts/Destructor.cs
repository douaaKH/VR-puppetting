using UnityEngine;

public class Destructor : MonoBehaviour {

	protected Generator generator;

	protected void Start () {
		generator = FindObjectOfType<Generator>();

		Collider collider = GetComponent<Collider>();

		if(collider != null) {
			Destroy(collider);
		}
		
		gameObject.AddComponent<CapsuleCollider>().isTrigger = true;
	}

	protected void OnCollisionEnter(Collision collision) {
		OnCollision(collision.gameObject);
	}

	protected void OnTriggerEnter(Collider collider) {
		OnCollision(collider.gameObject);
	}

	protected void OnCollision(GameObject go) {
		if(go.layer == Generator.FoodLayer) {
			Destroy(go);

			if(generator != null) {
				generator.AddPoint();
			}
		}
	}
}
