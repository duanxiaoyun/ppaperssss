//=============================================================================================================================
//
// Copyright (c) 2015-2017 VisionStar Information Technology (Shanghai) Co., Ltd. All Rights Reserved.
// EasyAR is the registered trademark or trademark of VisionStar Information Technology (Shanghai) Co., Ltd in China
// and other countries for the augmented reality technology developed by VisionStar Information Technology (Shanghai) Co., Ltd.
//
//=============================================================================================================================

using UnityEngine;
using EasyAR;

namespace Sample
{
    public class Coloring3DBehaviour : MonoBehaviour
    {
        Camera cam;
        RenderTexture renderTexture;
        ImageTargetBaseBehaviour targetBehaviour;
        public LayerMask cullingMask;

        void Start()
        {
            targetBehaviour = GetComponentInParent<ImageTargetBaseBehaviour>();
            gameObject.layer = 31;//LayerMask.NameToLayer("Coloring");
        }

        void Renderprepare()
        {
            if (!cam)//这里的cam暂时就叫它贴图相机，它的作用是获取一张屏幕截图（注意是整个屏幕的截图）
            {
                GameObject go = new GameObject("__cam");
                cam = go.AddComponent<Camera>();
                go.transform.parent = transform.parent;
                cam.hideFlags = HideFlags.HideAndDontSave;
            }
            cam.CopyFrom(Camera.main);//从主相机中获取一帧
            cam.depth = 0;
            cam.cullingMask = 31;

            if (!renderTexture)
            {
                renderTexture = new RenderTexture(Screen.width, Screen.height, -50);
            }
            cam.targetTexture = renderTexture;
            cam.Render();
            GetComponent<Renderer>().material.SetTexture("_MainTex", renderTexture);//把截图付给模型
        }

        void OnWillRenderObject()
        {
            if (!targetBehaviour || targetBehaviour.Target == null)
                return;
            Vector2 halfSize = targetBehaviour.Target.Size * 0.5f;//通过识别图的中心点得到识别图的四个定点 
            Vector3 targetAnglePoint1 = transform.parent.TransformPoint(new Vector3(-halfSize.x, 0, halfSize.y));
            Vector3 targetAnglePoint2 = transform.parent.TransformPoint(new Vector3(-halfSize.x, 0, -halfSize.y));
            Vector3 targetAnglePoint3 = transform.parent.TransformPoint(new Vector3(halfSize.x, 0, halfSize.y));
            Vector3 targetAnglePoint4 = transform.parent.TransformPoint(new Vector3(halfSize.x, 0, -halfSize.y));
            //获取一张屏幕贴图并付给model
            Renderprepare();
            //把四个UV点设置成刚获取的屏幕截屏中识别图的四个点到此结束
            GetComponent<Renderer>().material.SetVector("_Uvpoint1", new Vector4(targetAnglePoint1.x, targetAnglePoint1.y, targetAnglePoint1.z, 1f));
            GetComponent<Renderer>().material.SetVector("_Uvpoint2", new Vector4(targetAnglePoint2.x, targetAnglePoint2.y, targetAnglePoint2.z, 1f));
            GetComponent<Renderer>().material.SetVector("_Uvpoint3", new Vector4(targetAnglePoint3.x, targetAnglePoint3.y, targetAnglePoint3.z, 1f));
            GetComponent<Renderer>().material.SetVector("_Uvpoint4", new Vector4(targetAnglePoint4.x, targetAnglePoint4.y, targetAnglePoint4.z, 1f));
        }

        void OnDestroy()
        {
            if (renderTexture)
                DestroyImmediate(renderTexture);
            if (cam)
                DestroyImmediate(cam.gameObject);
        }
    }
}
