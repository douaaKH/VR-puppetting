using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class Puppet : MonoBehaviour {
    public GameObject Corps;
    Controller m_leapController;
    GameObject weird;
    GameObject LeftPalm;
    GameObject RightPalm;

    // Use this for initialization
    void Start () {
        m_leapController = new Controller();
        weird = GameObject.FindGameObjectWithTag("Weird");
    }

    void weirdWays()
    {
        RightPalm = GameObject.FindGameObjectWithTag("Right palm");
        LeftPalm = GameObject.FindGameObjectWithTag("Left palm");

        Corps.GetComponent<Transform>().position = LeftPalm.GetComponent<Transform>().position;
        Corps.GetComponent<Transform>().rotation = LeftPalm.GetComponent<Transform>().rotation;
        Debug.Log(LeftPalm.GetComponent<Transform>().position.x);

       


    }
    
    // Update is called once per frame
    void Update () {
        weirdWays();
    }
}


/*
        if (frame.Hands.Count == 2)
        {
            Hand leftHand = frame.Hands[0];
            Hand rightHand = frame.Hands[1];

            if (leftHand.PalmPosition.x > rightHand.PalmPosition.x)
            {
                leftHand = rightHand;
                rightHand = frame.Hands[0];
            }
            */