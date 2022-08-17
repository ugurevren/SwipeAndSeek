using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeToStart : MonoBehaviour
{
    private Vector3 startTouch, swipeDelta;
    [SerializeField] private GameObject levelsPanel;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTouch = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            swipeDelta = Input.mousePosition-startTouch;
            if (swipeDelta.magnitude>1)
            {
                levelsPanel.SetActive(true);
                Destroy(this);
            }

            startTouch = swipeDelta = Vector3.zero;
        }
    }
}
