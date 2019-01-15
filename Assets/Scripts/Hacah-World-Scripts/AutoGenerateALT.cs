using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGenerateALT : MonoBehaviour {

    public static GameObject AltPrefab;
    public static Transform AltParent;

    private void Start()
    {
        if (AltParent == null)
        {
            AltParent = GameObject.FindGameObjectWithTag("AltParent").transform;
        }
        if (AltPrefab == null)
        {
            AltPrefab =  (GameObject)Resources.Load("ALT");
        }
        var alt = Instantiate(AltPrefab, AltParent);
        alt.transform.position = transform.position;
        alt.transform.rotation = transform.rotation;
        alt.transform.localScale = transform.localScale;
        alt.gameObject.name = gameObject.name + " ALT (Auto)";
    }
}
