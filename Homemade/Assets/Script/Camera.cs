//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Camera : MonoBehaviour
//{
//	// 何を追いかけるか？
//	[SerializeField]
//	GameObject _target;

//	// 回転速度
//	[SerializeField]
//	float _rotateSpeed = 1.0f;

//	// PivotゲームオブジェクトのTransform
//	[SerializeField]
//	Transform _pivotTransform;

//	// 他のゲームオブジェクトのすべてがUpdateしてから
//	// 処理するメソッド
//	void LateUpdate() // LateUpdate：遅れて更新
//	{
//		// 現在のピボットのX軸回転を取得
//		float angleX = _pivotTransform.localEulerAngles.x;

//		// Mathf.Clamp：ある数値を最小値と最大値の間にする
//		if (angleX > 270)
//			angleX = 0;
//		else
//			angleX = Mathf.Clamp(angleX, 0, 70);

//		// 制限した角度を適用する
//		_pivotTransform.localEulerAngles = new Vector3(angleX, 0, 0);

//		// 自分の位置は追いかけるゲームオブジェクトの位置にする        
//		transform.position = _target.transform.position;
//	}
//}

