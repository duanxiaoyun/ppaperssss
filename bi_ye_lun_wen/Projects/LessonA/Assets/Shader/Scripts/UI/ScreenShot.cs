using UnityEngine;
using System.Collections;

public class ScreenShot : MonoBehaviour {
	public GameObject Earth;
	//申请公有变量储存要赋予贴图的模型
	public GameObject EarthFrame;
	//储存地球仪配件模型
	public GameObject EarthA;
	//储存太阳系中的地球

	public GameObject Plane;
	//储存面片

	private int ScreenWidth;
	//申请私有int型变量 记录屏幕的宽
	private int ScreenHeight;
	//申请私有int型变量 记录屏幕的高
	private Texture2D TextureShot;
	//申请Texture2D型变量 用来储存屏幕截图

	private Vector2 PlaneWH;
	//记录面片的宽高

	//记录面片的世界坐标
	private Vector3 TopLeft_Pl_W;
	//记录面片左上角的世界坐标
	private Vector3 BottomLeft_Pl_W;
	//记录面片左下角的世界坐标
	private Vector3 TopRight_Pl_W;
	//记录面片右上角的世界坐标
	private Vector3 BottomRight_Pl_W;
	//记录面片右下角的世界坐标

	// Use this for initialization
	void Start () {
		ScreenWidth = Screen.width;
		//获取屏幕的宽
		ScreenHeight=Screen.height;
		//获取屏幕的高

		TextureShot = new Texture2D (ScreenWidth,ScreenHeight,TextureFormat.RGB24,false);
		// 标准格式 ： Texture2D(int width,int height,TextureFormat format,bool mipmap);
		// “int width,int height,” 纹理的宽高
		//"TextureFormat format" 纹理的模式 RGB24 RGBA32等模式 
		//"bool mipmap"mipmap是一种分级纹理  在屏幕中显示大小不同时候给予不同级别的纹理 这里不使用

	}

	// Update is called once per frame
	void Update () {

	}

