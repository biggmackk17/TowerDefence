using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; //need this namespace

public class EditorWindowDemo : EditorWindow //inherit from editor window
{
    Vector2 _scrollPos; //we will use this later for scroll view
    [SerializeField]
    Texture _image; //image, assign in script inspector
    [SerializeField]
    GameObject _cube; //GO assign in script inspector
    int _index = 0;  //set an int for the dropdown, we will use this later;
    string[] _dropdownOptions; //array of drop down options, we will use this later;
    #region Hidden
    bool _joke1display;
    bool _showImage;
    #endregion




    [MenuItem("Tools/EditorWindow")] //create a dropdown so we can call show window function
    public static void ShowWindow() // this can be called anything. The line above is what runs it
    {
        GetWindow(typeof(EditorWindowDemo), false, "Custom Editor Window");  //This opens the window, will be a popout window, has a custom name overhead. 
    }


    private void OnEnable()
    {
        minSize = new Vector2(200, 400);  //sets min size window can be set in on enable
        maxSize = new Vector2(400, 800);  //set max size window can be set in on enable
        #region Its A Suprise
        _joke1display = false;
            _showImage = false;
        _dropdownOptions = new string[] { "Select Rating", "10", "Ten", "MoreOptions/10/HELLO/HI" }; //create drop down options

        #endregion   

    }


    private void OnGUI() //triggers when window is interacted with
    {
        //GUI LAYOUT AUTOFORMATS FOR US! Notice elements resize with the window. 
        GUILayout.BeginHorizontal("box"); //will start placing element automatically formated horizontally. Will create a "box around them"
        GUILayout.Button("Button 1");
        GUILayout.Button("Button 2");
        GUILayout.EndHorizontal(); //this ends the horizontal formatting
        GUILayout.Space(10);// create a space of x amount of pixels. Note that outside of horizontal formating the space's default is vertical
        GUILayout.BeginHorizontal(); //will start placing element automatically formated horizontally. no box
        GUILayout.Button("Button 3");
        GUILayout.Space(10);// create a space of x amount of pixels
        if (GUILayout.Button("Button 4")) //we can set button functionallity via an if statement
        {
            Debug.Log("Button 4 Was Pressed");
        }
        GUILayout.EndHorizontal(); //leave horizontal formating will default back to vertical
       _scrollPos = GUILayout.BeginScrollView(_scrollPos); //we need a vector 2 to keep track of where we are scrolled to. Will not move without it

        GUILayout.Label("Knock Knock"); //This creates text that is not interactable;
        GUILayout.Space(1000); 
        if (GUILayout.Button("Who's There"))
        {
            _joke1display = true;
        }

        #region DONT EVEN THINK ABOUT IT


        if (_joke1display)
        {
            GUILayout.Space(10);
            GUILayout.Label("GUI JOE");
            if (GUILayout.Button("GUI JOE Who?"))
            {
                _showImage = true;
            }
        }
        #endregion
        if (_showImage)
        {
            GUILayout.Box(_image); //displays a texture; 
            GUILayout.Space(100);
            
            _index = EditorGUILayout.Popup("Rate this joke out of 10", _index, _dropdownOptions);  //create a popup window(dropdown) name the field, pass in the index, pass in the string options
            switch (_index) // switch based on our index
            {
                case 0: break;
                default: GUILayout.Label("Thanks for your input!");
                    break;


            }

            GUILayout.Space(100);

            if (GUILayout.Button("Here is a prize!"))
            {
                Instantiate(_cube, Vector3.zero,Quaternion.identity); //standard instatiation 
            }


           if (GUILayout.Button("Close Window"))
            { 
                Close();  //closes editor window
            }

           var button = GUILayoutUtility.GetLastRect(); // gets the last rect that GUILayout drew so that we can grab positional data, size etc;
           


        }
        GUILayout.EndScrollView(); //ends the scroll view;



    }



}
