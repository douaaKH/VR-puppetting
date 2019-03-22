using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class AnimationHandling : MonoBehaviour
{

    Animator anim;

    public float ikWeight = 1;

    // TARGETS
    public Transform leftIKTargetForHands;
    public Transform rightIKTargetForHands;
    public Transform leftIKTargetForFeet;
    public Transform rightIKTargetForFeet;
    public Transform chestTarget;
    public Transform forwardTransform;

    public Transform hintLeft;
    public Transform hintRight;

    public GameObject leapLeftHandGO;
    public GameObject leapRightHandGO;


    // LEAP'S HANDS
    private RigidHand leapLeftRigidHand;
    private RigidHand leapRightRigidHand;

    private Transform rightPinkyFinger;
    private Transform leftPinkyFinger;


    private Transform chest;
    private Transform leftEye;
    private Transform rightEye;
    private Transform spine; 

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        leapLeftRigidHand = leapLeftHandGO.GetComponent<RigidHand>();
        leapRightRigidHand = leapRightHandGO.GetComponent<RigidHand>();

        leftEye = anim.GetBoneTransform(HumanBodyBones.LeftEye).GetChild(0);
        rightEye = anim.GetBoneTransform(HumanBodyBones.RightEye).GetChild(0);
        chest = anim.GetBoneTransform(HumanBodyBones.Chest);
        spine = anim.GetBoneTransform(HumanBodyBones.Spine);

        //previousTargetPosition = leftIKTargetForHands.position;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("shouldJump", false);

        //displacementTargetPosition = leftIKTargetForHands.position - previousTargetPosition;

        //print("y = " + displacementTargetPosition.y);
        //chest.Translate(0, displacementTargetPosition.y * 10, 0, Space.World);

        // MANIPULATING EYES USING THUMB
        if(leapLeftRigidHand.IsTracked == leapRightRigidHand.IsTracked)
        {
            print("Both leap hands are tracked or not tracked!");
            leftEye.LookAt(forwardTransform);
            rightEye.LookAt(forwardTransform);
        } else if (!leapLeftRigidHand.IsTracked)
        {
            //leftEye.LookAt(leftIKTargetForHands);
            //rightEye.LookAt(leftIKTargetForHands);
            leftEye.LookAt(leapRightHandGO.transform.GetChild(0).GetChild(2));
            rightEye.LookAt(leapRightHandGO.transform.GetChild(0).GetChild(2));
        } else if (!leapRightRigidHand.IsTracked)
        {
            //leftEye.LookAt(rightIKTargetForHands);
            //rightEye.LookAt(rightIKTargetForHands);
            leftEye.LookAt(leapLeftHandGO.transform.GetChild(0).GetChild(2));
            rightEye.LookAt(leapLeftHandGO.transform.GetChild(0).GetChild(2));
        }

        //if (displacementTargetPosition.y < -0.2)
        //{
        //    print("He should crouch!");
        //    anim.SetBool("shouldJump", true);
        //}

        //if (displacementTargetPosition.y > 0.2)
        //{
        //    print("He should stand!");
        //    anim.SetBool("shouldJump", false);
        //}

        //previousTargetPosition = leftIKTargetForHands.position;

        // Updating local positions of leap middle fingers
        rightPinkyFinger = leapRightHandGO.transform.GetChild(3).GetChild(2);
        leftPinkyFinger = leapLeftHandGO.transform.GetChild(3).GetChild(2);
        
    }

    // IK
    private void OnAnimatorIK()
    {
        // MANIPULATING FEET USING PINKY
        //anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, ikWeight);
        //anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, ikWeight);

        //anim.SetIKPosition(AvatarIKGoal.LeftFoot, leftIKTargetForFeet.position + rightPinkyFinger.localPosition);
        //anim.SetIKPosition(AvatarIKGoal.RightFoot, rightIKTargetForFeet.position + leftPinkyFinger.localPosition);

        ////anim.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, ikWeight);
        ////anim.SetIKHintPositionWeight(AvatarIKHint.RightKnee, ikWeight);

        ////anim.SetIKHintPosition(AvatarIKHint.LeftKnee, hintLeft.position);
        ////anim.SetIKHintPosition(AvatarIKHint.RightKnee, hintRight.position);

        //anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, ikWeight);
        //anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, ikWeight);

        //anim.SetIKRotation(AvatarIKGoal.LeftFoot, rightPinkyFinger.localRotation);
        //anim.SetIKRotation(AvatarIKGoal.RightFoot, leftPinkyFinger.localRotation);


        // MANIPULATING HANDS USING INDEX
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, ikWeight);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, ikWeight);

        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftIKTargetForHands.position);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightIKTargetForHands.position);

        //anim.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, ikWeight);
        //anim.SetIKHintPositionWeight(AvatarIKHint.RightKnee, ikWeight);

        //anim.SetIKHintPosition(AvatarIKHint.LeftKnee, hintLeft.position);
        //anim.SetIKHintPosition(AvatarIKHint.RightKnee, hintRight.position);

        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);

        anim.SetIKRotation(AvatarIKGoal.LeftHand, Quaternion.Inverse(leftIKTargetForHands.rotation));
        anim.SetIKRotation(AvatarIKGoal.RightHand, Quaternion.Inverse(rightIKTargetForHands.rotation));


        // BODY LOOK AT CONTROL WITH RIGHT HAND PALM
        //anim.SetLookAtWeight(1, 1, 1, 1, 1);
        anim.SetLookAtWeight(0.1f, 1, 0.5f, 0, 0.2f);
        anim.SetLookAtPosition(leapRightHandGO.transform.GetChild(5).position);


    }
}
