using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOMoveComponent : MonoBehaviour {

    [Header("控制的GameObject")]
    public Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        if(h != 0)
        {
            target.position = new Vector3(target.position.x + 0.1f * (h > 0 ? 1 : -1), target.position.y, target.position.z);
        }

        if (v != 0)
        {
            target.position = new Vector3(target.position.x, target.position.y, target.position.z + 0.1f * (v > 0 ? 1 : -1));
        }
    }
}
