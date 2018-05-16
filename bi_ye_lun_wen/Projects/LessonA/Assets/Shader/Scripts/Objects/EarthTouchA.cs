using UnityEngine;
using System.Collections;

public class EarthTouchA : MonoBehaviour {

	public GameObject EarthFrame;
	//储存地球仪配件
	public GameObject SolarSystem;
	//储存太阳系模块
	public int SetState=0;
	//申请Int型变量来储存点击的次数

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(SetState==1||SetState==2){
        //当第一次点击状态或第二次点击状态时
			transform.Rotate (0,25*Time.deltaTime,0,Space.Self);
			//让地球沿着自身Y轴转动
		}
	}

	//点击函数
	void OnMouseDown(){
		if(SetState==0){
			SetState = 1;
			//设置为状态1
		}else if(SetState==1){
			SetState = 2;
			//状态设置为2
			EarthFrame.SetActive(false);
			//取消地球仪配件
			
		}else if(SetState==2){
			SetState = 3;
			//状态设置为3
			SolarSystem.SetActive(true);
			//显示太阳系
			gameObject.GetComponent<Renderer>().enabled=false;
			//取消地球的显示，此处仅仅是渲染方式上不渲染，而不取消模型的激活状态
			
		}else if(SetState==3){
			SetState = 0;
			//状态设置为0
			gameObject.GetComponent<Renderer>().enabled=true;
			//显示地球
			EarthFrame.SetActive(true);
			//显示地球仪配件
			SolarSystem.SetActive(false);
			//取消太阳系的显示
		}
	}
}
