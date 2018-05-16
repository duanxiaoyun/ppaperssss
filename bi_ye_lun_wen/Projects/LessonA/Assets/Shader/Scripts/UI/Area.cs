//命名空间
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//加入UI命名空间

public class Area : MonoBehaviour {
//"Area"类名 ，需要和外部脚本名称保持一致
	public GameObject SuccessPlane_Image;
	//储存识别成功图片
	public GameObject Earth;
	//储存地球模型
	public Material Green_Mate;
	//申请材质变量储存绿色的材质
	public Material Red_Mate;
	//申请材质变量储存红色材质
	public Material Tran_Mate;
	//申请材质变量储存透明材质

	private bool HasRe=false;
	//申请布尔变量来确定是否已经识别
	private CanvasScaler CanS;
	//申请变脸储存UI屏幕自适度的缩放组件

	private float X_Sc;
	//申请浮点型类型的变量储存实际的缩放比例

	//记录扫描框的范围
	private Vector2 TopLeft_UI;
	//记录扫描框左上角的坐标
	//“private”申请类型为私有
	private Vector2 BottomLeft_UI;
	//记录扫描框左下角的坐标
	private Vector2 TopRight_UI;
	//记录扫描框右上角的坐标
	private Vector2 BottomRight_UI;
	//记录扫描框右下角的坐标

	//记录面片的世界坐标
	private Vector3 TopLeft_Pl_W;
	//记录面片左上角的世界坐标
	private Vector3 BottomLeft_Pl_W;
	//记录面片左下角的世界坐标
	private Vector3 TopRight_Pl_W;
	//记录面片右上角的世界坐标
	private Vector3 BottomRight_Pl_W;
	//记录面片右下角的世界坐标

	//记录面片的屏幕坐标
	private Vector2 TopLeft_Pl_Sc;
	//记录面片左上角的屏幕坐标
	private Vector2 BottomLeft_Pl_Sc;
	//记录面片坐下角的屏幕坐标
	private Vector2 TopRight_Pl_Sc;
	//记录面片右上角的屏幕坐标
	private Vector2 BottomRight_Pl_Sc;
	//记录面片右下角的屏幕坐标

	private Vector2 PlaneWH;
	//记录面片的宽高


	//脚本刚开始运行的时候调用一次
	// Use this for initialization
	void Start () {
		
		CanS = GameObject.Find ("Canvas").gameObject.GetComponent<CanvasScaler> ();
		//获取控制屏幕自适度的组件

		X_Sc = Screen.width / CanS.referenceResolution.x;
		//获取实际的缩放比例
	
	}

