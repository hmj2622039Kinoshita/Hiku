using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    // マウスクリック
    public void Button1()
    {
        SceneManager.LoadScene("Stage1");
    }
    public void Button2()
    {
        SceneManager.LoadScene("Stage2");
    }
    public void Button3()
    {
        SceneManager.LoadScene("Stage3");
    }
    private void Update()
    {
        // numキー入力
        if(Keyboard.current.numpad1Key.wasPressedThisFrame || Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Stage1");
        }
        if(Keyboard.current.numpad2Key.wasPressedThisFrame || Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Stage2");
        }
        if(Keyboard.current.numpad3Key.wasPressedThisFrame || Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Stage3");
        }
    }
}