	public void ScreenShot_Button(){

		PlaneWH = new Vector2 (Plane.GetComponent<MeshFilter>().mesh.bounds.size.x,Plane.GetComponent<MeshFilter>().mesh.bounds.size.z)*5*0.5f;
		//获取面片的宽高的一半
		//"gameObject.GetComponent<MeshFilter>().mesh.bounds.size.x"获取面片X方向的宽度
		//"*5"是因为开始获取到的长宽是模型本身的长宽，而场景中我们有缩放因素，父级物体放大了50倍，自身缩小到了0.1，因此获取实际宽高需要再乘以5

		//获取面片四个点的世界坐标
		TopLeft_Pl_W = Plane.transform.parent.position + new Vector3 (-PlaneWH.x,0,PlaneWH.y);
		//获取面片左上角的世界坐标
		//"gameObject.transform.parent.position"物体的父级物体的世界坐标
		//"new Vector2 (-PlaneWH.x,PlaneWH.y)"向左上方偏移的量
		BottomLeft_Pl_W = Plane.transform.parent.position + new Vector3 (-PlaneWH.x,0,-PlaneWH.y);
		//获取面片左下角的世界坐标
		TopRight_Pl_W = Plane.transform.parent.position + new Vector3 (PlaneWH.x,0,PlaneWH.y);
		//获取面片右上角的世界坐标
		BottomRight_Pl_W = Plane.transform.parent.position + new Vector3 (PlaneWH.x,0,-PlaneWH.y);
		//获取面片右下角的世界坐标

		//将截图时识别图四个角的世界坐标信息传递给Shader
		Earth.GetComponent<Renderer>().material.SetVector("_Uvpoint1",new Vector4(TopLeft_Pl_W.x,TopLeft_Pl_W.y,TopLeft_Pl_W.z,1f));
		//将左上角的世界坐标传递给Shader ，其中1f是否了凑齐四位浮点数 ，用来进行后续的矩阵变换操作
		Earth.GetComponent<Renderer>().material.SetVector("_Uvpoint2",new Vector4(BottomLeft_Pl_W.x,BottomLeft_Pl_W.y,BottomLeft_Pl_W.z,1f));
		Earth.GetComponent<Renderer>().material.SetVector("_Uvpoint3",new Vector4(TopRight_Pl_W.x,TopRight_Pl_W.y,TopRight_Pl_W.z,1f));
		Earth.GetComponent<Renderer>().material.SetVector("_Uvpoint4",new Vector4(BottomRight_Pl_W.x,BottomRight_Pl_W.y,BottomRight_Pl_W.z,1f));

		//将截图时识别图四个角的世界坐标信息传递给Shader
		EarthFrame.GetComponent<Renderer>().material.SetVector("_Uvpoint1",new Vector4(TopLeft_Pl_W.x,TopLeft_Pl_W.y,TopLeft_Pl_W.z,1f));
		//将左上角的世界坐标传递给Shader ，其中1f是否了凑齐四位浮点数 ，用来进行后续的矩阵变换操作
		EarthFrame.GetComponent<Renderer>().material.SetVector("_Uvpoint2",new Vector4(BottomLeft_Pl_W.x,BottomLeft_Pl_W.y,BottomLeft_Pl_W.z,1f));
		EarthFrame.GetComponent<Renderer>().material.SetVector("_Uvpoint3",new Vector4(TopRight_Pl_W.x,TopRight_Pl_W.y,TopRight_Pl_W.z,1f));
		EarthFrame.GetComponent<Renderer>().material.SetVector("_Uvpoint4",new Vector4(BottomRight_Pl_W.x,BottomRight_Pl_W.y,BottomRight_Pl_W.z,1f));

		//将截图时识别图四个角的世界坐标信息传递给Shader
		EarthA.GetComponent<Renderer>().material.SetVector("_Uvpoint1",new Vector4(TopLeft_Pl_W.x,TopLeft_Pl_W.y,TopLeft_Pl_W.z,1f));
		//将左上角的世界坐标传递给Shader ，其中1f是否了凑齐四位浮点数 ，用来进行后续的矩阵变换操作
		EarthA.GetComponent<Renderer>().material.SetVector("_Uvpoint2",new Vector4(BottomLeft_Pl_W.x,BottomLeft_Pl_W.y,BottomLeft_Pl_W.z,1f));
		EarthA.GetComponent<Renderer>().material.SetVector("_Uvpoint3",new Vector4(TopRight_Pl_W.x,TopRight_Pl_W.y,TopRight_Pl_W.z,1f));
		EarthA.GetComponent<Renderer>().material.SetVector("_Uvpoint4",new Vector4(BottomRight_Pl_W.x,BottomRight_Pl_W.y,BottomRight_Pl_W.z,1f));


		Matrix4x4 P = GL.GetGPUProjectionMatrix (Camera.main.projectionMatrix,false);
		//获取截图时GPU的投影矩阵
		Matrix4x4 V=Camera.main.worldToCameraMatrix;
		//获取截图时世界坐标到相机的矩阵
		Matrix4x4 VP=P*V;
		//储存两个矩阵的乘积
		Earth.GetComponent<Renderer>().material.SetMatrix("_VP",VP);
		//将截图时的矩阵转换信息传递给Shader
		EarthFrame.GetComponent<Renderer>().material.SetMatrix("_VP",VP);
		//将截图时的矩阵转换信息传递给Shader
		EarthA.GetComponent<Renderer>().material.SetMatrix("_VP",VP);
		//将截图时的矩阵转换信息传递给Shader

		TextureShot.ReadPixels (new Rect(0,0,ScreenWidth,ScreenHeight),0,0);
		//获取屏幕的像素信息 
		//第一个"0,0"获取屏幕像素的起始点
		//“ScreenWidth,ScreenHeight”获取屏幕像素的范围
		//第二个“0,0” 填充texture2D时填充的坐标

		TextureShot.Apply ();
		//确认之前对Texture2D进行的修改

		Earth.GetComponent<Renderer> ().material.mainTexture = TextureShot;
		//获取Earth的渲染组件中的材质的主纹理，并将Texture2D赋值给这个主纹理
		EarthFrame.GetComponent<Renderer> ().material.mainTexture = TextureShot;
		//获取Earth的渲染组件中的材质的主纹理，并将Texture2D赋值给这个主纹理
		EarthA.GetComponent<Renderer> ().material.mainTexture = TextureShot;
		//获取Earth的渲染组件中的材质的主纹理，并将Texture2D赋值给这个主纹理

		Plane.SetActive (false);
		//取消面片的激活状态
	}


}
