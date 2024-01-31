using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool IsLatencyTest;

    private void Update()
    {
        var left = Input.GetKeyDown(KeyCode.LeftArrow);
        var right = Input.GetKeyDown(KeyCode.RightArrow);

        if (!IsLatencyTest)
        {
            if (left) GameManager.Instance.LeftSlice();
            if (right) GameManager.Instance.RightSlice();
        }
        else
        {
            if (left) GameManagerLatencyTest.Instance.LeftSlice();
            if (right) GameManagerLatencyTest.Instance.RightSlice();
        }
    }
}
