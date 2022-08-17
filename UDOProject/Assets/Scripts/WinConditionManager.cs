using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinConditionManager : MonoBehaviour
{
   private static WinConditionManager instance;

   public static WinConditionManager Instance
   {
      get { return instance; }
   }

   [SerializeField] public GameObject[] targets;
   [SerializeField] public GameObject[] targetsUI;
   [SerializeField] private int maxSwipeCount;
   public int currentSwipeCount;
   [SerializeField] private GameObject victoryScreen;
   [SerializeField] private GameObject failScreen;

   public void Awake()
   {
      if (!instance)
      {
         instance = this;
      }else Destroy(this);

      Time.timeScale = 1;
   }
   public void IncreaseSwipeCounter()
   {
      currentSwipeCount++;
      if (currentSwipeCount > maxSwipeCount)
      {
         FinishGame();
      }
   }

   public void FinishGame()
   {
      Time.timeScale = 0;
      if (currentSwipeCount > maxSwipeCount)
      {
         Lose();
         return;
      }Win();
   }

   private void Win()
   {
      Vibration.Vibrate(100);
      victoryScreen.SetActive(true);
   }

   private void Lose()
   {
      Vibration.Vibrate(100);
      failScreen.SetActive(true);
   }
}
