using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyAR;
using UnityEngine.UI;

public class ARManager : MonoBehaviour {
    public List<BreakImageTarget> imageTargetList;
    public Transform breakParent;
    public Button btn_reset;
    public List<BreakImageTarget> breakTargetList;

    void Awake(){
        for (int i = 0; i < imageTargetList.Count;i++){
            imageTargetList[i].onTargetFound += OnTargetFound;
            imageTargetList[i].onTargetLost += OnTargetLost;
        }
        btn_reset.onClick.AddListener(ResetBreakTargetList);
    }

	// Use this for initialization
	void Start () {
		
	}
	
    void ResetBreakTargetList () {
        for (int i = 0; i < breakTargetList.Count;i++)
        {
            SetFoundTarget(breakTargetList[i]);
        }
        breakTargetList.Clear();
	}

    void OnTargetFound(BreakImageTarget obj)
    {
        ResetBreakTargetList();
        //Debug.Log("OnTargetFound:" + obj.name);
        SetFoundTarget(obj);
    }

    void OnTargetLost(BreakImageTarget obj)
    {
        //Debug.Log("OnTargetLost:" + obj.name);
        breakTargetList.Add(obj);
        obj.target.SetParent(breakParent);
        obj.target.localPosition = new Vector3(0, -0.35f, 1.5f);
        obj.target.localEulerAngles = new Vector3(0, 180, 0);
        obj.target.localScale = new Vector3(0.7f, 0.7f, 0.7f);
    }

    void SetFoundTarget(BreakImageTarget obj)
    {
        obj.target.SetParent(obj.transform);
        //Debug.Log("Parent:"+obj.target.parent.name);
        obj.target.localPosition = new Vector3(0, 0.287f, -0.35f);
        obj.target.localEulerAngles = new Vector3(-90, 180, 0);
        obj.target.localScale = new Vector3(0.7f, 0.7f, 0.7f);
    }
}
