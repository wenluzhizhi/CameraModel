using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePic : MonoBehaviour {

	public RectTransform rt;
	public Vector2 ImagePos;
	void Start () {
		rt = this.gameObject.GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () {



		ImagePos = RectTransformUtility.WorldToScreenPoint (Camera.main, rt.transform.position);
	}



}
