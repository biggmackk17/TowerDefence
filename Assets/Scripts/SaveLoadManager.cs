using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class SaveLoadManager : MonoBehaviour
{
    public static Action OnSave;

    SaveData _data;



    /*  private void Save()
      {
          _gameDataCollected = false;
          _turretDataCollected = false;
          OnSave?.Invoke();

      }

      private void RetrieveGameData(int warfunds,int lives)
      {

      }

      private void RetrieveTurretData()
      {

      }

      private void RetrieveSpawnManagerData()
      {

      }
      */

    public void Save() 
    {
        //creates or opens a file to save to
        FileStream file = new FileStream(Application.persistentDataPath + "/Game.dat", FileMode.OpenOrCreate);
        try
        {
            //binary formmater -- write to a file
            BinaryFormatter formatter = new BinaryFormatter();
            //serialize 
            formatter.Serialize(file, _data);
        }
        catch (SerializationException e) // need to use serialization runtime namespace
        {
            Debug.LogError("Saving Serialization Error: " + e.Message);
        }
        finally
        {
            file.Close();
        }

     
    }

    public void Load() 
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/Game.dat", FileMode.Open);
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            _data = (SaveData)formatter.Deserialize(file);
        }
        catch (SerializationException e)
        {
            Debug.LogError("Loading Serialization Error: " + e.Message);
        }
        finally
        {
            file.Close();
        }
    }
}
