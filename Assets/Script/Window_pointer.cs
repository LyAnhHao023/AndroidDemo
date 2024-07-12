using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_pointer : MonoBehaviour
{
    private Vector3 targetPos;
    [SerializeField]
    GameObject pointerGO;
    float borderSize = 30f;

    float movePointer=0;
    bool flag=false;

    public void SetTarget(Vector3 target)
    {
        targetPos = target;
    }

    private void Update()
    {        
        Vector3 targetPosScreenPoint = Camera.main.WorldToScreenPoint(targetPos);
        bool isOffScreen = targetPosScreenPoint.x <= borderSize || targetPosScreenPoint.x>=Screen.width- borderSize || targetPosScreenPoint.y<= borderSize || targetPosScreenPoint.y>=Screen.height- borderSize;

        if(isOffScreen)
        {
            RotaisionPointer();
            Vector3 cappedTargetScreenPos = targetPosScreenPoint;
            if(cappedTargetScreenPos.x <= borderSize) cappedTargetScreenPos.x = borderSize;
            if(cappedTargetScreenPos.x>=Screen.width- borderSize) cappedTargetScreenPos.x=Screen.width- borderSize;
            if (cappedTargetScreenPos.y <= borderSize) cappedTargetScreenPos.y = borderSize;
            if (cappedTargetScreenPos.y >= Screen.height - borderSize) cappedTargetScreenPos.y = Screen.height - borderSize;

            Vector3 pointerWorldPos= Camera.main.ScreenToWorldPoint(cappedTargetScreenPos);
            pointerGO.transform.position = pointerWorldPos;
        }
        else
        {
            pointerGO.transform.position= targetPos;
            pointerGO.transform.rotation = Quaternion.Euler(0,0, -90);
            if (movePointer >= 1f)
            {
                flag = true;
            }
            else if (movePointer <= 0f)
            {
                flag=false;
            }
            movePointer=flag? movePointer-0.01f:movePointer + 0.01f;
            pointerGO.transform.position = new Vector3(pointerGO.transform.position.x, targetPos.y + movePointer,0);
        }

    }

    private void RotaisionPointer()
    {
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0;
        Vector3 dir = (targetPos - fromPosition).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        pointerGO.transform.rotation = Quaternion.Euler(0, 0, angle);
    }


}
