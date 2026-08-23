using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
}
