using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyObject : MonoBehaviour
{
	void FixedUpdate()
	{
		Rigidbody rb = this.GetComponent<Rigidbody>();  // rigidbody���擾
		Vector3 force = new Vector3(0.0f, 0.0f, 1.0f);    // �͂�ݒ�
		rb.AddForce(force);  // �͂�������
	}
}