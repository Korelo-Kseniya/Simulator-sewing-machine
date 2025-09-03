using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public string sceneToLoad = "SampleScene";

    public void StartGames()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(sceneToLoad);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu 1");
    }

    public void ContinueGames()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
