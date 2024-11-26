using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	// �Q�[���̏�Ԃ��Ǘ�����񋓌^
	public enum GameState
	{
		Initialize, // �Q�[���̏��������
		Play,       // �Q�[���v���C��
		Result,     // ���ʕ\�����
	}

	// ���݂̃Q�[����Ԃ�ێ�����ÓI�ȕϐ��i������Ԃ�Play�j
	public static GameState gameState = GameState.Play;

	// �Q�[���J�n���ɌĂ΂��
	void Start()
	{
	}

	// ���t���[���Ă΂��
	void Update()
	{
		// �Q�[����Ԃɉ����ď����𕪊�
		if (gameState == GameState.Initialize)
		{
			// �Q�[���������̏����i���݂͖������j
		}
		else if (gameState == GameState.Play)
		{
			// �Q�[���v���C���̏����i���݂͖������j
		}
		else if (gameState == GameState.Result)
		{
			// ���ʕ\���̏����i���݂͖������j
		}
	}
}

public class PlayManager : MonoBehaviour
{
	// �v���C���[�̃^�[����ԁi1��ڂ�2��ڂ��j
	public enum TurnState
	{
		First,  // 1��ڂ̃^�[��
		Second, // 2��ڂ̃^�[��
	}

	// �{�[���̏�ԁi�������ł��Ă��邩�A�������ł��Ă��Ȃ����j
	public enum BallState
	{
		Ready,  // �{�[������������
		Unready, // �{�[������������Ă��Ȃ�
	}

	// �s���̏�ԁi�������ł��Ă��邩�A�������ł��Ă��Ȃ����j
	public enum PinState
	{
		Ready,  // �s������������
		Unready, // �s������������Ă��Ȃ�
	}

	// �v���C���[�̃^�[���A�{�[���A�s���̏�Ԃ�ÓI�ɊǗ�
	public static TurnState turnState = TurnState.First;
	public static BallState ballState = BallState.Unready;
	public static PinState pinState = PinState.Unready;

	void Start()
	{
	}

	// ���t���[���Ă΂��
	void Update()
	{
		// �R�����g�A�E�g���ꂽ�����́A�^�[����{�[���A�s���̏�ԂɊ�Â��ď��������s���郍�W�b�N
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
		IN,  // �s�����|��Ă��Ȃ�
		OUT, // �s�����|�ꂽ
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
					DestroyAllPin();  // ���ׂẴs����j��
					Invoke("InitPin", 3.5f);  // 3.5�b��Ƀs����������
					PlayManager.pinState = PlayManager.PinState.Ready;
				}
				else if (PlayManager.turnState == PlayManager.TurnState.Second)
				{
					Invoke("UpdatePin", 3.5f);
					Invoke("ArrangePin", 3.5f);  // �s���̍Ĕz�u
					PlayManager.pinState = PlayManager.PinState.Ready;
				}
			}

			if (PlayManager.pinState == PlayManager.PinState.Ready)
			{
				Invoke("UpdatePin", 3.5f);
			}

			// �|�ꂽ�s���̐���5�{�ȉ��Ȃ�V�[���J��
			if (PinManager.CountPin() <= 5)
			{
				SceneManager.LoadScene("NextSceneName"); // ���̃V�[�������w��
			}
		}
	}

	// ���ׂẴs����j�󂷂郁�\�b�h
	public void DestroyAllPin()
	{
		for (int i = 0; i < 10; i++)
		{
			if (pinFlag[i] == true)
			{
				Destroy(pin[i]);  // �s����j��
				pinFlag[i] = false;  // �s�����|�ꂽ�t���O���X�V
			}
		}
	}

	// �s���̏������A�z�u�A���̑��̏����͂����ɋL�q...

	// �|�ꂽ�s���̐����J�E���g���郁�\�b�h
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
	// �{�[���̃v���n�u�iPrefab�j
	public GameObject ballPrefab;
	private GameObject ball;

	// �{�[���̈ړ����x
	public float speed = 10;

	// �{�[��������������
	void InitBall()
	{
		ball = Instantiate(ballPrefab);
		ball.transform.position = new Vector3(1.18f, 3.0f, -26.26f);
	}

	// ���t���[���Ă΂��
	private void FixedUpdate()
	{
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		// �{�[�������݂���ꍇ�A�͂������Ĉړ�������
		if (ball != null)
		{
			Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
			rigidbody.AddForce(speed * x, 0, speed * z);

			// �{�[������ʊO�ɏo���ꍇ�̏���
			if (ball.transform.position.y < -5.0f)
			{
				Destroy(ball);
				PlayManager.ballState = PlayManager.BallState.Unready;
				PlayManager.pinState = PlayManager.PinState.Unready;

				// ���̃^�[���ɐ؂�ւ�
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

		// �{�[�������������łȂ��ꍇ�̓{�[���𐶐�����
		if (PlayManager.ballState == PlayManager.BallState.Unready)
		{
			InitBall();
			PlayManager.ballState = PlayManager.BallState.Ready;
		}

		// �X�y�[�X�L�[�Ń{�[�����Đ�������
		if (Input.GetKey(KeyCode.Space))
		{
			Destroy(ball);
			PlayManager.ballState = PlayManager.BallState.Unready;
			InitBall();
		}
	}
}
