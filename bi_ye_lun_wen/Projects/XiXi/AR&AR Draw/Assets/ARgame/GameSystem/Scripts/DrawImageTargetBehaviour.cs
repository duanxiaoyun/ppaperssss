
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace EasyAR
{
    public class DrawImageTargetBehaviour : ImageTargetBehaviour
    {
        public Action<DrawImageTargetBehaviour,bool> onTargetWillFind;
        public Action<DrawImageTargetBehaviour> onTargetDidFind;
        public Action<DrawImageTargetBehaviour> onTargetWillLost;
        public Action<DrawImageTargetBehaviour> onTargetDidLost;
        public Action<bool> onContainPanel;
        //public Action onScant;

        public GameObject environment;
        public Transform redPanel;
        public bool isContainPanel = false;
        private Camera arCamera;
        private RectTransform rect_frame;

        public Renderer panelRenderer,targetRenderer;
        public Camera targetCamera;
        private Camera mainCamera;

        private Texture2D scantTexture;
        public bool isApply=false;
        public bool isFind = false;
        public bool isWaitScan = true;
        public float isLostTime;
        private Animation targetAnimation;
        private LayerMask targetLayer;
        float playingTime;
        bool isPlaying;

        protected override void Awake()
        {
            base.Awake();
            GameObjectActiveControl = false;
            TargetFound += OnTargetFound;
            TargetLost += OnTargetLost;
        }

        protected override void Start()
        {
            base.Start();

            mainCamera = Camera.main;
            SetActive(false);
            isApply = false;
            isFind = false;
            isWaitScan = true;
            targetLayer = 1<<targetRenderer.gameObject.layer;
            targetAnimation = targetRenderer.transform.parent.GetComponent<Animation>();
            targetAnimation.playAutomatically = false;
        }

        public void Init(Camera arCam,RectTransform frame){
            arCamera = arCam;
            rect_frame = frame;
        }

        void OnTargetFound(TargetAbstractBehaviour behaviour)
        {
            isApply = environment.activeSelf;
            if(onTargetWillFind!=null)
                onTargetWillFind(this,isApply);
            gameObject.SetActive(true);
            isFind = true;
            isLostTime = float.MaxValue;
            if (isApply)
            {
                environment.SetActive(true);
                if (isPlaying)
                {
                    targetAnimation.Play();
                    targetAnimation["Take 001"].time = playingTime;
                }

            }
            else
            {
                rect_frame.gameObject.SetActive(true);
                redPanel.gameObject.SetActive(true);
                isWaitScan = true;
            }
        }

        void OnTargetLost(TargetAbstractBehaviour behaviour)
        {
            isFind = false;
            redPanel.gameObject.SetActive(false);
            isLostTime = Time.time;
            if (onTargetWillLost != null)
                onTargetWillLost(this);
        }

        protected override void Update()
        {
            base.Update();
            if(!isApply){
                Matrix4x4 P = GL.GetGPUProjectionMatrix(mainCamera.projectionMatrix, false);
                Matrix4x4 V = mainCamera.worldToCameraMatrix;
                Matrix4x4 M = panelRenderer.localToWorldMatrix;
                Matrix4x4 MVP = P * V * M;
                panelRenderer.material.SetMatrix("_MATRIX_MVP", MVP);//截图传入shader中处理
            }

            if (isFind)
            {
                Vector3 vec = redPanel.localScale * 0.5f;
                isContainPanel = RectTransformUtility.RectangleContainsScreenPoint(rect_frame, arCamera.WorldToScreenPoint(redPanel.position - redPanel.right * vec.x - redPanel.up * vec.y));
                if (isContainPanel)
                    isContainPanel = RectTransformUtility.RectangleContainsScreenPoint(rect_frame, arCamera.WorldToScreenPoint(redPanel.position + redPanel.right * vec.x - redPanel.up * vec.y));
                if (isContainPanel)
                    isContainPanel = RectTransformUtility.RectangleContainsScreenPoint(rect_frame, arCamera.WorldToScreenPoint(redPanel.position + redPanel.right * vec.x + redPanel.up * vec.y));
                if (isContainPanel)
                    isContainPanel = RectTransformUtility.RectangleContainsScreenPoint(rect_frame, arCamera.WorldToScreenPoint(redPanel.position - redPanel.right * vec.x + redPanel.up * vec.y));
                if (!isApply)
                {
                    if (!isContainPanel)
                    {
                        //alert.SetMsg("请对准整个画面").Show();
                        if (onContainPanel != null)
                            onContainPanel(false);
                    }
                    else
                    {
                        if (isWaitScan)
                        {
                            StartCoroutine(ScantTexture());
                        }
                        //alert.SetMsg("扫描成功").Show();
                        if (onContainPanel != null)
                            onContainPanel(true);
                    }
                }
            }
            else if(gameObject.activeSelf && Time.time - isLostTime>2)
            {
                SetActive(false);
                rect_frame.gameObject.SetActive(true);
            }

            if(Input.GetMouseButtonUp(0)){
                if(Physics.Raycast(arCamera.ScreenPointToRay(Input.mousePosition),1000,targetLayer)){
                    if(!targetAnimation.isPlaying)
                        targetAnimation.Play();
                }
            }
        }

        IEnumerator ScantTexture(){
            isWaitScan = false;
            yield return new WaitForSeconds (1);
            if (isContainPanel && !isApply)
            {
                redPanel.gameObject.SetActive(false);
                environment.SetActive(true);
                RenderTexture renderTexture = targetCamera.targetTexture;
                if (!scantTexture)
                {
                    scantTexture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
                }
                RenderTexture.active = renderTexture;
                scantTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
                scantTexture.Apply();
                RenderTexture.active = null;
//                Debug.Log("ScanRenderTexture");
                targetRenderer.material.SetTexture("_MainTex", scantTexture);
                rect_frame.gameObject.SetActive(false);
                isApply = true;
                //alert.Show(false);
                if (onTargetDidFind != null)
                    onTargetDidFind(this);
            }
            isWaitScan = true;
        }

        public void SetActive(bool value){
            if (targetAnimation != null && targetAnimation.isPlaying)
            {
                isPlaying = true;
                playingTime = targetAnimation["Take 001"].time;
                targetAnimation["Take 001"].time = 0;
                targetAnimation.Sample();
                targetAnimation.Stop();
            }
            else
            {
                isPlaying = false;
            }
            if (!value && environment.activeSelf && onTargetDidLost!=null)
            {
                onTargetDidLost(this);   
            }
            gameObject.SetActive(value);
            environment.SetActive(value);

        }
    }
}
