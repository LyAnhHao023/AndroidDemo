using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharManager : MonoBehaviour
{
    [SerializeField] List<CharacterData> characterDatas;

    public GameObject characterHolderPrefab;
    public GameObject characterTranform;
    GameObject characterButton;

    private void Start()
    {
        for (int i = 0; i < characterDatas.Count; i++)
        {
            characterButton = Instantiate(characterHolderPrefab, characterTranform.transform);
            characterButton.GetComponent<CharacterButton>().Set(characterDatas[i]);
        }
    }

    public void Select(int id)
    {
        StaticData.SelectedCharacter = characterDatas[id];
    }
}
