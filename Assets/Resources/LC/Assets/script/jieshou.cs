using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class jieshou : MonoBehaviour {
	public float carpx1;
	public float carpy1;
	public float carpz1;
	public float carrx1;
	public float carry1;
	public float carrz1;
	public GameObject car;
	public GameObject zc;      //黑屏板
	public GameObject gb;      //myobject
	public GameObject mcamera;
	public static float mcameray;
	public static string scene;
	public GameObject pingban;   //摄像机前的隐形面板
	public GameObject qiu;      //摄像机后的隐形的球
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//carpx1 = Convert.ToSingle(PhotonEngine1.carpx);
		//carpy1 = Convert.ToSingle(PhotonEngine1.carpy);
		//carpz1 = Convert.ToSingle(PhotonEngine1.carpz);

		//carrx1 = Convert.ToSingle(PhotonEngine1.carrx);
		//carry1 = Convert.ToSingle(PhotonEngine1.carry);
		//carrz1 = Convert.ToSingle(PhotonEngine1.carrz);

		//mcameray = Convert.ToSingle(PhotonEngine1.gd);

		car.transform.position = new Vector3(carpx1, carpy1, carpz1);
		car.transform.rotation = Quaternion.Euler(carrx1, carry1, carrz1);
		/*mcamera.transform.localPosition = new Vector3(0, mcameray, 0);
		pingban.transform.localPosition = new Vector3(0, mcamera.transform.localPosition.y, 1.1f);
		qiu.transform.localPosition = new Vector3(0, mcamera.transform.localPosition.y, -1.5f);
*/
	/*	if (PhotonEngine.zhencha == "kai")
		{
			zc.gameObject.SetActive(false);
		}

		if (PhotonEngine.zhencha == "guan")
		{
			zc.gameObject.SetActive(true);
		}*/

		/*if (PhotonEngine.scene == "沙漠")
		{
			SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
			//Application.LoadLevel(0);

			PhotonEngine.scene = "break";
            Destroy(gb);
        }
		if (PhotonEngine.scene == "山区")
		{
			SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
			PhotonEngine.scene = "break";
            Destroy(gb);
        }
		if (PhotonEngine.scene == "草地")
		{
			SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);
			PhotonEngine.scene = "break";
			Destroy(gb);
		}
		if (PhotonEngine.scene == "城市")
		{
			SceneManager.LoadSceneAsync(5, LoadSceneMode.Additive);

			PhotonEngine.scene = "break";
			Destroy(gb);
		}
		if (PhotonEngine.scene == "机场")
		{
			SceneManager.LoadSceneAsync(6, LoadSceneMode.Additive);

			PhotonEngine.scene = "break";
			Destroy(gb);
		}
		if (PhotonEngine.scene == "要地")
		{
			SceneManager.LoadSceneAsync(7, LoadSceneMode.Additive);
			PhotonEngine.scene = "break";
			Destroy(gb);
		}
		if (PhotonEngine.scene == "戈壁")
		{
			SceneManager.LoadSceneAsync(8, LoadSceneMode.Additive);
			PhotonEngine.scene = "break";
			Destroy(gb);
		}*/

	}
}
