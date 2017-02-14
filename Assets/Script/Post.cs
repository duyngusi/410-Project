using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contributed by Yiming Li

public class Post  {
   public string content { get; set; }
    public string fullname { get; set; }
    public int idforum { get; set; }
    public int idpost { get; set; }
    public int iduser { get; set; }
    public string tag { get; set; }
    public string time { get; set; }
    public string title { get; set; }
   

    public Post()
    {
        fullname = "";
        content = "";
        idforum = 0;
        idpost = 0;
        iduser = 0;
        tag = "";
        time = "";
        title = "";
        // this is the default constructor that JSONFX wanted.
    }



    public Post(string s1, int s2, int s3, int s4, string s5,string s6, string s7)
    {
        Debug.Log(s1 + s2 + s3 + s4 + s5 + s6 + s7);
        content = s1;
        idforum = s2;
        idpost = s3;
        iduser = s4;
        tag = s5;
        time = s6;
        title = s7;
        // this is the default constructor that JSONFX wanted.
    }
    // Update is called once per frame

}
