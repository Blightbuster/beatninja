using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinectController : MonoBehaviour
{
    public Rigidbody LeftHand;
    public SliceArea LeftSliceArea;

    public Rigidbody RightHand;
    public SliceArea RightSliceArea;

    private bool wasInLeftArea = false;
    private bool wasInRightArea = false;

    private const float activationDistance = 1.0f;

    void Update()
    {
        var isInLeftArea = LeftSliceArea.GetDistanceToArea(LeftHand.position) < activationDistance;
        if (isInLeftArea && !wasInLeftArea)
        {
            GameManager.Instance.LeftSlice();
            wasInLeftArea = true;
        }
        wasInLeftArea = isInLeftArea;

        var isInRightArea = RightSliceArea.GetDistanceToArea(RightHand.position) < activationDistance;
        if (isInRightArea && !wasInRightArea)
        {
            GameManager.Instance.RightSlice();
            wasInRightArea = true;
        }
        wasInRightArea = isInRightArea;
    }
}

