using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject specialText;
    public GameObject mainMenuContent;
    public void PlayGame()
    {
        specialText.SetActive(true);
        mainMenuContent.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (specialText.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);      // Loads the next scene in the queue

            }
        }
    }
}
