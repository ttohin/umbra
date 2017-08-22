using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;
	public float leftLimit;
	public float rightLimit;
	public float topLimit;
	public float bottomLimit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 targetPosition = target.position + offset;
		if (targetPosition.x < leftLimit) {
			targetPosition.x = leftLimit;
		}
		if (targetPosition.x > rightLimit) {
			targetPosition.x = rightLimit;
		}
		if (targetPosition.y < bottomLimit) {
			targetPosition.y = bottomLimit;
		}
		if (targetPosition.y > topLimit) {
			targetPosition.y = topLimit;
		}



		transform.position = Vector3.Lerp (transform.position, targetPosition, smoothSpeed);

	}
}
