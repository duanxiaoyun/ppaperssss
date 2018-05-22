using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaobei : MonoBehaviour {

    public GameObject qipao;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "meitiao1"&& other.GetComponent<Meitiao>().isFired)
        {
            qipao.SetActive(true);
        }
    }
}
