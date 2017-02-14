 using UnityEngine;

using UnityEngine.UI;

using System.Collections;

using System.Collections.Generic;
using JsonFx.Json;
using UnityEngine.SceneManagement;
//Contributed by Hoang Nguyen and Yiming Li

public class FlowMaster : MonoBehaviour {

   
    public User userObjScript;
    public string stringToEdit;
    // Use this for initialization
    void Start () {
      
        string userID = PlayerPrefs.GetString("UserID");
        string username = PlayerPrefs.GetString("Username");
        string password = PlayerPrefs.GetString("Password");
        string fullname = PlayerPrefs.GetString("Fullname");
        string email = PlayerPrefs.GetString("Email");
        string graduation_year = PlayerPrefs.GetString("GradYear");
        string type = PlayerPrefs.GetString("Type");
     

      userObjScript = GameObject.FindGameObjectWithTag("user").GetComponent<User>();

        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;


        if (sceneName == "WelcomeScreen")
        { userObjScript.requestForum(); }

        if (sceneName == "ReportingAdminScreen")
        {
            userObjScript.requestAllReports();
        }


        //userObjScript = userObj.GetComponent<User>();





        //userObjScript.id =userID;
        //userObjScript.username = username;
        //userObjScript.password = password;
        //userObjScript.fullname = fullname;
        //userObjScript.email = email;
        //userObjScript.gradyear = graduation_year;
        //userObjScript.type = type;

        //Debug.Log( userObjScript.id) ;
        //Debug.Log(userObjScript.username);
        //Debug.Log(userObjScript.password);
        //Debug.Log(userObjScript.fullname);

    }
 
    // Use this for initialization


    void OnGUI()
    {
        //GUIStyle style = new GUIStyle("TextArea");

        //style.fontSize = 30;

        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "Post_Create")
        {
          
            GUI.skin.textArea.normal.background = Texture2D.whiteTexture;
            GUI.skin.textArea.active.background = Texture2D.whiteTexture;
            GUI.skin.textArea.focused.background = Texture2D.whiteTexture;
            GUI.skin.textArea.hover.background = Texture2D.whiteTexture;
            GUI.contentColor = Color.black;
            stringToEdit = GUI.TextArea(new Rect(230, 400, 500, 600), stringToEdit, 200);
            PlayerPrefs.SetString("content_field", stringToEdit);

        }

        if (sceneName == "ReportScreen")
        {
            GUI.skin.textArea.fontSize = 40;

            stringToEdit = GUI.TextArea(new Rect(0, 250, 750, 900), stringToEdit, 400);
        }


    }

    public void changeforum(int id)
    {



        if (id == 3)
        {
            Application.LoadLevel("AddSubForum");
        }
        else
        {
            userObjScript.ChangeForum(id);
        }
    }

    public void Search()
    {
        GameObject search_field = GameObject.FindGameObjectWithTag("search_field");
        InputField search_input = search_field.GetComponent<InputField>();
        userObjScript.search(search_input.text);

    }
    public void CreatePost()
    {

        userObjScript.createPost();
    }

    public void RequestPost()
    {

        
        userObjScript.RequestPost();
    }


    public void RequestSubForum()
    {
        GameObject name_field = GameObject.FindGameObjectWithTag("name_field");
        InputField name_input = name_field.GetComponent<InputField>();

        GameObject content_field = GameObject.FindGameObjectWithTag("content_field");
        InputField content_input = content_field.GetComponent<InputField>();

        userObjScript.requestsubforum(content_input.text, name_input.text);

    }

    public void ReportPost()
    {

        int idpost = PlayerPrefs.GetInt("idpost");
        userObjScript.ReportPost(stringToEdit, idpost);

    }
	// Update is called once per frame
	
}
