using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	// ゲームの状態を管理する列挙型
	public enum GameState
	{
		Initialize, // ゲームの初期化状態
		Play,       // ゲームプレイ中
		Result,     // 結果表示状態
	}

	// 現在のゲーム状態を保持する静的な変数（初期状態はPlay）
	public static GameState gameState = GameState.Play;

	// ゲーム開始時に呼ばれる
	void Start()
	{
	}

	// 毎フレーム呼ばれる
	void Update()
	{
		// ゲーム状態に応じて処理を分岐
		if (gameState == GameState.Initialize)
		{
			// ゲーム初期化の処理（現在は未実装）
		}
		else if (gameState == GameState.Play)
		{
			// ゲームプレイ中の処理（現在は未実装）
		}
		else if (gameState == GameState.Result)
		{
			// 結果表示の処理（現在は未実装）
		}
	}
}

public class PlayManager : MonoBehaviour
{
	// プレイヤーのターン状態（1回目か2回目か）
	public enum TurnState
	{
		First,  // 1回目のターン
		Second, // 2回目のターン
	}

	// ボールの状態（準備ができているか、準備ができていないか）
	public enum BallState
	{
		Ready,  // ボールが準備完了
		Unready, // ボールが準備されていない
	}

	// ピンの状態（準備ができているか、準備ができていないか）
	public enum PinState
	{
		Ready,  // ピンが準備完了
		Unready, // ピンが準備されていない
	}

	// プレイヤーのターン、ボール、ピンの状態を静的に管理
	public static TurnState turnState = TurnState.First;
	public static BallState ballState = BallState.Unready;
	public static PinState pinState = PinState.Unready;

	void Start()
	{
	}

	// 毎フレーム呼ばれる
	void Update()
	{
		// コメントアウトされた部分は、ターンやボール、ピンの状態に基づいて処理を実行するロジック
	}
}

public class PinManager : MonoBehaviour
{

	public GameObject pinPrefab;
	private GameObject[] pin = new GameObject[10];
	private Vector3[] pinPos = new Vector3[10];
	private static bool[] pinFlag = new bool[10];
	private PIN_STATE[] pinState = new PIN_STATE[10];

	private enum PIN_STATE
	{
		IN,  // ピンが倒れていない
		OUT, // ピンが倒れた
	}

	private void Start()
	{
		InitPin();
	}

	private void Update()
	{
		if (GameManager.gameState == GameManager.GameState.Play)
		{
			if (PlayManager.pinState == PlayManager.PinState.Unready)
			{
				if (PlayManager.turnState == PlayManager.TurnState.First)
				{
					DestroyAllPin();  // すべてのピンを破壊
					Invoke("InitPin", 3.5f);  // 3.5秒後にピンを初期化
					PlayManager.pinState = PlayManager.PinState.Ready;
				}
				else if (PlayManager.turnState == PlayManager.TurnState.Second)
				{
					Invoke("UpdatePin", 3.5f);
					Invoke("ArrangePin", 3.5f);  // ピンの再配置
					PlayManager.pinState = PlayManager.PinState.Ready;
				}
			}

			if (PlayManager.pinState == PlayManager.PinState.Ready)
			{
				Invoke("UpdatePin", 3.5f);
			}

			// 倒れたピンの数が5本以下ならシーン遷移
			if (PinManager.CountPin() <= 5)
			{
				SceneManager.LoadScene("NextSceneName"); // 次のシーン名を指定
			}
		}
	}

	// すべてのピンを破壊するメソッド
	public void DestroyAllPin()
	{
		for (int i = 0; i < 10; i++)
		{
			if (pinFlag[i] == true)
			{
				Destroy(pin[i]);  // ピンを破壊
				pinFlag[i] = false;  // ピンが倒れたフラグを更新
			}
		}
	}

	// ピンの初期化、配置、その他の処理はここに記述...

	// 倒れたピンの数をカウントするメソッド
	public static int CountPin()
	{
		int ret = 0;
		for (int i = 0; i < 10; i++)
		{
			if (pinFlag[i] == false)
			{
				ret++;
			}
		}
		return ret;
	}
}

public class BallManager : MonoBehaviour
{
	// ボールのプレハブ（Prefab）
	public GameObject ballPrefab;
	private GameObject ball;

	// ボールの移動速度
	public float speed = 10;

	// ボールを初期化する
	void InitBall()
	{
		ball = Instantiate(ballPrefab);
		ball.transform.position = new Vector3(1.18f, 3.0f, -26.26f);
	}

	// 毎フレーム呼ばれる
	private void FixedUpdate()
	{
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		// ボールが存在する場合、力を加えて移動させる
		if (ball != null)
		{
			Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
			rigidbody.AddForce(speed * x, 0, speed * z);

			// ボールが画面外に出た場合の処理
			if (ball.transform.position.y < -5.0f)
			{
				Destroy(ball);
				PlayManager.ballState = PlayManager.BallState.Unready;
				PlayManager.pinState = PlayManager.PinState.Unready;

				// 次のターンに切り替え
				if (PinManager.CountPin() != 10)
				{
					PlayManager.turnState = PlayManager.TurnState.Second;
				}
				else
				{
					PlayManager.turnState = PlayManager.TurnState.First;
				}
			}
		}

		// ボールが準備完了でない場合はボールを生成する
		if (PlayManager.ballState == PlayManager.BallState.Unready)
		{
			InitBall();
			PlayManager.ballState = PlayManager.BallState.Ready;
		}

		// スペースキーでボールを再生成する
		if (Input.GetKey(KeyCode.Space))
		{
			Destroy(ball);
			PlayManager.ballState = PlayManager.BallState.Unready;
			InitBall();
		}
	}
}
