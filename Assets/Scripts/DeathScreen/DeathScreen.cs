using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] PlayerVariables playerVariables;
    [SerializeField] TextMeshProUGUI scoreText;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;                     // Make Cursor visible
        Cursor.visible = true;
        scoreText.text += playerVariables.points;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
