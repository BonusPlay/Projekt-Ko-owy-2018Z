using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideDoors : MonoBehaviour {

    [SerializeField]
    private Collider2D Col2D;
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;
        float dist = Vector2.Distance(new Vector2(pz.x,pz.y), new Vector2(transform.position.x, transform.position.y));
        Col2D.enabled = dist < 1f ? false : true;
    }
}
