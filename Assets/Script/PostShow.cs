using UnityEngine;

using UnityEngine.UI;

using System.Collections;

using System.Collections.Generic;
using JsonFx.Json;

//Contributed by Hoang Nguyen
public class PostShow : MonoBehaviour
{

    public string title;
    public string content;
    public string time;
    public int idpost;
    public int iduser;
    public Comment[] commentarray;
    Text contenttext;
    public string originaltext;
    // Use this for initialization
    void Start()
    {
        title = PlayerPrefs.GetString("title");
        content = PlayerPrefs.GetString("content");
        time = PlayerPrefs.GetString("time");
        idpost = PlayerPrefs.GetInt("idpost");
        iduser = PlayerPrefs.GetInt("thisuser");
        Debug.Log(title + content + time + idpost);



        GameObject title_field = GameObject.FindGameObjectWithTag("title_field");
        Text titletext = title_field.GetComponent<Text>();
        titletext.text = title;

        GameObject content_field = GameObject.FindGameObjectWithTag("content_field");
        contenttext = content_field.GetComponent<Text>();
        
        contenttext.text = content;
        originaltext = content;




        RequestComment();

    }





    public void RequestComment()
    {
        string url = "http://143.44.65.27:8080/RESTLawrenum/api/handlecomment/idpost?idpost=" + idpost;
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
            commentarray = JsonReader.Deserialize<Comment[]>(www.data);

            contenttext.text = originaltext;

            for (int i = 0; i < commentarray.Length; i++)
            {
                

                contenttext.text = contenttext.text + "\n" + "<c>"+commentarray[i].fullname + ": " + commentarray[i].content;



            }
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }


    public void createComment()
    {

        GameObject comment_field = GameObject.FindGameObjectWithTag("comment_field");
        InputField comment_input = comment_field.GetComponent<InputField>();
        Comment newcomment = new Comment(comment_input.text, "", 0, idpost, iduser, "");

        string json = "{\"content\":\"" + newcomment.content + "\",\"iduser\":\"" + newcomment.iduser + "\",\"idpost\":\"" + newcomment.idpost + "\"}";

        comment_input.text = "";
        Debug.Log(json);

        WWW www1;
        Dictionary<string, string> postHeader = new Dictionary<string, string>();
        postHeader.Add("Content-Type", "application/json");

        // convert json string to byte
        var formData = System.Text.Encoding.UTF8.GetBytes(json);


        string url = "http://143.44.65.27:8080/RESTLawrenum/api/comment";
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
            RequestComment();

        }
        else
        {
            Debug.Log("WWW Error: " + www1.error);
            //Display the error text
        }
    }





    void OnGUI()
    {



        // Update is called once per frame

    }
}
