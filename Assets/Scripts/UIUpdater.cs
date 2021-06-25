using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIUpdater : MonoBehaviour
{[Header("Text")]

    [SerializeField] Text _waveText;
    [SerializeField] Text _livesText;
    [SerializeField] Text _statusText;
    [SerializeField] Text _warfundText;
    [Space]
    [Space]
    [Header("Button Highlights")]
    [SerializeField] GameObject _pauseHighlight;
    [SerializeField] GameObject _ffHeightlight;
    [SerializeField] GameObject _normalHighlight;

    public static Action <GameManager.GameSpeed> OnChangeSpeed;
    public static Action OnRestart;



    private void OnEnable()
    {
        GameManager.UpdateUILives += UpdateLives;
        GameManager.UpdateUIWarfunds += UpdateWarfunds;
        SpawnManager.UIUpdateWave += UpdateWave;
    }



    private void UpdateWarfunds(int amount)
    {
        _warfundText.text = amount.ToString();
    }

    private void UpdateWave(int wave)
    {
        _waveText.text = wave.ToString() + "/10";
    }


    private void UpdateLives(int lives)
    {
        _livesText.text = lives.ToString();
    }


    private void ChangeStatus()
    {


    }


    public void NormalSpeed()
    {
      
        Debug.Log("normal speed");
        OnChangeSpeed?.Invoke(GameManager.GameSpeed.normal);
        _normalHighlight.SetActive(true);
        _ffHeightlight.SetActive(false);
        _pauseHighlight.SetActive(false);
    }

    public void FastForward()
    {
        Debug.Log("fast forward speed"); 
        OnChangeSpeed?.Invoke(GameManager.GameSpeed.fast);
        _normalHighlight.SetActive(false);
        _ffHeightlight.SetActive(true);
        _pauseHighlight.SetActive(false);

    }
    public void Pause()
    {
        Debug.Log("pause speed");
        OnChangeSpeed?.Invoke(GameManager.GameSpeed.stop);
        _normalHighlight.SetActive(false);
        _ffHeightlight.SetActive(false);
        _pauseHighlight.SetActive(true);
    }



    private void OnDisable()
    {
        GameManager.UpdateUILives -= UpdateLives;
        GameManager.UpdateUIWarfunds -= UpdateWarfunds;
        SpawnManager.UIUpdateWave -= UpdateWave;
    }





}
