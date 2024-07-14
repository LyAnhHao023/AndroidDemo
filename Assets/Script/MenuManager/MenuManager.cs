using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _settingsMenuCanvas;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _levelUpUI;
    [SerializeField] private GameObject _OpenChestUI;
    [SerializeField] private GameObject statShow;
    [SerializeField] private GameObject buffTable;
    [SerializeField] private GameObject missionUI;
    [SerializeField] private GameObject _UpgradeAnvilUI;
    [SerializeField] private GameObject _UpgradeAnvilResultUI;
    [SerializeField] private GameObject _UpgradeAnvilInfoHolderUI;
    [SerializeField] private GameObject _CollabAnvilUI;
    [SerializeField] private GameObject _AdsUI;

    private UpgradeSlotManager _slotManager;
    private CollabSlotManager _collabSlotManager;

    /*[SerializeField] private GameObject _mainMenuFirst;*/
    /*[SerializeField] private GameObject _settingsMenuFirst;*/

    [SerializeField] List<UpgradeButton> upgradeButtons;

    [SerializeField] private GameObject levelUpEffect;
    [SerializeField] private ParticleSystem _levelUpEffect;

    [SerializeField] Vector3 statShowStep;
    [SerializeField] Vector3 startStatShowStep;
    [SerializeField] Vector3 buffTableStep;
    [SerializeField] Vector3 startBuffTableStep;
    [SerializeField] Vector3 openChestStep;
    [SerializeField] Vector3 startOpenChestStep;
    [SerializeField] Vector3 missionStep;
    [SerializeField] Vector3 startMissionStep;
    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;

    [SerializeField] Text GameOverText;

    [SerializeField] CharacterInfo_1 characterInfo;

    [SerializeField] GameObject Inventory;
    [SerializeField] GameObject SkillHolder;

    [SerializeField] AudioManager audioManager;

    private bool isPaused;

    private bool isGameOver;

    private bool isSelectBuff;

    float timer = 10;
    bool missionShow = false;

    // Start is called before the first frame update
    void Start()
    {
        _mainMenuCanvas.SetActive(false);
        _settingsMenuCanvas.SetActive(false);
        _gameOverUI.SetActive(false);
        _levelUpUI.SetActive(false);
        _OpenChestUI.SetActive(false);
        levelUpEffect.SetActive(false);
        _UpgradeAnvilUI.SetActive(false);
        _UpgradeAnvilResultUI.SetActive(false);
        _UpgradeAnvilInfoHolderUI.SetActive(false);
        _CollabAnvilUI.SetActive(false);

        isGameOver = false;
        isSelectBuff = false;
        isPaused = false;

        _slotManager = _UpgradeAnvilUI.GetComponent<UpgradeSlotManager>();
        _collabSlotManager = _CollabAnvilUI.GetComponent<CollabSlotManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= 0)
        {
            timer -= Time.deltaTime;
        }

        if(timer < 0 && !missionShow)
        {
            missionUI.LeanMoveLocal(startMissionStep, tweenTime).setEase(tweenType).setIgnoreTimeScale(true);
            missionShow = true;
        }

        if (InputManager.instance.MenuOpenCloseInput)
        {
            if (!isGameOver && !isSelectBuff)
            {
                if (!isPaused)
                {
                    Pause();
                }
                else
                {
                    Unpause();
                }
            }
        }
            
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;

        OpenMainMenu();
    }

    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1f;

        CloseAllMenus();
    }

    private void OpenMainMenu()
    {
        _mainMenuCanvas.SetActive(true);
        _settingsMenuCanvas.SetActive(false);
        missionUI.LeanMoveLocal(missionStep, tweenTime).setEase(tweenType).setIgnoreTimeScale(true);

        EventSystem.current.SetSelectedGameObject(null);
    }

    /*private void OpenSettingsMenuHandle()
    {
        _settingsMenuCanvas.SetActive(true);
        _mainMenuCanvas.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_settingsMenuFirst);
    }*/

    private void CloseAllMenus()
    {
        _mainMenuCanvas.SetActive(false);
        _settingsMenuCanvas.SetActive(false);
        missionUI.LeanMoveLocal(startMissionStep, tweenTime).setEase(tweenType).setIgnoreTimeScale(true);

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Unpause();
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Unpause();
    }

    public void AdsScreen()
    {
        isPaused = true;
        Time.timeScale = 0f;

        _AdsUI.SetActive(true);
    }

    public void GameOverScreen(bool stageComplete = false)
    {

        isPaused = true;
        isGameOver = true;
        Time.timeScale = 0f;

        characterInfo.MissionCheck();

        _gameOverUI.SetActive(true);

        missionUI.LeanMoveLocal(missionStep, tweenTime).setEase(tweenType).setIgnoreTimeScale(true);

        EventSystem.current.SetSelectedGameObject(null);

        int totalK = PlayerPrefs.GetInt("TotalKill", 0) + StaticData.totalKill;
        PlayerPrefs.SetInt("TotalKill", totalK);
        PlayerPrefs.Save();

        int coinLocal = PlayerPrefs.GetInt("Coins", 0);
        PlayerPrefs.SetInt("Coins", coinLocal + StaticData.totalCoin);
        PlayerPrefs.Save();

        GameOverText.text = stageComplete ? "CLEAR STAGE" : "GAME OVER";

        audioManager.PlaySFX(stageComplete?audioManager.Win:audioManager.Lose);

    }

    public void LevelUpScene(List<UpgradeData> upgradeDatas)
    {
        isSelectBuff = true;
        isPaused = true;

        audioManager.PlaySFX(audioManager.LevelUp);

        statShow.LeanMoveLocal(statShowStep, tweenTime).setEase(tweenType).setIgnoreTimeScale(true);
        buffTable.LeanMoveLocal(buffTableStep, tweenTime).setEase(tweenType).setIgnoreTimeScale(true);

        for (int i = 0; i < upgradeDatas.Count; i++)
        {
            upgradeButtons[i].Set(upgradeDatas[i]);
        }

        Time.timeScale = 0f;
        _levelUpUI.SetActive(true);
        levelUpEffect.SetActive(true);

        _levelUpEffect.Play();

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void LevelUpDone()
    {
        _levelUpEffect.Stop();

        statShow.LeanMoveLocal(startStatShowStep, 0f).setEase(tweenType).setIgnoreTimeScale(true);
        buffTable.LeanMoveLocal(startBuffTableStep, 0f).setEase(tweenType).setIgnoreTimeScale(true);

        isSelectBuff = false;

        _levelUpUI.SetActive(false);
        levelUpEffect.SetActive(false);
        Unpause();
    }

    public void OpenChestScene()
    {
        isSelectBuff = true;
        isPaused = true;

        _OpenChestUI.LeanMoveLocal(openChestStep, tweenTime).setEase(tweenType).setIgnoreTimeScale(true);

        Time.timeScale = 0f;

        _OpenChestUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void CloseChestScene()
    {
        _OpenChestUI.LeanMoveLocal(startOpenChestStep, tweenTime).setEase(tweenType).setIgnoreTimeScale(true);

        isSelectBuff = false;

        _OpenChestUI.SetActive(false);
        Unpause();
    }

    public void AnvilUpgradeScene()
    {
        _slotManager.SetWeapon(characterInfo.weaponSlotsManager);
        _slotManager.SetPassiveItem(characterInfo.itemSlotsManager);
        _UpgradeAnvilUI.SetActive(true);
        Inventory.SetActive(false);
        SkillHolder.SetActive(false);
        isPaused = true;
        isSelectBuff = true;

        Time.timeScale = 0f;

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void AnvilUpgradeDone()
    {
        isSelectBuff = false;

        _UpgradeAnvilUI.SetActive(false);
        Inventory.SetActive(true);
        SkillHolder.SetActive(true);
        _UpgradeAnvilResultUI.SetActive(false);
        _UpgradeAnvilInfoHolderUI.SetActive(false);
        Unpause();
    }

    public void OpenCollabAnvilScene()
    {
        _collabSlotManager.SetWeapon(characterInfo.weaponSlotsManager);
        _collabSlotManager.SetPassiveItem(characterInfo.itemSlotsManager);
        _CollabAnvilUI.SetActive(true);
        Inventory.SetActive(false);
        SkillHolder.SetActive(false);
        isPaused = true;
        isSelectBuff = true;

        Time.timeScale = 0f;

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void CloseCollabAnvilScene()
    {
        isSelectBuff = false;

        _CollabAnvilUI.SetActive(false);
        Inventory.SetActive(true);
        SkillHolder.SetActive(true);
        _UpgradeAnvilResultUI.SetActive(false);
        _UpgradeAnvilInfoHolderUI.SetActive(false);
        Unpause();
    }
}
