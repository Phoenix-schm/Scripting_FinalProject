using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);      // Loads the next scene in the queue
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
