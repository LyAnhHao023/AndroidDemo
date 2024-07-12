using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
    public bool isUseSkill=false;

    public void OnClick()
    {
        isUseSkill = true;
    }
}
