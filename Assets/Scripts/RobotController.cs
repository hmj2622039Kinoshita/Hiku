using UnityEngine;
using UnityEngine.SceneManagement;

public class RobotController : MonoBehaviour
{
    [SerializeField] Transform magnet; // Magnet
    [SerializeField] GameObject upPosition; // ロボットの四辺のオブジェクト
    [SerializeField] GameObject downPosition;
    [SerializeField] GameObject leftPosition;
    [SerializeField] GameObject rightPosition;
    [SerializeField] GameObject goalEffect; // ゴールエフェクト
    [SerializeField] GameObject clearText; // CLEAR文字
    public bool moveRobot = false; // ロボットが移動しているかどうか（false = 停止）
    private bool goal = false; // ゴールに当たっているかどうか
    private Vector3 moveDirection; // ロボットの移動方向
    private float goalTimer = 0f; // ゴールタイマー
    private float moveTimer = 0f; // ロボットのタイマー

    public void StartRobot(Vector3 direction) // 磁石から呼び出す
    {
        if (moveRobot == true)  return;
        // 進む方向を決める
        moveDirection = direction;
        // 移動開始
        moveRobot = true;
    }

    void Update()
    {
        // ロボットが動いている場合
        if (moveRobot == true)
        {
            moveTimer += Time.deltaTime;

            //Debug.Log("moveTimer = " + moveTimer);

            if (moveTimer >= 0.4f)
            {
                moveTimer = 0f;

                if (moveDirection == Vector3.right)
                {
                    if (CanMove(rightPosition) == true)
                    {
                        transform.position += moveDirection;
                    }
                    else
                    {
                        moveRobot = false;
                    }
                }
                else if (moveDirection == Vector3.left)
                {
                    if (CanMove(leftPosition) == true)
                    {
                        transform.position += moveDirection;
                    }
                    else
                    {
                        moveRobot = false;
                    }
                }
                else if (moveDirection == Vector3.forward)
                {
                    if (CanMove(upPosition) == true)
                    {
                        transform.position += moveDirection;
                    }
                    else
                    {
                        moveRobot = false;
                    }
                }
                else if (moveDirection == Vector3.back)
                {
                    if (CanMove(downPosition) == true)
                    {
                        transform.position += moveDirection;
                    }
                    else
                    {
                        moveRobot = false;
                    }
                }
            }
        }
        Collider[] goalCheck = Physics.OverlapBox(transform.position,Vector3.one * 0.4f);

        foreach (Collider collider in goalCheck)
        {
            if (collider.CompareTag("Goal"))
            {
                goal = true;
                goalEffect.SetActive(true);　//ゴールエフェクトオン
                clearText.SetActive(true); // CLEARテキスト表示
            }
        }

        // ゴールに当たっていて、ロボットが停止している場合
        if (goal == true && moveRobot == false)
        {
            // ゴールに停止している時間を加える
            goalTimer += Time.deltaTime;

            // 1秒経過したらゴールシーンへ
            if (goalTimer >= 2f)
            {
                SceneManager.LoadScene("Title");
            }
        }
        else
        {
            // ゴールにいない、または移動中ならタイマーをリセット
            goalTimer = 0f;
        }
    }

    private bool CanMove(GameObject position)
    {
        Collider[] colliders = Physics.OverlapBox(position.transform.position,Vector3.one * 0.4f);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Object") == true)
            {
                return false;
            }

            if (collider.CompareTag("Wall") == true)
            {
                return false;
            }
        }

        return true;
    }

    // 離れたとき
    private void OnCollisionExit(Collision collision)
    {
        // Goalから離れた場合
        if (collision.gameObject.CompareTag("Goal"))
        {
            // Goalに当たっていない
            goal = false;
            // タイマーをリセット
            goalTimer = 0f;
        }
    }

    public void StartRobotMove()
    {
        if (magnet.position.x < transform.position.x)
        {
            moveDirection = Vector3.right;
        }
        else if (magnet.position.x > transform.position.x)
        {
            moveDirection = Vector3.left;
        }
        else if (magnet.position.z < transform.position.z)
        {
            moveDirection = Vector3.forward;
        }
        else if (magnet.position.z > transform.position.z)
        {
            moveDirection = Vector3.back;
        }
        moveRobot = true;
    }
}
