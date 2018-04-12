using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyOVRInspector : MonoBehaviour {
    public OVRPlayerController playerController { get; private set; }
    static public OVRCameraRig cameraRig
    {
        get
        {
            return GameObject.Find("OVRCameraRig").GetComponent<OVRCameraRig>();
        }
    }

    public GameObject leftCamera { get; private set; }
    public GameObject rightCamera { get; private set; }
    public OVRManager manager { get; private set; }
    public Transform centerEyeTransform { get; private set; }

    private int playerLayer;
    // Input module
    private OVRInputModule inputModule;

    //Cache of playcontroller state
    private bool previouslySkippingMouseRotation = true;
    private bool previouslyHaltedMovementUpdate = true;

    // GUI Canvas and Panels
    GameObject canvas;

    // Prefabs
    private EventSystem eventSystemPrefab;

    private void Awake()
    {
        playerLayer = GetLayerOrReportError("Player");
        eventSystemPrefab = (EventSystem)Resources.Load("Prefabs/EventSystem", typeof(EventSystem));
        // Setup canvas and canvas panel builders 
        canvas = transform.Find("Canvas").gameObject;
        AssignCameraRig();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        OVRInput.Controller activeController = OVRInput.GetActiveController();
        Transform activeTransform = cameraRig.centerEyeAnchor;

        if ((activeController == OVRInput.Controller.LTouch) || (activeController == OVRInput.Controller.LTrackedRemote))
            activeTransform = cameraRig.leftHandAnchor;

        if ((activeController == OVRInput.Controller.RTouch) || (activeController == OVRInput.Controller.RTrackedRemote))
            activeTransform = cameraRig.rightHandAnchor;

        if (activeController == OVRInput.Controller.Touch)
            activeTransform = cameraRig.rightHandAnchor;

        OVRGazePointer.instance.rayTransform = activeTransform;
        inputModule.rayTransform = activeTransform;
    }

    int GetLayerOrReportError(string layer)
    {
        int layerIndex = LayerMask.NameToLayer(layer);
        if (layerIndex == -1)
        {
            Debug.LogError(string.Format("No \"{0}\" layer exists", layer));
        }
        return layerIndex;
    }

    public void AssignCameraRig()
    {
        FindPlayerAndCamera();
        // There has to be an event system for the GUI to work
        EventSystem eventSystem = GameObject.FindObjectOfType<EventSystem>();
        if (eventSystem == null)
        {
            Debug.Log("Creating EventSystem");
            eventSystem = (EventSystem)GameObject.Instantiate(eventSystemPrefab);

        }
        else
        {
            //and an OVRInputModule
            if (eventSystem.GetComponent<OVRInputModule>() == null)
            {
                eventSystem.gameObject.AddComponent<OVRInputModule>();
            }
        }
        inputModule = eventSystem.GetComponent<OVRInputModule>();

        playerController = FindObjectOfType<OVRPlayerController>();
        if (playerController)
        {
            CachePlayerControlDefaults();
        }
        cameraRig.EnsureGameObjectIntegrity();
        canvas.GetComponent<Canvas>().worldCamera = cameraRig.leftEyeCamera;
    }

    void FindPlayerAndCamera()
    {
        playerController = FindObjectOfType<OVRPlayerController>();
        if (playerController && playerController.gameObject.layer != playerLayer)
        {
            Debug.LogError("PlayerController should be layer \"Player\"");
        }


        if (cameraRig)
        {
            Transform t = cameraRig.transform.Find("TrackingSpace");
            centerEyeTransform = t.Find("CenterEyeAnchor");
        }

        manager = FindObjectOfType<OVRManager>();
    }

    #region Player Controller Caching
    void CachePlayerControlDefaults()
    {
        playerController.GetSkipMouseRotation(ref previouslySkippingMouseRotation);
        playerController.GetHaltUpdateMovement(ref previouslyHaltedMovementUpdate);
    }

    void SetPlayerControlDefaults()
    {
        playerController.SetSkipMouseRotation(previouslySkippingMouseRotation);
        playerController.SetHaltUpdateMovement(previouslyHaltedMovementUpdate);
    }
    #endregion Interaction With Player Controller
}
