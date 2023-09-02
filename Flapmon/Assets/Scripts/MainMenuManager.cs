using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject transition;

    public void StartTheGame()
    {
        StartTransition();
        Invoke("LoadScene",1.5f);
    }

    private void StartTransition() => transition.SetActive(true);
    private void LoadScene() => SceneManager.LoadScene("Level1_Scene");

}
