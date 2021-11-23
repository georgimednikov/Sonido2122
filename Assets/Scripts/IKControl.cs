using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class IKControl : MonoBehaviour
{

    protected Animator animator;

    public bool ikActive = false;
    public Transform rightHandObj = null;
    public Transform lookObj = null;
    public Transform leftHip, rightHip;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {

                // Set the look target position, if one has been assigned
                if (lookObj != null)
                {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }

                // Set the right hand target position and rotation, if one has been assigned
                if (rightHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                }

                if(rightHip != null) 
                {
                    RaycastHit hit; 
                    if(Physics.Raycast(rightHip.position, -transform.up, out hit, 3)) 
                    {
                        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
                        animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + transform.up * 0.1f);
                    }
                }

                if (leftHip != null)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(leftHip.position, -transform.up, out hit, 3))
                    {
                        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
                        animator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + transform.up * 0.1f);
                    }
                }
            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }
}