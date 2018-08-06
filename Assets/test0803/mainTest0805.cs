using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class mainTest0805 : MonoBehaviour {

	public int width=0;
	public int height=0;
	public bool showOld=false;
	public string tips_mode="";



	public RawImage rawLeft;
	public Text ShowOldText;
	public Text showTitle;

	public List<Vector2> raw_image = new List<Vector2> ();
	public List<Vector2> new_image = new List<Vector2> ();


	public List<MovePic> listPoint = new List<MovePic> ();


	public int adjustPoint =0;
	public bool isAdjustRaw=false;


	public RectTransform moveRT;

	// Use this for initialization
	void Start () {
		width = Screen.width;
		height = Screen.height;

		t2d = new Texture2D (width,height,TextureFormat.RGB24,false);
		rt = new RenderTexture (width,height,24);
		mainCamera.targetTexture = rt;

		moveRT = listPoint [adjustPoint].gameObject.GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector2 pos_1 = moveRT.anchoredPosition;
		float x=pos_1.x;
		float y=pos_1.y;
		if (Input.GetKey (KeyCode.UpArrow)) {
			y += 1;
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			y -= 1;
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			x -= 1;
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			x += 1;
		}
		moveRT.anchoredPosition = new Vector2 (x,y);

	}


	public Camera mainCamera;
	public RenderTexture rt;
	public Texture2D t2d;
	public int num=0;
	public string path="";
	public void SnapPic(){

		num++;
		RenderTexture.active = rt;
		t2d.ReadPixels (new Rect(0,0,rt.width,rt.height),0,0);
		t2d.Apply ();
		rawLeft.texture = t2d;
		RenderTexture.active = null;
		byte[] byt = t2d.EncodeToJPG ();
		File.WriteAllBytes ("F://img/233.jpg",byt);
	}


	public void showOldPic(){
		showOld = !showOld;
		rawLeft.gameObject.SetActive (showOld);
		if (showOld) {
			ShowOldText.text = "show new";
		} else {
			ShowOldText.text = "show old";
		}

	}

	private void SetNewPos(){
		Vector2	ImagePos = RectTransformUtility.WorldToScreenPoint (Camera.main,moveRT.transform.position);
		if (isAdjustRaw) {
			raw_image[adjustPoint] = new Vector2 (ImagePos.x,Screen.height-ImagePos.y);
		} else {
			new_image[adjustPoint] = new Vector2 (ImagePos.x,Screen.height-ImagePos.y);
		}
	}

	public void SetIndex(){
		SetNewPos ();
		adjustPoint++;

		if (adjustPoint >= 4) {
			adjustPoint = 0;
		}
		moveRT = listPoint [adjustPoint].gameObject.GetComponent<RectTransform> ();
		showTitle.text = showOld+"   "+adjustPoint;
	}

	public void SetAdjustMode(){
		isAdjustRaw = !isAdjustRaw;
		showTitle.text = isAdjustRaw+"   "+adjustPoint;
	}
}
