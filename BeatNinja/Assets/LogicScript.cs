using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicScript : MonoBehaviour
{
    public GameObject obj_lefthand;
    public GameObject obj_leftarea;
    public GameObject obj_righthand;
    public GameObject obj_rightarea;
    
    private int frameCount = 0; // frame counter
    private Vector3 lLastPosition; // position of last time(left hand)
    private Vector3 rLastPosition; // position of last time(right hand)
    public int recordInterval = 30; // frame interval is 30
    public Vector3 lPositionDifference;
    public Vector3 rPositionDifference;



    // Start is called before the first frame update
    private void Start()
    {

        obj_leftarea = GameObject.Find("Left");
        obj_lefthand = GameObject.Find("Hand_L");

        obj_rightarea = GameObject.Find("Right");
        obj_righthand = GameObject.Find("Hand_R");

        lLastPosition = obj_lefthand.transform.position;//initial position of left hand
        Debug.Log("last position is " + lLastPosition);//test
        rLastPosition = obj_righthand.transform.position;//initial position of right hand
        Debug.Log("last position is " + rLastPosition);//test

    }
    // Update is called once per frame
    void Update()
    {
        frameCount++;
        if (frameCount >= recordInterval)
        {
            // record position
            lRecordObjectPosition();
            rRecordObjectPosition();
            // reset frame count
            frameCount = 0;
        }


        if (nearToCenterL() == true && System.Math.Abs(lPositionDifference.x) >= 0.2f && System.Math.Abs(lPositionDifference.y) >= 0.2f && System.Math.Abs(lPositionDifference.z) >= 0.2f)
        {
            //GameObject..GetComponent<SliceArea>().Slice();
            //Debug.Log("Near!");

            GameManager.Instance.LeftSlice();

        }

        if (nearToCenterR() == true && System.Math.Abs(rPositionDifference.x) >= 0.2f && System.Math.Abs(rPositionDifference.y) >= 0.2f && System.Math.Abs(rPositionDifference.z) >= 0.2f)
        {
            //GameObject..GetComponent<SliceArea>().Slice();
            //Debug.Log("Near!");

            GameManager.Instance.RightSlice();

        }



    }

    public bool nearToCenterL()
    {

        Vector3 worldPos_lhand = obj_lefthand.transform.position;
        //Debug.Log("the position of left hand is :"+ worldPos_lhand);
        Vector3 worldPos_larea = obj_leftarea.transform.position;
        //Debug.Log("the position of left area is :" + worldPos_larea);
        if (System.Math.Abs(worldPos_lhand.y - worldPos_larea.y) < 1 && System.Math.Abs(worldPos_lhand.z - worldPos_larea.z) < 1)
        {
            return true;
        }
        else { return false; }

    }

    public bool nearToCenterR()
    {

        Vector3 worldPos_rhand = obj_righthand.transform.position;
        //Debug.Log("the position of right hand is :"+ worldPos_lhand);
        Vector3 worldPos_rarea = obj_rightarea.transform.position;
        //Debug.Log("the position of right area is :" + worldPos_larea);
        if (System.Math.Abs(worldPos_rhand.y - worldPos_rarea.y) < 1 && System.Math.Abs(worldPos_rhand.z - worldPos_rarea.z) < 1)
        {
            return true;
        }
        else { return false; }

    }


    public void lRecordObjectPosition()
    {
        // record currend position
        Vector3 currentPosition = obj_lefthand.transform.position;

        // 计算与上一次记录的坐标的差值
        lPositionDifference = currentPosition - lLastPosition;

        //  positionDifference
        Debug.Log("Position Difference of left hand: " + lPositionDifference);

        // update position
        lLastPosition = currentPosition;
    }

    public void rRecordObjectPosition()
    {
        // record currend position
        Vector3 currentPosition = obj_righthand.transform.position;

        // 计算与上一次记录的坐标的差值
        rPositionDifference = currentPosition - rLastPosition;

        //  positionDifference
        Debug.Log("Position Difference of right hand: " + rPositionDifference);

        // update position
        rLastPosition = currentPosition;
    }
}

