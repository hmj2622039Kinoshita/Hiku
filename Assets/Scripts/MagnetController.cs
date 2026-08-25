using System;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MagnetController : MonoBehaviour
{
    [SerializeField] Transform robot; // ロボット
    [SerializeField] GameObject upPosition; // ロボットの四辺のオブジェクト
    [SerializeField] GameObject downPosition;
    [SerializeField] GameObject leftPosition;
    [SerializeField] GameObject rightPosition;
    [SerializeField] RobotController robotController;
    private Vector3 selectDirection = Vector3.forward; // 現在選択している方向

    private bool SetMagnet(GameObject position) // 磁石が置けるかどうか
    {
        Collider[] colliders = Physics.OverlapBox(position.transform.position,Vector3.one * 0.4f); // positionの場所の中心から0.4倍の範囲にcolliderがあるか調べる

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Object") == true)
            {
                return false;
            }

            if (collider.CompareTag("Wall") == true)
            {
                return false; // 磁石を置けない
            }
        }
        return true; // 磁石を置ける
    }
    void Update()
    {
        // 上キー
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            if (SetMagnet(upPosition) == true)
            {
                selectDirection = Vector3.forward;
                transform.position = new Vector3(upPosition.transform.position.x, transform.position.y, upPosition.transform.position.z);
            }
        }

        // 下キー
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
                if (SetMagnet(downPosition) == true)
                {
                    selectDirection = Vector3.back;
                transform.position = new Vector3(downPosition.transform.position.x, transform.position.y, downPosition.transform.position.z);
            }
        }

        // 左キー
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            if (SetMagnet(leftPosition) == true)
            {
                selectDirection = Vector3.left;
                transform.position = new Vector3(leftPosition.transform.position.x, transform.position.y, leftPosition.transform.position.z);
            }
        }

        // 右キー
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            if (SetMagnet(rightPosition) == true)
            {
                selectDirection = Vector3.right;
                transform.position = new Vector3(rightPosition.transform.position.x, transform.position.y, rightPosition.transform.position.z);
            }
        }

        // エンターキー
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            robotController.StartRobotMove();
        }

        // Escキー
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Title");
        }
    }
}
