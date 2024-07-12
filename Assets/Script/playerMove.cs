using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{

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
        if(isCanMove)
        {
            moveInput.x = Input.GetAxis("Horizontal");
            moveInput.y = Input.GetAxis("Vertical");
            transform.position += moveInput * player.characterStats.speed * Time.deltaTime;

            animation.SetFloat("Speed", moveInput.sqrMagnitude);


            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mousePos.x >transform.position.x)
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
