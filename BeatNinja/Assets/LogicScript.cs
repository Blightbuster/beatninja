using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicScript : MonoBehaviour
{
    public GameObject obj_lefthand;
    public GameObject obj_leftarea;
    public GameObject obj_righthand;
    public GameObject obj_rightarea;


    // Start is called before the first frame update
    private void Start()
    {

        obj_leftarea = GameObject.Find("Left");
        obj_lefthand = GameObject.Find("Hand_L");

        obj_rightarea = GameObject.Find("Right");
        obj_righthand = GameObject.Find("Hand_R");


    }
    // Update is called once per frame
    void Update()
    {

        if (nearToCenterL() == true)
        {
            //GameObject..GetComponent<SliceArea>().Slice();
            //Debug.Log("Near!");

            GameManager.Instance.LeftSlice();

        }

        if (nearToCenterR() == true)
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

    /* public bool isStill() {
         //get the velocity of the current frame
         Vector3 velocity = rb.velocity;

         // determine if the object is in motion by assessing the magnitude of its velocity.
         float speed = velocity.magnitude;

         if (speed == 0 ) {
             return true;
         }
         else { return false; }
     }*/
}
