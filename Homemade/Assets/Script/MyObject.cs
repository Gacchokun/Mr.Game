using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyObject : MonoBehaviour
{
	void FixedUpdate()
	{
		Rigidbody rb = this.GetComponent<Rigidbody>();  // rigidbody‚ğæ“¾
		Vector3 force = new Vector3(0.0f, 0.0f, 1.0f);    // —Í‚ğİ’è
		rb.AddForce(force);  // —Í‚ğ‰Á‚¦‚é
	}
}