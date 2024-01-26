using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicScript : MonoBehaviour
{
    public Rigidbody obj_lefthand;
    public GameObject obj_leftarea;
    public Rigidbody obj_righthand;
    public GameObject obj_rightarea;

    private bool wasInLeftArea = false;
    private bool wasInRightArea = false;

    // Start is called before the first frame update
    private void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (nearToCenter(obj_leftarea, obj_lefthand.transform) &&
            obj_lefthand.velocity.magnitude > 0.2
            && !wasInLeftArea)
        {
            GameManager.Instance.LeftSlice();
            wasInLeftArea = true;
        }
        else wasInLeftArea = false;

        if (nearToCenter(obj_rightarea, obj_righthand.transform) &&
            obj_righthand.velocity.magnitude > 0.2
            && !wasInRightArea)
        {
            GameManager.Instance.RightSlice();
            wasInRightArea = true;
        }
        else wasInRightArea = false;
    }

    public bool nearToCenter(GameObject area, Transform hand)
    {

        Vector3 posHand = hand.transform.position;
        //Debug.Log("the position of left hand is :"+ posHand);
        Vector3 posArea = area.transform.position;
        //Debug.Log("the position of left area is :" + posArea);
        return System.Math.Abs(posHand.y - posArea.y) < 1 && System.Math.Abs(posHand.x - posArea.x) < 1;
    }
}

