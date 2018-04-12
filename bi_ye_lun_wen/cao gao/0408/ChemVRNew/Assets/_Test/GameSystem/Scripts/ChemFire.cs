using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemFire : MonoBehaviour {

    public GameObject _match;
    public GameObject _cube;
    public GameObject _fire;

    bool isFire;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "match")
        {
            _fire.SetActive(true);
            isFire = true;
            Debug.Log("match");
        }
        if (other.gameObject.tag == "meitiao1" && isFire)
        {
            Meitiao meitiao = other.GetComponent<Meitiao>();
            if(!meitiao.isFired)
                meitiao.SetFire(true);
            //_meitiao1.GetComponent<MeshRenderer>().material/*SetTextureScale("wb",new Vector2(0.5f,0));*/
            Debug.Log("meitiao1");
        }

        if (other.gameObject.tag == "gaizi")
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
            //Destroy(other.GetComponent<OVRGrabbable>());
            //other.GetComponent<OVRGrabbable>().gameObject = false;
            other.transform.position = new Vector3(0.0616f, 1.1169f, -0.7143f);
            other.transform.eulerAngles = new Vector3(-90, 0, 0);
            _fire.SetActive(false);
            isFire = false;
            Debug.Log("gaizi");
        }

    }

}
