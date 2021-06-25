using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class UnderscoreReplace : EditorWindow
{

    private GameObject[] renameObjects;
    private EditorWindow window;

    [MenuItem("Tools/UnderscoreReplace")]

    static void CreateUnderscoreReplace()
    {
      EditorWindow.GetWindow<UnderscoreReplace>();
    }

    private void OnEnable()
    {
      
       
    }


    private void OnGUI()
    {
        EditorGUILayout.LabelField("This tool will change the first found underscore '_' to a colon ':' ");
        EditorGUILayout.LabelField("for each object selected in the your scene.");
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("This window will close when the operation is complete.");
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        renameObjects = Selection.gameObjects;
        EditorGUILayout.LabelField("Selection count: " + Selection.objects.Length);



            if (GUILayout.Button("Rename"))
        {
            Rename();
        }

       
        
    }

    

    void Rename()
    {
        foreach (GameObject obj in renameObjects)
        {
            var newName = obj.name.ToString();
            var stringarray = newName.ToCharArray();
            for (int i = 0; i <stringarray.Length; i++)
            {
                
                if (stringarray[i].Equals('_'))
                {

                    stringarray[i] = ':' ;
                    newName = new string (stringarray);
                    Debug.Log(obj.name + "'s name changed to " +newName);
                    obj.name = newName.ToString();
                   
                    break;
                }


            }

        }
        this.Close();
    }

}

