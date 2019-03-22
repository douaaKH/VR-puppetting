using UnityEngine;

public class StickyFinger : MonoBehaviour {

	protected void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.layer == Generator.FoodLayer) {
			collision.gameObject.transform.parent = transform;

			Rigidbody rigid_body = collision.gameObject.GetComponent<Rigidbody>();

			if(rigid_body != null) {
				rigid_body.isKinematic = true;
			}
		}
	}
}
