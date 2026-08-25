using UnityEngine;
using UnityEngine.Audio;
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
    [SerializeField] GameObject overText; // CLEAR文字
    [SerializeField] GameObject overbackText; // CLEAR文字
    [SerializeField] AudioSource audioSource; // AudioSource
    [SerializeField] AudioClip clear; // ゲームクリア時の効果音
    [SerializeField] AudioClip gameover; // ゲームオーバー時の効果音
    public bool moveRobot = false; // ロボットが移動しているかどうか（false = 停止）
    private bool goal = false; // ゴールに当たっているかどうか
    private bool over = false; // 詰みマスに当たっているかどうか
    private Vector3 moveDirection; // ロボットの移動方向
    private float goalTimer = 0f; // ゴールタイマー
    private float overTimer = 0f; // 詰みマスタイマー
    private float moveTimer = 0f; // ロボットのタイマー

    public void StartRobot(Vector3 direction) // 磁石から呼び出す
    {
        if (moveRobot == true) return; // ロボットが動いていたら
        moveDirection = direction; // 進む方向を決める
        moveRobot = true; // ロボットが移動している状態
    }

    void Update()
    {
        if (moveRobot == true)　// ロボットが動いている場合
        {
            moveTimer += Time.deltaTime;
            if (moveTimer >= 0.4f)
            {
                moveTimer = 0f;
                if (moveDirection == Vector3.right) // 右方向に移動
                {
                    if (CanMove(rightPosition) == true)
                    {
                        transform.position += moveDirection;
                    }
                    else { moveRobot = false; }
                }
                else if (moveDirection == Vector3.left) // 左方向に移動
                {
                    if (CanMove(leftPosition) == true)
                    {
                        transform.position += moveDirection;
                    }
                    else { moveRobot = false; }
                }
                else if (moveDirection == Vector3.forward) // 上方向に移動
                {
                    if (CanMove(upPosition) == true)
                    {
                        transform.position += moveDirection;
                    }
                    else { moveRobot = false; }
                }
                else if (moveDirection == Vector3.back) // 下方向に移動
                {
                    if (CanMove(downPosition) == true)
                    {
                        transform.position += moveDirection;
                    }
                    else { moveRobot = false; }
                }
            }
        }

        Collider[] goalCheck = Physics.OverlapBox(transform.position,Vector3.one * 0.4f);　// ゴールがあるか確認

        foreach (Collider collider in goalCheck)
        {
            if (collider.CompareTag("Goal") && moveRobot == false) // ゴールマス
            {
                goal = true;
                goalEffect.SetActive(true);　//ゴールエフェクトオン
                clearText.SetActive(true); // CLEARテキスト表示
                audioSource.PlayOneShot(clear); // クリア時の効果音
            }

            if (collider.CompareTag("Over") && moveRobot == false) // 詰みマス
            {
                over = true;
                overText.SetActive(true); // GAMEOVERテキスト表示
                overbackText.SetActive(true); // GAMEOVER背景表示
                audioSource.PlayOneShot(gameover); // ゲームオーバー時の効果音
            }
        }

        // ゴールに当たっていて、ロボットが停止している場合
        if (goal == true && moveRobot == false)
        {
            goalTimer += Time.deltaTime; // ゴールに停止している時間
            if (goalTimer >= 2.4f) // 2.4秒経過したらタイトルシーンへ戻る
            {
                SceneManager.LoadScene("Title");
            }
        }
        else
        {
            goalTimer = 0f; // ゴールにいない、または移動中ならタイマーをリセット
        }

        // 詰みマスにいて、ロボットが停止している場合
        if (over == true && moveRobot == false)
        {
            overTimer += Time.deltaTime; // 詰みマスに停止している時間
            if (overTimer >= 1.5f) // 1.5秒経過したらタイトルシーンへ戻る
            {
                SceneManager.LoadScene("Title");
            }
        }
        else
        {
            overTimer = 0f; // 詰みマスにいない、または移動中ならタイマーをリセット
        }
    }

    private bool CanMove(GameObject position) // 障害物や壁があるかどうか確認
    {
        Collider[] colliders = Physics.OverlapBox(position.transform.position, Vector3.one * 0.4f);

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
        return true; // trueならロボットが移動する
    }

    // ゴールから離れたとき（ゴールを通過）
    private void OnCollisionExit(Collision collision)
    {
        // Goalから離れた場合
        if (collision.gameObject.CompareTag("Goal"))
        {
            goal = false; // Goalに当たっていない
            goalTimer = 0f; // タイマーをリセット
        }
    }

    public void StartRobotMove() // ロボットの移動する方向
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
