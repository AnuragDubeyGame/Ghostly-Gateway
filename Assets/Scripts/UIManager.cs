using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _instuction;
    [SerializeField] private GameObject _deathMenu;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _score;

    private float _timeSurvived;
    void Start()
    {
        GameManager.Instance.OnStateChange += Instance_OnStateChange;
        _restartButton.GetComponent<Button>().onClick.AddListener(RestartGame);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    private void Instance_OnStateChange(GameState obj)
    {
        if(obj == GameState.Idle)
        {
            ChangeGameObjectVisisbility(_instuction, true);
            ChangeGameObjectVisisbility(_deathMenu, false);
            _timeSurvived = 0f;
        }
        else if(obj == GameState.Running)
        {
            ChangeGameObjectVisisbility(_instuction, false);
            ChangeGameObjectVisisbility(_deathMenu, false);

        }
        else if(obj == GameState.Death)
        {
            ChangeGameObjectVisisbility(_instuction, false);
            ChangeGameObjectVisisbility(_deathMenu, true);
            CalculateScore();

        }
    }
    private void Update()
    {
        if(GameManager.Instance.gameState== GameState.Running)
        {
            _timeSurvived += Time.deltaTime;
        }
    }
    private void CalculateScore()
    {
        _timeSurvived = Mathf.Round(_timeSurvived * 100f) / 100f;
        _score.GetComponent<TextMeshProUGUI>().text = $" Survied for {_timeSurvived}s";
    }
    private void ChangeGameObjectVisisbility(GameObject obj, bool val)
    {
        if(val)
        {
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj.gameObject.SetActive(false);

        }
    }
}
