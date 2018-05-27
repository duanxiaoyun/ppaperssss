using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VR;

public class MainMenu : MonoBehaviour {
    public Button btn_ar,btn_arDraw,btn_vr;

    private void Awake()
    {
    }

    // Use this for initialization
    void Start () {
        btn_ar.onClick.AddListener(GotoAR);
        btn_arDraw.onClick.AddListener(GotoARDraw);
        btn_vr.onClick.AddListener(GotoVR);
	}

    void GotoAR()
    {
        SceneManager.LoadScene("AR");
    }

    void GotoARDraw()
    {
        SceneManager.LoadScene("ARDraw");
    }

    void GotoVR()
    {
        SceneManager.LoadScene("VR");
    }
}
