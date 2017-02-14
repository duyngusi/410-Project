using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JsonFx.Json;

//Contributed by Hoang Nguyen and Yiming Li

public class Request_Post : MonoBehaviour {

    Post[] postarray;
    [System.Serializable]
    public class PostsCollection
    {
        public Post[] posts;
    }

    // Use this for initialization
    void Start () {


        string url = "http://143.44.65.27:8080/RESTLawrenum/api/post/idforum?idforum=1";
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
            postarray = JsonReader.Deserialize<Post[]>(www.data);

       

            foreach (var row in postarray)
                Debug.Log(row.tag + row.idforum + row.content);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }



    void OnGUI()
    {



        if (postarray != null)


        {
            for (int i = 0; i < postarray.Length; i++)
            {
                GUIStyle customButtoni = new GUIStyle("Label");
                customButtoni.fontSize = 35;
                customButtoni.font = (Font)Resources.Load("Fonts/OpenSans");
                customButtoni.normal.textColor = Color.black;
                customButtoni.wordWrap = true;
                if (GUI.Button(new Rect(100, 260 + 80 * i, 300, 70), postarray[i].title, customButtoni))
                {
                    PlayerPrefs.SetString("title", postarray[i].title);
                    PlayerPrefs.SetString("content", postarray[i].content);
                    PlayerPrefs.SetString("time", postarray[i].time);
                    




                    Application.LoadLevel("PostScreen");
                }


            }
        }

    }

  
}
