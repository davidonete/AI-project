﻿using UnityEngine;
using System.Collections;

public class CarBehaviour : MonoBehaviour {

  private enum CarStates
  {
    kCarState_Searching,
    kCarState_Driving,
    kCarState_Waiting
  }

  public struct CarConditions
  {
    // Searching State
    public bool IsSearching;
    // Driving State
    public bool IsDriving;
    public bool IsGreenLightOn;
    public bool IsCrossWalkDetected;
    public bool IsReachedPoint;
    public bool IsOtherCarNear;
    // Waiting State
    public bool IsWaiting;
  }
 
  private CarStates State;
  private CarConditions Condition;


  void Start()
  {
        //this.CarStateas
    State = CarStates.kCarState_Searching;

    Condition.IsSearching = true;
    Condition.IsDriving = false;
    Condition.IsGreenLightOn = false;
    Condition.IsCrossWalkDetected = false;
    Condition.IsReachedPoint = false;
    Condition.IsWaiting = false;
    Condition.IsOtherCarNear = false;
  }

  void Update()
  {
    StateMachine();
  }

  void StateMachine()
  {
    switch (State)
    {
      case CarStates.kCarState_Searching:
        Searching();
        break;

      case CarStates.kCarState_Driving:
        Driving();
        break;

      case CarStates.kCarState_Waiting:
        Waiting();
        break;

      default:
        break;
    }
  }

  // Searching for a new direction
  void Searching()
  {
    if (Condition.IsSearching)
    {
      /*
      if (SearchedPoint)
      {
        Condition.IsSearching = false;
      }
      */
      Condition.IsSearching = false;
    }
    else
    {
      Condition.IsDriving = true;
      State = CarStates.kCarState_Driving;
    }
  }

  // Driving to the end point
  void Driving()
  {
    if (Condition.IsDriving)
    { 
      if (Condition.IsCrossWalkDetected && Condition.IsGreenLightOn)
      {
        Condition.IsDriving = false;
        Condition.IsWaiting = true;
        State = CarStates.kCarState_Waiting;
      }
      else
      {
        // Test
        if (!Condition.IsOtherCarNear)
            transform.Translate(Vector3.down * 1.0f * Time.deltaTime);
      }

      /*
     if (ReachedPoint)
     {
       Condition.IsDriving = false;
     }
     */
    }
    else
    {
      Condition.IsSearching = true;
      State = CarStates.kCarState_Searching;
    }
  }

  // Waiting traffic light
  void Waiting()
  {
    if (Condition.IsWaiting)
    {
      if (!Condition.IsGreenLightOn)
        Condition.IsWaiting = false;
    }
    else
    {
      Condition.IsDriving = true;
      State = CarStates.kCarState_Driving;
    }
  }

  // Setters & Getters

  public void SetIsGreenLightOn(bool result)
  {
    Condition.IsGreenLightOn = result;
  }

  public void SetIsCrossWalkDetected(bool result)
  {
    Condition.IsCrossWalkDetected = result;
  }

  public void SetIsOtherCarNear(bool result)
  {
    Condition.IsOtherCarNear = result;
  }

  public CarConditions GetCarStates
  {
    get { return Condition; }
  }
}
