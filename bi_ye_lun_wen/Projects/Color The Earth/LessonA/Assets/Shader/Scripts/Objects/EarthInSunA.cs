using UnityEngine;
using System.Collections;

public class EarthInSunA : MonoBehaviour {

	public GameObject Earth;
	//保存地球
	public GameObject SolarSystem;
	//保存太阳系

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		Earth.GetComponent<Renderer> ().enabled = true;
		//激活地球渲染组件
		Earth.GetComponent<ScreenShot>().EarthFrame.SetActive(true);
		//通过截图脚本中地球仪配件的变量 来激活地球仪配件的显示
		Earth.GetComponent<EarthTouchA>().SetState=0;
		//将点击交互的状态设置为0
		SolarSystem.SetActive(false);
		//取消太阳系的显示
	}
}
