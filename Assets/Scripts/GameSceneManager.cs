using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public void PlayerDied()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);      // Loads the next scene in the queue
    }
}
