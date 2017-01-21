using UnityEngine;
using UnityEditor.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

    void OnGUI() {
        if (GUI.Button(new Rect(10, 100, 100, 30), "Play")) {
            EditorSceneManager.LoadScene("LevelOne");
        }
        if (GUI.Button(new Rect(10, 140, 100, 30), "Options")) {
             EditorSceneManager.LoadScene("OptionsMenu");
        }
        if (GUI.Button(new Rect(10, 180, 100, 30), "Exit Game")) {
            Application.Quit();
        }
    }
}
