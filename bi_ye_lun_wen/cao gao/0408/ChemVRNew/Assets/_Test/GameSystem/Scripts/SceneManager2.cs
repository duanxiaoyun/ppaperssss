using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager2 : MonoBehaviour {

    public Image middlepic;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loadangfa()
    {
        middlepic.GetComponent<Image>().sprite = Resources.Load<Sprite>("fangfa");
    }
    public void loadyuansubiao()
    {
        middlepic.GetComponent<Image>().sprite = Resources.Load<Sprite>("ShinyPeriodicTable_1");
    }
    public void loadyuanli()
    {
        middlepic.GetComponent<Image>().sprite = Resources.Load<Sprite>("yuanli");
    }
    public void loadintro()
    {
        middlepic.GetComponent<Image>().sprite = Resources.Load<Sprite>("intro");
    }
    public void loadvideo()
    {
        middlepic.GetComponent<Image>().sprite = Resources.Load<Sprite>("video");
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("Main");
        Debug.Log("Main");
    }
}
