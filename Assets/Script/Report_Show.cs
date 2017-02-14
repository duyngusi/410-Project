using UnityEngine;

using UnityEngine.UI;

using System.Collections;
using System.Net;
using System.Collections.Generic;
using JsonFx.Json;

public class Report_Show : MonoBehaviour {

    public string content;
    public int idtarget;
    Text contenttext;
    public int idreport;
    static public string deletereport_url = "143.44.65.27:8080/RESTLawrenum/api/report/";
    static public string acceptReport_url = "143.44.65.27:8080/RESTLawrenum/api/post/";
    // Use this for initialization
    void Start () {
       content =  PlayerPrefs.GetString("content");
       idtarget =  PlayerPrefs.GetInt("idtarget");
        idreport = PlayerPrefs.GetInt("idreport");
        GameObject content_field = GameObject.FindGameObjectWithTag("content_field");
        contenttext = content_field.GetComponent<Text>();
        contenttext.text = content;

    }


    public void accept()
    
        {

            string url = acceptReport_url + idtarget;
            //  string url2 = "user=" + username_input + "&password=" + password_input;
            // string url = url1 + url2;
            Debug.Log(url);
            WWW www = new WWW(url);
            StartCoroutine(WaitForRequestAccept(www));
        }

        IEnumerator WaitForRequestAccept(WWW www)
    {
            yield return www;

            // check for errors
            if (www.error == null)
            {

                Debug.Log("WWW Ok!: " + www.data);

            }
            else
            {
                Debug.Log("WWW Error: " + www.error);
            }
        }
    

    public void decline()
    {

        string url = deletereport_url + idreport;
        //  string url2 = "user=" + username_input + "&password=" + password_input;
        // string url = url1 + url2;
        Debug.Log(url);
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
           
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}

  

