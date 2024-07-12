using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    [SerializeField] Image CharacterImage;
    [SerializeField] GameObject CharacterLocker;

    GameObject CharacterHolder;
    SetCharacterShow setCharacterShow;

    private CharacterData CharData;

    private void Awake()
    {
        CharacterHolder = GameObject.FindGameObjectWithTag("CharacterHolder");
        setCharacterShow = CharacterHolder.GetComponent<SetCharacterShow>();
    }

    public void Set(CharacterData characterData)
    {
        CharData = characterData;
        CharacterImage.sprite = characterData.image;
        CharacterLocker.SetActive(!characterData.acquired);
    }

    public void SetCharacter()
    {
        StaticData.SelectedCharacter = CharData;
        setCharacterShow.GetComponentInParent<SetCharacterShow>().SetCharacter(CharData);
    }
}
