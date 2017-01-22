using UnityEngine;
using UnityEditor.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public void LoadScene() {
        EditorSceneManager.LoadScene("LevelOne");
    }
}
