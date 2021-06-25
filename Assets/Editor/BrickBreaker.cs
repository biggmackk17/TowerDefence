
using UnityEngine;
using System.Collections.Generic;
    
using UnityEditor;

public class BrickBreaker : EditorWindow
{
    float _paddlePos;
    Rect _paddleRect;
    Rect _ball;
    Vector2 _ballDir;
    Vector2 _rectPos = new Vector2(0, 0);
    Vector2 _rectSize = new Vector3(2, .5f);
    Vector2 _ballPos = new Vector3(150, 300);
    List<Rect> _brickRectList = new List<Rect>();
    List<Brick> _bricks = new List<Brick>();
    int _brickAmount = 20;
    bool win = false;
    bool lose = false;
    int _bricksLeft;
    bool _bossIsComing;


    bool play;
    [MenuItem("ImportantTools/BrickBreaker")]
    public static void ShowWindow()
    {
        GetWindow(typeof(BrickBreaker),true,"SystemStats");


    }

    Vector2 _brickPos;
    Vector2 _brickSize = new Vector2 (30,10);
    private void OnEnable()
    {
        _bricksLeft = _brickAmount;
        _bossIsComing = false;
        win = false;
        lose = false;
        _ballDir = new Vector2(0, 1);
        _ball = new Rect(_ballPos.x, _ballPos.y, 10, 10);
        EditorApplication.update += Update;
        this.maxSize = (new Vector2(332, 500));
        this.minSize = (new Vector2(332, 500));
        // EditorGUI.DrawRect(new Rect(_ballPos.x, _ballPos.y, 10, 10), Color.red);
        _brickPos.x = 0;
        _brickPos.y = 80;
        for (var i = 0; i < _brickAmount; i++)
        {
            if (i % 10 == 0)
            {
                _brickPos.y += _brickSize.y +10;
                _brickPos.x = 0;
            }
            else
            {
                _brickPos.x += _brickSize.x +2;

            }

            var brickRect = new Rect(_brickPos, _brickSize);
            _brickRectList.Add(brickRect);

            var newBrick = new Brick(true, _brickSize, _brickPos);
            _bricks.Add(newBrick);



        }

       // _bricks[4].enabled = false;

    }


    private void OnGUI()
    {
        if (!_bossIsComing)
        {
            _paddleRect = new Rect(_rectPos, _rectSize);
            _paddlePos = EditorGUILayout.Slider("Controller", _paddlePos, 0, 280);
            EditorGUI.DrawRect(_paddleRect, Color.black);

            EditorGUI.DrawRect(new Rect(_paddlePos, 350, 50, 10), Color.black);

            EditorGUI.DrawRect(_ball, Color.red);
            if (lose)
            {
                EditorGUI.LabelField(new Rect(200, 200, 100, 50), "You Lose");
            }
            else if (win)
            {
                EditorGUI.LabelField(new Rect(200, 200, 100, 50), "You Win");
            }


            for (var i = 0; i < _brickRectList.Count; i++)
            {
                if (_bricks[i].enabled)
                {
                    EditorGUI.DrawRect(_brickRectList[i], Color.blue);

                }

            }


            if (GUILayout.Button("StartGame"))
            { play = true; }

            if (GUILayout.Button("Restart"))
            { Restart(); }

            if (GUILayout.Button("Boss Is Coming!"))
            {
                _bossIsComing = true;
            }

        }
        else
        {
            EditorGUILayout.LabelField("Performance: " + _fakePerformance);
            EditorGUILayout.LabelField("FPS: " + _fakeFPS);
            EditorGUILayout.LabelField("System Temp: " + _fakeTemp+"f");
            EditorGUILayout.LabelField("Optimization Percent: " + _fakePercent);




        }




    }

        float _fakePerformance;
        float _fakeFPS;
        float _fakeTemp;
        float _fakePercent;

    void Update()
    {
        _fakePerformance = Random.Range(10,90);
        _fakeFPS = Random.Range(1000, 2000);
        _fakeTemp = Random.Range(80, 90);
        _fakePercent = Random.Range(90,100);



        if (play)
        {

            _ball.position += _ballDir/5;

            if (RangeCheck(_ball.position.x, _paddlePos - 25, _paddlePos + 50) && RangeCheck(_ball.position.y, 345, 360))
            {
                _ballDir.x = (Random.Range(-2f, 2f));
                _ballDir.y = -1;

            }

            if (_ball.position.x > 332f || _ball.position.x <= 0f)
            {
                _ballDir.x = -_ballDir.x;
            }

            if (_ball.position.y < 0)
            {
                _ballDir.y = 1;
            }

      
            foreach (Brick brick in _bricks)
            {
               
                if (brick.enabled == true)
                {
                  
                    if (RangeCheck(_ball.position.x, brick.xRange.x, brick.xRange.y) && RangeCheck(_ball.position.y, brick.yRange.x, brick.yRange.y))
                    {
                        _ballDir = -_ballDir;
                        brick.enabled = false;
                        _bricksLeft--;
                        if (_bricksLeft <= 0)
                        {
                            play = false;
                            win = true;
                        }
                    }
                }
               

            }

            if (_ball.position.y > 500)
            {
                play = false;
                lose = true;
                
                
            }



        }
            //Repaint();
    }

    void Restart()
    {
        play = false;
        win = false;
        lose = false;
       _bricksLeft = _brickAmount;
        foreach (Brick brick in _bricks)
        {
            brick.enabled = true;
        }
        _ball.position = _ballPos;
    }


    bool RangeCheck(float value, float min, float max)
    {
        if (value >= min && value <= max)
        {
            return true;
        }
        else return false;
    }

    private void OnDisable()
    {
        play = false;
    }


}
    public class Brick
    {
   public bool enabled;
   public Vector2 size;
    public Vector2 position;
    public Vector2 xRange;
    public Vector2 yRange;

    public Brick(bool _enabled, Vector2 _size, Vector2 _position)
    {
        enabled = _enabled;
        size = _size;
        position = _position;
        xRange = new Vector2(_position.x - size.x / 2, _position.x + size.x / 2);
        yRange = new Vector2(_position.y - size.y / 2, _position.y + size.y / 2);
    }

    }
