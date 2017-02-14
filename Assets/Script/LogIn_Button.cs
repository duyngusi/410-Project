using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Contributed by Hoang Nguyen

public class LogIn_Button : MonoBehaviour
{
    public GameObject user;
    private GameObject userobj;
    public void LogIn()
    {
       
       GameObject username_field = GameObject.FindGameObjectWithTag("username_field");
        InputField username_input = username_field.GetComponent<InputField>();
        
        GameObject password_field = GameObject.FindGameObjectWithTag("password_field");
        InputField password_input = password_field.GetComponent<InputField>();
        Debug.Log(username_input);
        Debug.Log(password_input);

        string url = "http://143.44.65.27:8080/RESTLawrenum/api/user?user="+ username_input.text +"&password=" + password_input.text;
     //  string url2 = "user=" + username_input + "&password=" + password_input;
     // string url = url1 + url2;

        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.data);
            string str = www.text;
         
            
                userobj = Instantiate(user, Vector3.zero, Quaternion.identity) as GameObject;
                userobj.tag = "user";
                User a = userobj.GetComponent<User>();
                JsonUtility.FromJsonOverwrite(www.data, a);

                Debug.Log("Day la id: " + a.iduser);
            PlayerPrefs.SetInt("thisuser", a.iduser);

                Debug.Log("Trash");
                if (a.iduser == 0)
                {
                    GameObject error_field = GameObject.FindGameObjectWithTag("error_field");
                    Text error_text = error_field.GetComponent<Text>();
                    error_text.text = "Wrong Password";
                    Destroy(userobj);
                }

                



                else
              {

                if (a.type != 2)
                { Application.LoadLevel("WelcomeScreen"); }
                else
                {
                    Application.LoadLevel("ReportingAdminScreen");
                }
            }
            }
        
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}