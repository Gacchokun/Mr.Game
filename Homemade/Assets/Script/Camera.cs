//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Camera : MonoBehaviour
//{
//	// ����ǂ������邩�H
//	[SerializeField]
//	GameObject _target;

//	// ��]���x
//	[SerializeField]
//	float _rotateSpeed = 1.0f;

//	// Pivot�Q�[���I�u�W�F�N�g��Transform
//	[SerializeField]
//	Transform _pivotTransform;

//	// ���̃Q�[���I�u�W�F�N�g�̂��ׂĂ�Update���Ă���
//	// �������郁�\�b�h
//	void LateUpdate() // LateUpdate�F�x��čX�V
//	{
//		// ���݂̃s�{�b�g��X����]���擾
//		float angleX = _pivotTransform.localEulerAngles.x;

//		// Mathf.Clamp�F���鐔�l���ŏ��l�ƍő�l�̊Ԃɂ���
//		if (angleX > 270)
//			angleX = 0;
//		else
//			angleX = Mathf.Clamp(angleX, 0, 70);

//		// ���������p�x��K�p����
//		_pivotTransform.localEulerAngles = new Vector3(angleX, 0, 0);

//		// �����̈ʒu�͒ǂ�������Q�[���I�u�W�F�N�g�̈ʒu�ɂ���        
//		transform.position = _target.transform.position;
//	}
//}

