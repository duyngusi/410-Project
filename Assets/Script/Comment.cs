using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contributed by Hoang Nguyen and Yiming Li

public class Comment {

    public string content { get; set; }
    public string fullname { get; set; }
    public int idcomment { get; set; }
    public int idpost { get; set; }
    public int iduser { get; set; }
    public string time { get; set; }

    public Comment()
    {
        fullname = "";
        content = "";
       
        idpost = 0;
        iduser = 0;
        time = "";
        idcomment = 0;
        // this is the default constructor that JSONFX wanted.
    }


    public Comment(string s1, string s2, int s3, int s4, int s5, string s6)
    {
        Debug.Log(s1 + s2 + s3 + s4 + s5 + s6 );
        content = s1;
        fullname = s2;
        idcomment = s3;
        idpost = s4;
        iduser = s5;
        time = s6;
        
        // this is the default constructor that JSONFX wanted.
    }

    // Update is called once per frame

}
