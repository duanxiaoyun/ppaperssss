using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagers : MonoBehaviour {

    public GameObject text1;
    public GameObject text2;
    public GameObject text3;

    public GameObject selected;

    public GameObject mg;
    public GameObject o2;

    // Use this for initialization
    void Start () {
        mg.transform.position = new Vector3(-1.468f,2.633f,0.41f);
        o2.transform.position = new Vector3(1.279f,2.829f,0.424f);
    }
	
	// Update is called once per frame
	void Update () {
      
        
	}

    public void GoToChem()
    {
        SceneManager.LoadScene("ChemVR");
        Debug.Log("ChemVR");
    }
 

    public void ShowInfo1()
    {
        text2.SetActive(false);
        text3.SetActive(false);
        text1.SetActive(true);
        StartCoroutine(loadselected());
        Debug.Log("text1");
    }
    public void ShowInfo2()
    {
        text1.SetActive(false);
        text3.SetActive(false);
        text2.SetActive(true);
        Debug.Log("text2");
    }
    public void ShowInfo3()
    {
        text1.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(true);
        Debug.Log("text2");
    }
    public void Showvideo()
    {
        text2.SetActive(false);
        text1.SetActive(true);
        Debug.Log("text1");
    }

    IEnumerator loadselected()
    {
        selected.SetActive(true);
        yield return new WaitForSeconds(1); 
        selected.SetActive(false);
        yield return new WaitForSeconds(1); 
        selected.SetActive(true);
    }


    public void Quitgame()
    {
       
        #if UNITY_EDITOR

          UnityEditor.EditorApplication.isPlaying = false;

        #else
         Application.Quit();
        #endif
 
    }
    

}
