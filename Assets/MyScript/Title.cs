using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.ispractice=false;
        SceneManager.LoadScene("Main");
    }

    public void Practice()
    {
        GameManager.ispractice=true;
        SceneManager.LoadScene("Practice");
    }
}
