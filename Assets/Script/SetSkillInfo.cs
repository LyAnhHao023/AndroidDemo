using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSkillInfo : MonoBehaviour
{
    [SerializeField] Image skillIcon;
    [SerializeField] Text skillName;
    [SerializeField] Text skillDescription;

    [SerializeField] Animator skillVideo;

    string lastVideo;

    public void Set(SkillInfo skillInfo)
    {
        skillIcon.sprite = skillInfo.Icon;
        skillName.text = skillInfo.name.ToUpper();
        skillDescription.text = skillInfo.decription;
    }

    public void SetSkillVideo(CharacterData characterData)
    {
        if(lastVideo != null)
            skillVideo.SetBool(lastVideo, false);
        lastVideo = characterData.name;
        skillVideo.SetBool(characterData.name, true);
    }
}
