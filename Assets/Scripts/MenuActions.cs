using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    // Call this from the Start Game button
    public void StartGame()
    {
        SceneManager.LoadScene("MicroscapeScene", LoadSceneMode.Single);
    }

    // Call this from the Exit button
    public void ExitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // For testing in editor
        #else
                    Application.Quit(); // Quits the built application
        #endif
    }
}