using UnityEngine;

using UnityEngine.UI;

using System.Collections;

using System.Collections.Generic;
using JsonFx.Json;
using UnityEngine.SceneManagement;

//Contributed by Hoang Nguyen and Yiming Li

public class User: MonoBehaviour
{

    public int iduser;
    public string username;
    public string password;
    public int type;
    public string fullname;
    public string email;
    public int graduation_year;
    public int curForum;
   public Forum[] forumarray;
    public Post[] postarray;
   static public string allforum_url = "http://143.44.65.27:8080/RESTLawrenum/api/forum";
   static public string requestpost_url = "http://143.44.65.27:8080/RESTLawrenum/api/handlepost/idforum?idforum=";
   static public string createpost_url = "http://143.44.65.27:8080/RESTLawrenum/api/post";
    static public string reportpost_url = "http://143.44.65.27:8080/RESTLawrenum/api/report";
    static public string searchtag_url =  "http://143.44.65.27:8080/RESTLawrenum/api/handlepost/tag?tag=";
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    public User(int a1, string a2, string a3, int a4, string a5, string a6, int a7)
    {
        iduser = a1;
        fullname = a5;
        username = a2;
        password = a3;
        email = a6;
        graduation_year = a7;
        type = a4;
        curForum = 0;
      
    }

    public void requestForum()
    {
       
        WWW www = new WWW(allforum_url);
        StartCoroutine(WaitForRequest2(www));
    }
    IEnumerator WaitForRequest2(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.data);
          forumarray = JsonReader.Deserialize<Forum[]>(www.data);


            List<string> names = new List<string>();
            foreach (var row in forumarray)
            { names.Add(row.name);
            
            }
            names.Add("Add");


            GameObject dropdown = GameObject.FindGameObjectWithTag("drop_down_forum");
            Dropdown dropdown_input = dropdown.GetComponent<Dropdown>();
            dropdown_input.AddOptions(names);
            if (curForum == 0)
            { ChangeForum(0); }
            else
            {
               for(int i = 0; i < forumarray.Length; i ++)
                { if (curForum == forumarray[i].idforum)
                    {
                        dropdown_input.value = i;
                        ChangeForum(i);
                    }
                  
                }

            }
          //  dropdown_input.AddOptions();


        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

    public void search(string tag)
    {
        string url = searchtag_url + tag;
        //  string url2 = "user=" + username_input + "&password=" + password_input;
        // string url = url1 + url2;

        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
    }

    public void ChangeForum(int id)
    {
        curForum = forumarray[id].idforum;

        Debug.Log(curForum);
        RequestPost();

    }



    public void requestsubforum(string content, string name)
    {
        
        PrivateRequestClass newsfrequest = new PrivateRequestClass();
        newsfrequest.content = content;
        newsfrequest.title = name;
        newsfrequest.iduser = iduser;
        newsfrequest.request_type = 1;


        string json = "{\"content\":\"" + newsfrequest.content + "\",\"iduser\":\"" + newsfrequest.iduser + "\",\"title\":\"" + newsfrequest.title  + "\",\"request_type\":\"" + 1 + "\"}";
        WWW www1;
        Dictionary<string, string> postHeader = new Dictionary<string, string>();
        postHeader.Add("Content-Type", "application/json");

        // convert json string to byte
        var formData = System.Text.Encoding.UTF8.GetBytes(json);


        string url = "http://143.44.65.27:8080/RESTLawrenum/api/request";
        www1 = new WWW(url, formData, postHeader);
        StartCoroutine(WaitForRequest1(www1));
    }




    public void ReportPost(string content, int idpost)
    {

        Debug.Log("idpost: " + idpost);

        Report newpost = new Report();
        newpost.content = content;
        newpost.idpost = idpost;
        newpost.iduser = iduser;



        string json = "{\"content\":\"" + newpost.content + "\",\"iduser\":\"" + newpost.iduser + "\",\"idtarget\":\"" + newpost.idpost +  "\"}";
        WWW www1;
        Dictionary<string, string> postHeader = new Dictionary<string, string>();
        postHeader.Add("Content-Type", "application/json");

        // convert json string to byte
        var formData = System.Text.Encoding.UTF8.GetBytes(json);


        string url = reportpost_url;
        www1 = new WWW(url, formData, postHeader);
        StartCoroutine(WaitForRequest7(www1));
    }

    IEnumerator WaitForRequest7(WWW www1)
    {
        yield return www1;

        //check for errors
        if (www1.error == null)
        {
            Debug.Log("WWW Ok!: " + www1.data);
            Application.LoadLevel("WelcomeScreen");
        }
        else
        {
            Debug.Log("WWW Error: " + www1.error);
            //Display the error text
        }
    }



    public void RequestPost()
    {
        string url = requestpost_url + curForum;
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


        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;



        if (sceneName == "WelcomeScreen")
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

                    GUI.Label(new Rect(500, 260 + 80 * i, 300, 70), postarray[i].fullname, customButtoni);
                    if (GUI.Button(new Rect(100, 260 + 80 * i, 300, 70), postarray[i].title, customButtoni))
                    {
                        PlayerPrefs.SetString("title", postarray[i].title);
                        PlayerPrefs.SetString("content", postarray[i].content);
                        PlayerPrefs.SetInt("idpost", postarray[i].idpost);
                        PlayerPrefs.SetString("time", postarray[i].time);


                        Application.LoadLevel("PostScreen");
                   

                    }



                }
            }
        }
    }

    public void createPost()
    {
        GameObject title_field = GameObject.FindGameObjectWithTag("title_field");
        InputField title_input = title_field.GetComponent<InputField>();
       string content_input = PlayerPrefs.GetString("content_field");
        GameObject tag_field = GameObject.FindGameObjectWithTag("tag_field");
        InputField tag_input = tag_field.GetComponent<InputField>();


        Post newpost = new Post(content_input, 1, 0, iduser,tag_input.text, "", title_input.text);


        string json = "{\"content\":\"" + newpost.content + "\",\"idforum\":\"" + curForum + "\",\"iduser\":\"" + newpost.iduser + "\",\"tag\":\"" + newpost.tag + "\",\"title\":\"" + newpost.title + "\"}";

        Debug.Log(json);

        WWW www1;
        Dictionary<string, string> postHeader = new Dictionary<string, string>();
        postHeader.Add("Content-Type", "application/json");
        
        // convert json string to byte
        var formData = System.Text.Encoding.UTF8.GetBytes(json);


        string url = createpost_url;
        www1 = new WWW(url, formData, postHeader);
        StartCoroutine(WaitForRequest1(www1));





    }

    IEnumerator WaitForRequest1(WWW www1)
    {
        yield return www1;

        //check for errors
        if (www1.error == null)
        {
            Debug.Log("WWW Ok!: " + www1.data);
            Application.LoadLevel("WelcomeScreen");
        }
        else
        {
            Debug.Log("WWW Error: " + www1.error);
            //Display the error text
        }
    }


}




