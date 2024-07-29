using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{

    [SerializeField] FixedJoystick joystick;

    private Animator animation;

    //Ẩn đi phần chỉnh sửa trên Inspector
    [HideInInspector]
    public Vector3 moveInput;

    [HideInInspector]
    public float scaleX;

    CharacterInfo_1 player;

    public bool isCanMove=true;

    private void Start()
    {
        player=GetComponent<CharacterInfo_1>();
        animation = player.characterAnimate.GetComponent<Animator>();
    }

    private void Update()
    {

        if(isCanMove && Time.timeScale > 0)
        {
            moveInput.x =joystick.Horizontal;
            moveInput.y = joystick.Vertical;
            transform.position += moveInput * player.characterStats.speed * Time.deltaTime;

            animation.SetFloat("Speed", moveInput.sqrMagnitude);

            if (moveInput.x != 0)
            {
                if (moveInput.x > 0)
                {
                    animation.transform.rotation = Quaternion.Euler(0, 0, 0);
                    scaleX = 1;
                }
                else
                {
                    animation.transform.rotation = Quaternion.Euler(0, 180, 0);
                    scaleX = -1;
                }
            }
        }
    }
}
