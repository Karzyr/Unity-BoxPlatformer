using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {


	public float rotationSpeed = 100;
	
	// Update is called once per frame
	void Update () {

		float angleRot = rotationSpeed * Time.deltaTime;
		transform.Rotate(Vector3.up * angleRot, Space.World);
		
	}
}
