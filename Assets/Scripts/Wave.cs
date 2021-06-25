using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "TowerDefense/Wave", fileName = "Wave.Asset")]
public class Wave : ScriptableObject
{
    public int WaveNum;
    public string Name;
    public GameObject[] Enemies;
    public float Delay;

}
