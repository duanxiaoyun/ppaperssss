using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meitiao : MonoBehaviour {

    public bool isFire = false;
    public bool isFired { get; private set; }
    public GameObject fire;
    public MeshRenderer offsetControll;


	// Use this for initialization
	void Start () {
        isFired = false;
        SetFire(isFire);
    }
	
	// Update is called once per frame.
	void Update () {
        if (isFire)
        {
            float deltaTime = Time.deltaTime * 0.33f;
            offsetControll.material.mainTextureOffset += new Vector2(0.5f, 0) * deltaTime;
            fire.transform.localPosition += fire.transform.right * deltaTime;
            if (fire.transform.localPosition.x >= 0.5f) {
                SetFire(false);
                isFired = true;
            }
        }
	}

    public void SetFire(bool active) {
        isFire = active;
        fire.SetActive(active);
    }


}
