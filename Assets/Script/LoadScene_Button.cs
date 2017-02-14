using UnityEngine;
using System.Collections;
//Contributed by Hoang Nguyen

public class LoadScene_Button : MonoBehaviour {

	// Use this for initialization
	public void ChangeScene(string scenename)
	{
		Application.LoadLevel (scenename);
	}
}
