using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionTrigger : MonoBehaviour
{
    public MotionManager motionManager;


    public void Screen_true(int open)
    {
        motionManager.OpeningScreen(Convert.ToBoolean(open));
    }
}