	//每一帧都调用
	// Update is called once per frame
	void Update () {

		//计算了扫描框四个点的坐标位置，“*X_Sc"是屏幕自适度的缩放比例，这样才能获取真正运行时UI图片的宽高
		TopLeft_UI = new Vector2 (Screen.width-400*X_Sc,Screen.height+300*X_Sc)*0.5f;
		//给扫描框左上角的坐标赋值
		//"Screen.width-400,Screen.height+300" 屏幕的宽度减去扫描框的宽度，屏幕的高度减去扫描框的高度
		BottomLeft_UI=new Vector2(Screen.width-400*X_Sc,Screen.height-300*X_Sc)*0.5f;
		//给扫描框左下角的坐标赋值
		TopRight_UI=new Vector2(Screen.width+400*X_Sc,Screen.height+300*X_Sc)*0.5f;
		//给扫描框右上角的坐标赋值
		BottomRight_UI=new Vector2(Screen.width+400*X_Sc,Screen.height-300*X_Sc)*0.5f;
		//给扫描框右下角的坐标赋值

		PlaneWH = new Vector2 (gameObject.GetComponent<MeshFilter>().mesh.bounds.size.x,gameObject.GetComponent<MeshFilter>().mesh.bounds.size.z)*5*0.5f;
		//获取面片的宽高的一半
		//"gameObject.GetComponent<MeshFilter>().mesh.bounds.size.x"获取面片X方向的宽度
		//"*5"是因为开始获取到的长宽是模型本身的长宽，而场景中我们有缩放因素，父级物体放大了50倍，自身缩小到了0.1，因此获取实际宽高需要再乘以5

		//获取面片四个点的世界坐标
		TopLeft_Pl_W = gameObject.transform.parent.position + new Vector3 (-PlaneWH.x,0,PlaneWH.y);
		//获取面片左上角的世界坐标
		//"gameObject.transform.parent.position"物体的父级物体的世界坐标
		//"new Vector2 (-PlaneWH.x,PlaneWH.y)"向左上方偏移的量
		BottomLeft_Pl_W = gameObject.transform.parent.position + new Vector3 (-PlaneWH.x,0,-PlaneWH.y);
		//获取面片左下角的世界坐标
		TopRight_Pl_W = gameObject.transform.parent.position + new Vector3 (PlaneWH.x,0,PlaneWH.y);
		//获取面片右上角的世界坐标
		BottomRight_Pl_W = gameObject.transform.parent.position + new Vector3 (PlaneWH.x,0,-PlaneWH.y);
		//获取面片右下角的世界坐标


		//获取面片的屏幕坐标
		TopLeft_Pl_Sc=Camera.main.WorldToScreenPoint(TopLeft_Pl_W);
		//获取面片左上角的屏幕坐标
		//Camera.main.WorldToScreenPoint(Vector3()); 将世界坐标转化为屏幕坐标
		BottomLeft_Pl_Sc=Camera.main.WorldToScreenPoint(BottomLeft_Pl_W );
		//获取面片左下角的屏幕坐标
		TopRight_Pl_Sc=Camera.main.WorldToScreenPoint(TopRight_Pl_W);
		//获取面片右上角的屏幕坐标
		BottomRight_Pl_Sc=Camera.main.WorldToScreenPoint(BottomRight_Pl_W );
		//获取面片右下角的屏幕坐标

		//判断面片是否在扫描框范围内
		if(TopLeft_Pl_Sc.x>TopLeft_UI.x&&TopLeft_Pl_Sc.y<TopLeft_UI.y&&BottomLeft_Pl_Sc.x>BottomLeft_UI.x&&BottomLeft_Pl_Sc.y>BottomLeft_UI.y&&TopRight_Pl_Sc.x<TopRight_UI.x&&TopRight_Pl_Sc.y<TopLeft_UI.y&&BottomRight_Pl_Sc.x<BottomRight_UI.x&&BottomRight_Pl_Sc.y>BottomRight_UI.y){
		//当面片完全处于扫描框范围内时 执行以下代码
			if(HasRe==false){
            //如果尚未识别
			   gameObject.GetComponent<Renderer>().material=Green_Mate;
			   //将脚本所附着的物体（面片）的材质变为绿色材质
			   StartCoroutine("SuccessUI");
			   //调用显示识别成功图片的延迟函数
			   StartCoroutine("ScreenShot");
			   //调用截图的延迟函数
			   HasRe=true;
			   //已经识别
			}
			
		}else{
        //当面片并非完全处于扫描框范围内时  执行以下代码
			gameObject.GetComponent<Renderer>().material=Red_Mate;
			//将脚本所附着的物体（面片）的材质变为红色材质
			HasRe=false;
			//识别状态设置为未识别
		}


	
	}

	//显示识别成功图片的延迟函数
	IEnumerator SuccessUI (){
		yield return new WaitForSeconds (0.5f);
		//延迟0.5秒
		SuccessPlane_Image.SetActive(true);
		//激活提示识别成功的图片
		gameObject.GetComponent<Renderer>().material=Tran_Mate;
		//给面片材质赋值为透明材质，出去截图时的影响
	}

	//截图的延迟函数
	IEnumerator ScreenShot(){
		yield return new WaitForSeconds (2.0f);
		//延迟2秒
		if(HasRe==true){
        //当处于识别状态的时候才执行截图函数
		gameObject.GetComponent<Renderer>().material=Tran_Mate;
		//给面片材质赋值为透明材质，出去截图时的影响
		Earth.GetComponent<ScreenShot>().ScreenShot_Button();
		//调用地球模型上截图脚本的截图函数
		}
	}



}
