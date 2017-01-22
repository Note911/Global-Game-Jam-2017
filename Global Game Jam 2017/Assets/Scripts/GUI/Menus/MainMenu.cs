using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public void LoadScene() {
        SceneManager.LoadScene("LevelOne");
    }
}
