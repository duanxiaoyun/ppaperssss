using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRInputTest : MonoBehaviour {

    public Vector2 lTrackPrimaryTouchpad;
    public Vector2 rTrackPrimaryTouchpad;

    // Use this for initialization
    void Start () {

	}
    // Update is called once per frame
    void Update () {
        //shua xin rift touch 
       //OVRInput.Update();
       if (OVRInput.GetDown(OVRInput.Touch.Two))
        {
            Debug.Log("OVRInput.GetDown.Touch.Two");
        }

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Debug.Log("OVRInput.GetDown.Touch.One");
        }

        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            Debug.Log("OVRInput.GetDown.Touch.Three");
        }

        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            Debug.Log("OVRInput.GetDown.Touch.Four");
        }

        if (OVRInput.GetDown(OVRInput.Button.Left))
        {
            Debug.Log("OVRInput.GetDown.Button.Left");
        }

        if (OVRInput.GetDown(OVRInput.Button.Right))
        {
            Debug.Log("OVRInput.GetDown.Button.Right");
        }

        if (OVRInput.GetDown(OVRInput.Button.Back))
        {
            Debug.Log("OVRInput.GetDown.Button.Back");
        }

        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            Debug.Log("OVRInput.GetDown.Button.Start");
        }

        //lTrackPrimaryTouchpad = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, OVRInput.Controller.LTrackedRemote);
        //rTrackPrimaryTouchpad = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, OVRInput.Controller.RTrackedRemote);

        lTrackPrimaryTouchpad = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        rTrackPrimaryTouchpad = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);


        //if (Mathf.Abs(rTrackPrimaryTouchpad.x) > 0.1f)
        //{
        //    Debug.Log(Mathf.Abs(rTrackPrimaryTouchpad.x));
        //}

        // (X/Y range of -1.0f to 1.0f)
        //OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        //if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick))
        //{
        //    Debug.Log("OVRInput.Button.PrimaryThumbstick");
        //}

        //if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick))
        //{
        //    Debug.Log("OVRInput.Button.PrimaryThumbstick");
        //}

        //// (Up/Down/Left/Right - Interpret the thumbstick as a D-pad).
        //if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickUp))
        //{
        //    Debug.Log("OVRInput.Button.PrimaryThumbstickUp");
        //}

    }
}
