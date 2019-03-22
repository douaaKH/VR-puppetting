using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class PlayWithPuppet : MonoBehaviour {

   public
    GameObject puppet;
    GameObject puppetRightArm;
    GameObject puppetLeftArm;
    GameObject puppetLowerRightArm;
    GameObject puppetLowerLeftArm;
    GameObject puppetRightHand;
    GameObject puppetLeftHand;
    Quaternion originalPuppetRotationValue;
    Quaternion originalPuppetLeftArmRotationValue;
    Quaternion originalPuppetLowerLeftArmRotationValue;

    GameObject right_hand_palm;
    GameObject left_hand_palm;
    GameObject right_hand_index;
    GameObject left_hand_index;
    RigidHand rightRigidHand;
    RigidHand leftRigidHand;
    Vector3 previousRightPalmPosition;
    Vector3 previousLeftPalmPosition;
    Vector3 previousRightIndexPosition;
    Vector3 previousLeftIndexPosition;

    float rotationResetSpeed;
    
    // Use this for initialization
    void Start () {
        // PUPPET
        puppet = GameObject.Find("Skel");
        // puppet's upper arms
        puppetRightArm = GameObject.Find("rightArm1_LoResUpperArm");
        puppetLeftArm = GameObject.Find("leftArm1_LoResUpperArm");
        // puppet's lower arms
        puppetLowerRightArm = GameObject.Find("rightArm1_LoResLowerArm");
        puppetLowerLeftArm = GameObject.Find("leftArm1_LoResLowerArm");
        // puppet's hands
        puppetRightHand = GameObject.Find("rightArm1_LoResHand");
        puppetLeftHand = GameObject.Find("leftArm1_LoResHand");

        // HANDS
        rightRigidHand = GameObject.Find("/Leap/HandModels/RigidRoundHand_R").GetComponent<RigidHand>();
        leftRigidHand = GameObject.Find("/Leap/HandModels/RigidRoundHand_L").GetComponent<RigidHand>();
        // palms
        right_hand_palm = GameObject.Find("/Leap/HandModels/RigidRoundHand_R/palm");
        left_hand_palm = GameObject.Find("/Leap/HandModels/RigidRoundHand_L/palm");
        // fingers
        right_hand_index = GameObject.Find("/Leap/HandModels/RigidRoundHand_R/index");
        left_hand_index = GameObject.Find("/Leap/HandModels/RigidRoundHand_L/index");

        // saving previous positions/rotations of
        // HANDS
        previousRightPalmPosition = right_hand_palm.transform.position;
        previousLeftPalmPosition = left_hand_palm.transform.position;
        previousRightIndexPosition = right_hand_index.transform.position;
        previousLeftIndexPosition = left_hand_index.transform.position;
        // PUPPET
        //originalPuppetRotationValue = puppet.transform.rotation; // save the initial rotation of the puppet
        originalPuppetLeftArmRotationValue = puppetLeftArm.transform.rotation; // save the initial rotation of the puppet
        originalPuppetLowerLeftArmRotationValue = puppetLowerLeftArm.transform.rotation; // save the initial rotation of the puppet
        
        rotationResetSpeed = 50.0f;
    }

    // Update is called once per frame
    void Update () {

        //puppetRightArm.transform.position += (rightRigidHand.transform.position - previousRightPalmPosition);
        //puppetLeftArm.transform.position += (rightRigidHand.transform.position - previousLeftPalmPosition);
        ////puppetLeftArm.transform.position += new Vector3(0,0.001f,0);
        ////puppetLeftArm.transform.position += new Vector3(0, 0.001f, 0);
        //previousRightPalmPosition = rightRigidHand.transform.position;
        //previousLeftPalmPosition = leftRigidHand.transform.position;

        //Vector3 targetRightDir = puppetLowerLeftArm.transform.position - right_hand_palm.transform.position;

        //// The step size is equal to speed times frame time.
        float step = rotationResetSpeed * Time.deltaTime;

        //Vector3 newDir = Vector3.RotateTowards(puppetLeftArm.transform.up, targetRightDir, step, 0.0f);
        //Debug.DrawRay(transform.position, newDir, Color.red);

        //// Move our position a step closer to the target.
        //puppetLeftArm.transform.rotation = Quaternion.LookRotation(newDir);

        //puppetLeftArm.transform.Rotate(new Vector3(right_hand_palm.transform.position.y, right_hand_palm.transform.position.x, 0) * Time.deltaTime * speed);
        //puppetLeftArm.transform.Rotate((right_hand_palm.transform.position - previousRightPalmPosition) * Time.deltaTime * speed);
        //previousRightPalmPosition = right_hand_palm.transform.position;

        //Vector3 newDir = Vector3.RotateTowards(puppetLowerLeftArm.transform.forward, targetRightDir, step, 0.0f);
        //puppetLowerLeftArm.transform.rotation = Quaternion.LookRotation(newDir);

        // checking if hands are detected
        if (rightRigidHand.IsTracked)
        {

            // ************ MANIPULATING ARMS WITH PALM ************ 
            // manipulating left arm with right hand
            Vector3 upperArmToHandDirectionL = puppetLeftHand.transform.position - puppetLeftArm.transform.position;
            //Vector3 upperArmToTargetHandDirectionL = right_hand_palm.transform.position - puppetLeftArm.transform.position;
            Vector3 upperArmToTargetHandDirectionL = upperArmToHandDirectionL + right_hand_palm.transform.position - previousRightPalmPosition;
            Quaternion qL = Quaternion.FromToRotation(upperArmToHandDirectionL, upperArmToTargetHandDirectionL);
            puppetLeftArm.transform.rotation = qL * puppetLeftArm.transform.rotation;
            previousRightPalmPosition = right_hand_palm.transform.position;
            //puppetLeftArm.transform.rotation = Quaternion.FromToRotation(Vector3.right, v);

            // manipulating right arm with left hand
            Vector3 upperArmToHandDirectionR = puppetRightHand.transform.position - puppetRightArm.transform.position;
            Vector3 upperArmToTargetHandDirectionR = left_hand_palm.transform.position - puppetRightArm.transform.position;
            Quaternion qR = Quaternion.FromToRotation(upperArmToHandDirectionR, upperArmToTargetHandDirectionR);
            //puppetRightArm.transform.rotation = qR * puppetRightArm.transform.rotation;

            // ************ MANIPULATING LOWER ARMS WITH INDEX ************ 
            Vector3 lowerArmToHandDirectionL = puppetLeftHand.transform.position - puppetLowerLeftArm.transform.position;
            Vector3 lowerArmToTargetDirectionL = lowerArmToHandDirectionL + right_hand_index.transform.position - previousRightIndexPosition;
            Quaternion qLL = Quaternion.FromToRotation(upperArmToHandDirectionL, upperArmToTargetHandDirectionL);
            puppetLowerLeftArm.transform.rotation = qL * puppetLowerLeftArm.transform.rotation;
            previousRightIndexPosition = right_hand_index.transform.position;

            print("right hand is tracked!");

            // ************ MOVING PUPPET UPWARD ************ 
            // TODO
        } else
        {
            puppetLeftArm.transform.rotation = Quaternion.Slerp(puppetLeftHand.transform.rotation, originalPuppetLeftArmRotationValue, step);
            puppetLowerLeftArm.transform.rotation = Quaternion.Slerp(puppetLeftHand.transform.rotation, originalPuppetLowerLeftArmRotationValue, step);
            print("right hand is not tracked!");

        }


    }
}
