using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class Puppet : MonoBehaviour {
    public GameObject Corps;
    Controller m_leapController;
    // Use this for initialization
    void Start () {
        m_leapController = new Controller();
    }

    void weirdWays()
    {
        Frame frame = m_leapController.Frame();

        if (frame.Hands.Count == 2)
        {
            Hand leftHand = frame.Hands[0];
            Hand rightHand = frame.Hands[1];

            if (leftHand.PalmPosition.x > rightHand.PalmPosition.x)
            {
                leftHand = rightHand;
                rightHand = frame.Hands[0];
                leftHand = frame.Hands[1];
            }
        }
        if (frame.Hands.Count == 1)
        {
            Hand leftHand = frame.Hands[0];
            Hand rightHand = frame.Hands[0];
        }
            if (frame.Hands.Count != 0)
        {

            Vector3 Corpspos;
            Corpspos.x = frame.Hands[0].PalmPosition.x / 100;
            Corpspos.y = frame.Hands[0].PalmPosition.y / 100;
            Corpspos.z = -frame.Hands[0].PalmPosition.z / 100;
            Corps.transform.position = Corpspos;
            Vector3 Corpsrot;
            Corpsrot.x = frame.Hands[0].PalmNormal.x;
            Corpsrot.y = frame.Hands[0].PalmNormal.y ;
            Corpsrot.z = -frame.Hands[0].PalmNormal.z ;
            Corps.transform.position = Corpsrot;



        }
        

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