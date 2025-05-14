using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] PlayerVariables playerVariables;
    [SerializeField] TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText.text += playerVariables.points;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);      // Loads the next scene in the queue

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
