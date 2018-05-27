using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Walk : MonoBehaviour {

	public Transform[] PathsTrans;
	private Vector3[] Paths;

	public float speed = 2f;
	public Transform head;

	private string old,cur;

	// Use this for initialization
	void Start () {
//		Paths = new Vector3[PathsTrans.Length];
//		for (int i = 0; i < PathsTrans.Length; i++) {
//			Paths [i] = PathsTrans [i].position;
//		}
//		transform.DOPath (Paths, 6f,PathType.CatmullRom,PathMode.Full3D);
//		Go ();
	}

	void Go()
	{
		transform.DOLocalMoveZ (head.position.z + 1f, 3f).OnComplete (Go);
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = new Ray (head.transform.position, head.transform.forward);
		RaycastHit hit;
		if(Physics.Raycast(ray,out hit,Mathf.Infinity))
	    {
			transform.Translate (head.forward * Time.deltaTime * speed, Space.Self);
			float dis = Vector3.Distance(transform.position,hit.transform.position);
			Debug.Log(hit.transform.name + "_" +dis);
			if (dis < 6f) {
				speed = 0f;
			} else {
				speed = 1f;
			}
//			cur = hit.transform.name;
//			if (old != cur) { // other wall or new wall
//			} else { // same wall
//			}
		}
	}
}


