using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void GoToMainMenu()
    {
        Vibration.Vibrate(50);
        SceneManager.LoadScene(0);
    }
    public void Retry()
    {
        Vibration.Vibrate(50);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        Vibration.Vibrate(50);
        PlayerPrefs.SetInt("Level"+(SceneManager.GetActiveScene().buildIndex+1),1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        
    }
}
