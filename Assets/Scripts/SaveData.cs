using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct SerializableVector3
{
   public float x;
   public float y;
   public float z;

    public Vector3 GetPos()
    {
        return new Vector3(x, y, z);
    }
}

[System.Serializable]
public struct SerializableVector4
{
    float x;
    float y;
    float z;
    float w;

}
public class SaveData 
{
    TurretData _turretData;
    


    public SaveData()
    {
       
    }
}

[System.Serializable]
public class TurretData
{
    SerializableVector4 _rotation;
    SerializableVector3 _position;
    //public enum TurretType { gattling,doubleGattling,rocket, doubleRocket}
   // TurretType _turretType;
    //public TurretData(SerializableVector3 position, SerializableVector4 rotation, TurretType turretType)
   // {
     //   _position = position;
       // _rotation = rotation;
      //  _turretType = turretType;
    }






