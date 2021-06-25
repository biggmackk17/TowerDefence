using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    

    int _warFunds;
    int _lives = 20;

    public static Action<int> UpdateUILives;
    public static Action<int> UpdateUIWarfunds;
    public static Action<int,int> OnSave;

    public enum GameSpeed
    {
        stop,
        normal,
        fast
    }
    [SerializeField]
    GameSpeed _gameSpeed = GameSpeed.normal;


    private void ChangeSpeed(GameSpeed speed)
    {
        Debug.Log("SpeedChanged");
       _gameSpeed = speed;

        switch (_gameSpeed)
        {
            case (GameSpeed.stop):
                {
                    Time.timeScale = 0;
                    break;
                }
            case (GameSpeed.normal):
                { Time.timeScale = 1;
                    break;
                }
            case (GameSpeed.fast):
                {
                    Time.timeScale = 2;
                    break;
                }
            default: break;


        }
           
    }


    private void OnEnable()
    {
        EndPoint.RemoveLife += RemoveLife;
        Enemy.AddWarfund += AddWarfunds;
        UIUpdater.OnChangeSpeed += ChangeSpeed;
    }

    private void Start()
    {
        UpdateUIWarfunds?.Invoke(_warFunds);
    }

    private void AddWarfunds(int amount)
    {
        _warFunds += amount;
        UpdateUIWarfunds?.Invoke(_warFunds);
    }

    private void RemoveWarfunds(int amount)
    {
        _warFunds -= amount;
        UpdateUIWarfunds?.Invoke(_warFunds);
    }

    private void RemoveLife()
    {
        _lives--;
        UpdateUILives?.Invoke(_lives);
    }

    private void OnDisable()
    {
        EndPoint.RemoveLife -= RemoveLife;
    }

    private void SaveData() //always pass in warfunds then lives
    {
        OnSave?.Invoke(_warFunds, _lives);
    }
}
