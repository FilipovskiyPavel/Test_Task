using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity.Test
{
    public class BoyAnimations : MonoBehaviour
    {
        [HideInInspector]
        public SpineBeginnerBodyState state;
        public bool facingLeft;
        public event System.Action StartAimEvent;   // Lets other scripts know when Spineboy is aiming.
        public event System.Action StopAimEvent;   // Lets other scripts know when Spineboy is no longer aiming.
        public MovementStyle style;

        public void StartAim() 
        {
            StartAimEvent?.Invoke();   // Fire the "StartAimEvent" event.   // Fire the "StartAimEvent" event.
        }

        public void StopAim() 
        {
            StopAimEvent?.Invoke();   // Fire the "StopAimEvent" event.   // Fire the "StopAimEvent" event.
        }

        public void TryMove(float speed) 
        {
            if (speed != 0) 
            {
                bool speedIsNegative = speed < 0f;
                facingLeft = speedIsNegative; // Change facing direction whenever speed is not 0.
            }
        }
        public void SpeedTraking(float currentSpeed)
        {
            if(style == MovementStyle.Walk) 
            {
                if(currentSpeed > 2f)
                {
                    state = SpineBeginnerBodyState.Walk;
                }
                else if(currentSpeed <2f && currentSpeed >= 0)
                {
                    state = SpineBeginnerBodyState.Idle;
                }

            }   
            else if(style == MovementStyle.Run)
            {
                if(currentSpeed > 2f)
                {
                    state = SpineBeginnerBodyState.Running;
                }
                else if(currentSpeed <2f && currentSpeed >= 0)
                {
                    state = SpineBeginnerBodyState.Idle;
                }

            }
            else if(style == MovementStyle.Hybrid)
            {
                if(currentSpeed > 2f && currentSpeed < 10f)
                {
                    state = SpineBeginnerBodyState.Walk;
                }
                else if(currentSpeed > 10f)
                {
                    state = SpineBeginnerBodyState.Running;
                }
                else if(currentSpeed <2f && currentSpeed >= 0)
                {
                    state = SpineBeginnerBodyState.Idle;
                }

            }

        }
    }
	public enum SpineBeginnerBodyState 
    {
		Idle,
		Running,
		Jumping,
        Walk,
        Turn
	}
    public enum MovementStyle
    {
        Walk,
        Run,
        Hybrid
    }
}

