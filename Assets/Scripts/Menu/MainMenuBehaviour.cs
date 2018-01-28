using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuBehaviour : MonoBehaviour {

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}
