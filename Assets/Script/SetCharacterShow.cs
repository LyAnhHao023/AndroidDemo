using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCharacterShow : MonoBehaviour
{
    [SerializeField] GameObject CharacterHolder;
    [SerializeField] GameObject AcceptButton;
    [SerializeField] Text Name;
    [SerializeField] SpriteToUIImage spriteToUIImage;
    [SerializeField] Text HP;
    [SerializeField] Text CRT;
    [SerializeField] Text ATK;
    [SerializeField] Text SPD;

    [SerializeField] GameObject skillHolder;
    [SerializeField] SetSkillInfo skillInfo;

    float timer = 5;

    bool stand;

    private CharacterData CharData;

    private void Update()
    {
        if (CharData != null)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = Random.Range(3, 6);
                stand = !stand;
                spriteToUIImage.SetAnimation(stand);
            }
        }
    }

    public void SetCharacter(CharacterData charData)
    {
        CharacterHolder.SetActive(true);
        AcceptButton.GetComponentInParent<Button>().enabled = charData.acquired;
        CharData = charData;
        Name.text = charData.name;
        HP.text = charData.stats.maxHealth.ToString();
        CRT.text = string.Format("{0}%", charData.stats.crit);
        ATK.text = charData.stats.strenght.ToString();
        SPD.text = charData.stats.speed.ToString();
        spriteToUIImage.SetCharacterAnimation(charData.animatorPrefab.GetComponent<SpriteRenderer>());
        stand = true;
        skillHolder.SetActive(true);
        skillInfo.Set(charData.skillInfo);
        skillInfo.SetSkillVideo(charData);
    }
}
