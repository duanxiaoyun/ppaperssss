using System;
using EasyAR;
using UnityEngine;

public class BreakImageTarget : MonoBehaviour {

    public Transform target;

    public Action<BreakImageTarget> onTargetFound, onTargetLost;

    public void Init(ImageTargetBehaviour imageTarget)
    {
        imageTarget.TargetFound += OnTargetFound;
        imageTarget.TargetLost += OnTargetLost;
    }

    void OnTargetFound(TargetAbstractBehaviour behaviour)
    {
        if (onTargetFound != null)
            onTargetFound(this);
    }

    void OnTargetLost(TargetAbstractBehaviour behaviour)
    {
        if (onTargetLost != null)
            onTargetLost(this);
    }
}
