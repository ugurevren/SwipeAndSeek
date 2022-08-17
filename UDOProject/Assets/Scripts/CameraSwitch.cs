using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraSwitch : MonoBehaviour

{
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = true;
    public float SWIPE_THRESHOLD = 20f;
    public Transform[] transforms;
    public int TargetIndex;
    public void SwitchTransform(int index)
    {
        transform.DOMove(transforms[index].position,0.7f);
        transform.DORotate(transforms[index].eulerAngles,0.7f);
    }
    void Update()
    {

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            //Detects Swipe while finger is still moving
            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = touch.position;
                    checkSwipe();
                }
            }

            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                checkSwipe();
            }
        }
    }

    void checkSwipe()
    {
        //Check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            //Debug.Log("Vertical");
            if (fingerDown.y - fingerUp.y > 0)//up swipe
            {
                Vibration.Vibrate(50);
                OnSwipeDown();
            }
            else if (fingerDown.y - fingerUp.y < 0)//Down swipe
            {
                Vibration.Vibrate(50);
                OnSwipeUp();
            }
            fingerUp = fingerDown;
        }

        //Check if Horizontal swipe
        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            //Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                Vibration.Vibrate(50);
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                Vibration.Vibrate(50);
                OnSwipeLeft();
            }
            fingerUp = fingerDown;
        }

        //No Movement at-all
        else return;
        SwitchTransform(TargetIndex);
        WinConditionManager.Instance.IncreaseSwipeCounter();
        
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    //////////////////////////////////CALLBACK FUNCTIONS/////////////////////////////
    void OnSwipeUp()
    {
        if (TargetIndex >= 4)
        {
            TargetIndex -= 4;
        }
        Debug.Log("Swipe UP");
    }

    void OnSwipeDown()
    {
        if (TargetIndex <= 3)
        {
            TargetIndex += 4;
        }
        Debug.Log("Swipe Down");
    }

    void OnSwipeLeft()
    {
     if (TargetIndex != 3 && TargetIndex != 7)
            {
                TargetIndex += 1;
            }
            else TargetIndex -= 3;
        
       
        Debug.Log("Swipe Left");
    }
    void OnSwipeRight()
    {
        Debug.Log("Swipe Right");
        if (TargetIndex != 0 && TargetIndex != 4)
                {
                    TargetIndex -= 1;
                }
                else TargetIndex += 3;
    }
}

