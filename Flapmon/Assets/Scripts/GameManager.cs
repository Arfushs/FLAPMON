using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _startingSceneTransition;
    [SerializeField] private GameObject _endingSceneTransition;
    [SerializeField] private TextMeshProUGUI _gemText;
    [SerializeField] private String nextLevelName;
    private int totalGemCount;
    private int currentGemCount;

    public static GameManager Instance;

    private bool flag = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        
        totalGemCount = FindObjectsOfType<Gem>().Length;
        currentGemCount = totalGemCount;
        
    }

    private void Start()
    {
        _endingSceneTransition.SetActive(true);
        Invoke("DisableEndingSceneTransition",1.25f);
    }

    private void Update()
    {
        _gemText.text = currentGemCount.ToString();

        if (currentGemCount ==0&&!flag)
        {
            flag = true;
            Invoke("ActivateStartingSceneTransition",1f);
            Invoke("LoadNextLevel",2.5f);
        }
    }

    private void DisableEndingSceneTransition()
    {
        _endingSceneTransition.SetActive(false);
    }
    private void DisableStartingSceneTransition()
    {
        _startingSceneTransition.SetActive(false);
    }
    private void ActivateEndingSceneTransition()
    {
        _endingSceneTransition.SetActive(true);
    }
    private void ActivateStartingSceneTransition()
    {
        _startingSceneTransition.SetActive(true);
    }

    public void DecreaseGemCount()
    {
        currentGemCount -= 1;
    }

    public static void RestartGame()
    {
        Instance.Invoke("ActivateStartingSceneTransition",1f);
        Instance.Invoke("ReloadSameLevel",2.5f);
        
    }

    private void ReloadSameLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void LoadNextLevel() => SceneManager.LoadScene(nextLevelName);
}
