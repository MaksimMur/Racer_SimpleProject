using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerScene : MonoBehaviour
{
    public void Exit() {
        Application.Quit();
    }
    public void LoadScene(string nameOfScene) {
        SceneManager.LoadSceneAsync(nameOfScene);
    }
}
