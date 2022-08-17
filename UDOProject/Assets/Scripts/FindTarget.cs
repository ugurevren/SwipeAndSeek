using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTarget : MonoBehaviour
{
    [SerializeField] private LayerMask layersToHit;
    private Camera _camera;
    private Vector3 screenPosition;
    private GameObject currentTarget;
    private GameObject currentTargetUI;
    private short currentTargetIndex;
    public GameObject ExplosionPrefab;

    private void Start()
    {
        _camera = Camera.main;
        currentTarget = WinConditionManager.Instance.targets[0];
        currentTargetUI = WinConditionManager.Instance.targetsUI[0];
        foreach (var t in WinConditionManager.Instance.targetsUI)
        {
            t.SetActive(false);
        }
        WinConditionManager.Instance.targetsUI[0].SetActive(true);
    }

    void FixedUpdate()
    {
        screenPosition = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {   
            Ray ray = _camera.ScreenPointToRay(screenPosition);
            if (Physics.Raycast(ray, out RaycastHit hitData, 100, layersToHit))
            {
                if (hitData.transform.gameObject==currentTarget)
                {
                    Vibration.Vibrate(100);
                    Instantiate(ExplosionPrefab, currentTarget.transform.position, currentTarget.transform.rotation);
                    currentTargetIndex++;
                    currentTarget.SetActive(false);
                    currentTargetUI.SetActive(false);
                    try
                    {
                        currentTarget = WinConditionManager.Instance.targets[currentTargetIndex];
                        currentTargetUI = WinConditionManager.Instance.targetsUI[currentTargetIndex];
                        currentTargetUI.SetActive(true);
                    }
                    catch (Exception )
                    {
                        WinConditionManager.Instance.FinishGame();
                    }
                }
            }
        
        
        } 

    }
}