using UnityEngine;
using System.Collections;

public class SecUI : MonoBehaviour {

	private float CancelTime=0;
	//申请浮点类型的变量来记录 识别成功提示所存在的时间

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		CancelTime += Time.deltaTime;
		//记录识别成功提示所存在的时间
		//每一帧运行都加经过一帧所使用的键

		if(CancelTime>1.3f){  
        //当识别成功的提示存在时间大于1.3秒时
			CancelTime=0;
			//记录存在时间归零
			gameObject.SetActive(false);
			//取消识别成功提示面板
		}
	}
}
