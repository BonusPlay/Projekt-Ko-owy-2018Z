using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInGame : MonoBehaviour {

    private SpriteRenderer SR;

    private void Start()
    {
        SR = this.gameObject.GetComponent<SpriteRenderer>();
        SR.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        
    }
}
