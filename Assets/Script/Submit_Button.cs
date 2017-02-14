using UnityEngine;

using UnityEngine.UI;

using System.Collections;

using System.Collections.Generic;

//Contributed by Hoang Nguyen

public class Submit_Button : MonoBehaviour {
    public GameObject user;
    private GameObject userobj;
    // Use this for initialization
    public void SubmitRegistration()
	{
		GameObject username_field = GameObject.FindGameObjectWithTag ("username_field");
		InputField username_input= username_field.GetComponent<InputField> ();

        GameObject password_field = GameObject.FindGameObjectWithTag("password_field");
        InputField password_input = password_field.GetComponent<InputField>();



        GameObject email_field = GameObject.FindGameObjectWithTag("email_field");
        InputField email_input = email_field.GetComponent<InputField>();


        GameObject graduationyear_field = GameObject.FindGameObjectWithTag ("graduation_year_field");
		InputField graduationyear_input = graduationyear_field.GetComponent<InputField> ();

		GameObject fullname_field = GameObject.FindGameObjectWithTag ("fullname_field");
		InputField fullname_input= fullname_field.GetComponent<InputField> ();

	









		Debug.Log (username_input.text);
		Debug.Log (graduationyear_input.text);
		Debug.Log (fullname_input.text);
		Debug.Log (password_input.text);
		Debug.Log (email_input.text);

        
        WWW www;
        Dictionary<string, string> postHeader = new Dictionary<string, string>();
        postHeader.Add("Content-Type", "application/json");

        Debug.Log("Alo" + graduationyear_input.text);


      string jsonStr = "{\"username\":\"" + username_input.text + "\",\"password\":\"" + password_input.text + "\",\"fullname\":\"" + fullname_input.text + "\",\"email\":\"" + email_input.text + "\",\"graduation_year\":\"" + int.Parse(graduationyear_input.text) + "\"}";
   

        // convert json string to byte
        var formData = System.Text.Encoding.UTF8.GetBytes(jsonStr);


        string url = "http://143.44.65.27:8080/RESTLawrenum/api/user";
        www = new WWW(url, formData, postHeader);
        StartCoroutine(WaitForRequest(www));
   



		
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;

        //check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.data);
            string str = www.text;
            if (str.Equals("0"))
            {
                Debug.Log("Got here");
                GameObject error_field = GameObject.FindGameObjectWithTag("error_field");
                Text error_text = error_field.GetComponent<Text>();
                error_text.text = "Wrong username or password";
            }
            else
            {
                PlayerPrefs.SetString("UserID", str);


                userobj = Instantiate(user, Vector3.zero, Quaternion.identity) as GameObject;
                userobj.tag = "user";
                User a = userobj.GetComponent<User>();
                JsonUtility.FromJsonOverwrite(www.data, a);

                Debug.Log("Day la id: " + a.iduser);
                PlayerPrefs.SetInt("thisuser", a.iduser);
                Application.LoadLevel("WelcomeScreen");
            }
        }
        else
			{
			Debug.Log ("WWW Error: " + www.error);
				//Display the error text
			}
	}
}
