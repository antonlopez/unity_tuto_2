using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class RotateManager : MonoBehaviour
{
    private static RotateManager instance;
    private GameObject _pyramid;

    public static RotateManager GetInstance() {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    public void SetPyramid(GameObject pyramid) {
        _pyramid = pyramid;
    }

    public void SetXaxis() {
        var rotate = _pyramid.GetComponent<LeanTwistRotateAxis>();
        rotate.Axis.x = -1;
        rotate.Axis.y = 0;
        rotate.Axis.z = 0;
    }

    public void SetYaxis()
    {
        var rotate = _pyramid.GetComponent<LeanTwistRotateAxis>();
        rotate.Axis.x = 0;
        rotate.Axis.y = -1;
        rotate.Axis.z = 0;
    }

    public void SetZaxis()
    {
        var rotate = _pyramid.GetComponent<LeanTwistRotateAxis>();
        rotate.Axis.x = 0;
        rotate.Axis.y = 0;
        rotate.Axis.z = -1;
    }


}
