using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterInfo_1 : MonoBehaviour
{
    public MissionManager missionManager;

    public UpgradeData secondweapon;

    List<MissionObject> missionObjects;

    public InventorySlotsManager inventorySlotsManager;

    public MenuManager menuManager;

    public int currentHealth;

    public int currentSlowhealth = 0;

    public HealthBar healthBar;

    [SerializeField] public GameObject slowHealth;
    public SlowHealthBar slowHealthBar;

    [SerializeField] GameObject shield;
    public ShieldBar shieldBar;
    public Slider shieldSlider;

    public ExpBar expBar;
    public Slider expSlider;
    public int level;
    public int maxExpValue;

    public float elapsedTime;

    public CountSys countSys;

    public GameOverCoin overCoin;

    public PlayerStatShow statShow;

    [SerializeField] LevelUpSelectBuff levelUpSelectBuff;

    public List<UpgradeData> upgradeDatas;
    public List<UpgradeData> upgradeDatasFromChest;
    public List<UpgradeData> weaponSlotsManager = new List<UpgradeData>();
    public List<UpgradeData> itemSlotsManager = new List<UpgradeData>();

    [SerializeField] WeaponsManager weaponsManager;
    [SerializeField] PassiveItemsManager itemsManager;

    int currentExp;

    public int coins = 0;

    int maxHealth;

    public float healthPercent = 0;
    public float attackPercent = 0;
    public float speedPercent = 0;
    public float critPercent = 0;
    public float critDamagePercent = 0;
    public float expPercent = 0;

    int baseHealth;
    int baseAttack;
    float baseCrit;
    float baseCritDmg;
    float baseSpeed;
    int hpRegen;

    float timeToHealth = 30;
    float timerToHealth;

    [SerializeField]
    public CharacterData characterData;
    [SerializeField]
    GameObject CharacterAnimateTranform;
    public GameObject characterAnimate;
    public CharacterStats characterStats;
    public SkillInfo skillInfor;
    public SkillTree skillTree;

    public int numberMonsterKilled = 0;

    bool slowHealthAcquired = false;

    int shieldMaxValue;
    public int shieldCurrentValue;

    //AudioManager
    AudioManager audioManager;

    //kiểm tra bất tử
    [HideInInspector]
    public bool isInvincible = false;
    [HideInInspector]
    //phần trăm buff độ lớn của vk
    public float weaponSize = 0;
    //% Giam sat thuong nhan vao;
    [HideInInspector]
    public float reduceDmgTake=0;
    //Tỉ lệ né đòn
    [HideInInspector]
    public float evasion = 0;

    int respawnLife = 1;

    private void Awake()
    {
        healthPercent = PlayerPrefs.GetInt("HPlv", 0) * 0.04f;
        attackPercent = PlayerPrefs.GetInt("ATKlv", 0) * 0.04f;
        speedPercent = PlayerPrefs.GetInt("SPElv", 0) * 0.04f;
        critPercent = PlayerPrefs.GetInt("CRTlv", 0) * 1f;
        expPercent = PlayerPrefs.GetInt("EXPlv", 0) * 0.04f;
        hpRegen = PlayerPrefs.GetInt("HP Regenlv", 0);

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        StaticData.bigBossKill = 0;
    }

    private void Start()
    {
        if (StaticData.SelectedCharacter != null)
            characterData = StaticData.SelectedCharacter;

        timerToHealth = timeToHealth;

        missionObjects = missionManager.missionsObject;

        reduceDmgTake = 0;
        weaponSize = 0;
        evasion = 0;

        characterAnimate = Instantiate(characterData.animatorPrefab, CharacterAnimateTranform.transform);
        characterStats.SetStats(characterData.stats);
        skillInfor.SetData(characterData.skillInfo);
        skillTree.SetData(characterData.skillTree);


        baseAttack = characterStats.strenght;
        baseCrit = characterStats.crit;
        baseCritDmg=characterStats.critDmg;
        baseSpeed = characterStats.speed;
        baseHealth = characterStats.maxHealth;

        //load skill tree manager
        GetComponent<SkilTreeManager>().LoadBuffSkillTree(skillTree);

        statUpdate();

        maxHealth = characterStats.maxHealth;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        slowHealth.SetActive(false);
        shield.SetActive(false);
        shieldMaxValue = 0;
        shieldCurrentValue = 0;

        UpgradeData data = null;

        data = levelUpSelectBuff.weaponList.Find(item => item.weaponData == characterData.beginerWeapon);

        if(data != null)
        {
            weaponSlotsManager.Add(data);
            weaponsManager.AddWeapon(data.weaponData);
            levelUpSelectBuff.WeaponAcquired(data);
        }

        /*weaponSlotsManager.Add(secondweapon);
        weaponsManager.AddWeapon(secondweapon.weaponData);
        levelUpSelectBuff.WeaponAcquired(secondweapon);*/

        inventorySlotsManager.WeaponSlotUpdate(weaponSlotsManager);

        level = 1;
        maxExpValue = 10;
        currentExp = 0;
        expBar.SetMaxExp(level, maxExpValue);
        countSys.SetCoinCount(0);
        countSys.SetKillCount(0);
        overCoin.SetCoinGain(0);

        statShow.SetHealth(maxHealth);
        statShow.SetAttack(characterStats.strenght);
        statShow.SetSpeed(Mathf.FloorToInt(characterStats.speed));
        statShow.SetCrit(characterStats.crit);
    }

    // Update is called once per frame
    void Update()
    {
        countSys.SetCoinCount(coins);
        elapsedTime += Time.deltaTime;

        if(hpRegen != 0)
        {
            timerToHealth -= Time.deltaTime;
        }

        if (timerToHealth <= 0)
        {
            HealthByNumber(hpRegen);
            timerToHealth = timeToHealth;
        }

        if (slowHealthAcquired)
        {
            slowHealth.SetActive(true);

            if (currentHealth >= currentSlowhealth && currentHealth > 1)
            {
                currentSlowhealth = currentHealth;
                slowHealthBar.SetMaxHealth(maxHealth);
                slowHealthBar.SetHealth(currentSlowhealth);
            }
        }
    }

    public void PlusMaxSheild(int shieldPlus)
    {
        if (shieldMaxValue == 0)
        {
            shield.SetActive(true);
            shieldMaxValue += shieldPlus;
            shieldCurrentValue = shieldMaxValue;

            shieldBar.SetMaxShield(shieldMaxValue);
            shieldBar.SetShield(shieldCurrentValue);
        }
        else
        {
            shieldMaxValue += shieldPlus;
            shieldCurrentValue += shieldPlus;

            shieldBar.SetMaxShield(shieldMaxValue);
            shieldBar.SetShield(shieldCurrentValue);
        }
    }

    public void ShieldRecovery(int shieldRecovery)
    {
        shieldCurrentValue = (shieldCurrentValue + shieldRecovery > shieldMaxValue) ? shieldMaxValue : shieldCurrentValue + shieldRecovery;
        shieldBar.SetShield(shieldCurrentValue);
    }

    public void KilledMonster()
    {
        ++numberMonsterKilled;
        countSys.SetKillCount(numberMonsterKilled);
        MissionCheck();
        StaticData.totalKill = numberMonsterKilled;
    }

    public void GainCoin(int coinGain)
    {
        coins += coinGain;
        countSys.SetCoinCount(coins);
        StaticData.totalCoin = CoinGainPercent(coins, Mathf.FloorToInt(elapsedTime % 60));
    }

    public void GainExp(int exp)
    {
        audioManager.PlaySFX(audioManager.PickUpExp);
        currentExp += exp + (int)(exp * expPercent);

        if (currentExp >= maxExpValue)
        {
            LevelUp();
        }

        expBar.SetExp(currentExp);
    }

    public void LevelUp()
    {
        upgradeDatas = levelUpSelectBuff.GetUpgrades(4);
        menuManager.LevelUpScene(upgradeDatas);
        currentExp -= maxExpValue;
        level += 1;
        maxExpValue += Mathf.FloorToInt((float)(maxExpValue * 0.2));
        expBar.SetMaxExp(level, maxExpValue);
    }

    public void Upgrade(int id)
    {
        switch ((int)upgradeDatas[id].upgradeType)
        {
            case 0: //WeaponUpgrade
                {
                    upgradeDatas[id].acquired = true;
                    levelUpSelectBuff.WeaponNextUpgradeInfo(upgradeDatas[id]);
                    weaponsManager.AddWeapon(upgradeDatas[id].weaponData);

                    UpgradeData data = null;

                    data = weaponSlotsManager.Find(item => item.weaponData.name == upgradeDatas[id].weaponData.name);

                    if (data != null)
                    {
                        data.level = upgradeDatas[id].level;

                        if (data.level == 7)
                        {
                            data.maxed = true;
                        }
                    }

                    inventorySlotsManager.WeaponSlotUpdate(weaponSlotsManager);
                }
                break;
            case 1: //ItemUpgrade
                {
                    upgradeDatas[id].acquired = true;
                    levelUpSelectBuff.ItemNextUpgradeInfo(upgradeDatas[id]);
                    itemsManager.AddItem(upgradeDatas[id].itemsData);
                    inventorySlotsManager.ItemSlotUpdate(itemSlotsManager);
                }
                break;
            case 2: //WeaponUnlock
                {
                    weaponSlotsManager.Add(upgradeDatas[id]);
                    weaponsManager.AddWeapon(upgradeDatas[id].weaponData);
                    inventorySlotsManager.WeaponSlotUpdate(weaponSlotsManager);
                    upgradeDatas[id].acquired = true;

                    levelUpSelectBuff.WeaponAcquired(upgradeDatas[id]);
                }
                break;
            case 3: //ItemUnlock
                {
                    itemSlotsManager.Add(upgradeDatas[id]);
                    itemsManager.AddItem(upgradeDatas[id].itemsData);
                    inventorySlotsManager.ItemSlotUpdate(itemSlotsManager);
                    upgradeDatas[id].acquired = true;

                    levelUpSelectBuff.ItemAcquired(upgradeDatas[id]);

                    if (upgradeDatas[id].itemsData.name == "SlowHealth")
                    {
                        slowHealthAcquired = true;
                    }
                }
                break;
            case 4: //StatUpgrade
                {
                    if (upgradeDatas[id].buffName.Contains("ATK"))
                    {
                        attackPercent += 0.1f;
                        statUpdate();
                    }
                    if (upgradeDatas[id].buffName.Contains("CRT"))
                    {
                        critPercent += 2;
                        statUpdate();
                    }
                    if (upgradeDatas[id].buffName.Contains("HP"))
                    {
                        healthPercent += 0.1f;
                        statUpdate();
                    }
                    if (upgradeDatas[id].buffName.Contains("SPD"))
                    {
                        speedPercent += 0.05f;
                        statUpdate();
                    }
                }
                break;
            case 5: //GainCoin
                {
                    coins += 50;
                    countSys.SetCoinCount(coins);
                }
                break;
        }

        menuManager.LevelUpDone();
    }

    public void UpgradeFromChest(int id)
    {
        switch ((int)upgradeDatasFromChest[id].upgradeType)
        {
            case 0: //WeaponUpgrade
                {
                    upgradeDatasFromChest[id].acquired = true;
                    levelUpSelectBuff.WeaponNextUpgradeInfo(upgradeDatasFromChest[id]);
                    weaponsManager.AddWeapon(upgradeDatasFromChest[id].weaponData);

                    UpgradeData data = null;

                    data = weaponSlotsManager.Find(item => item.weaponData.name == upgradeDatasFromChest[id].weaponData.name);

                    if(data != null)
                    {
                        data.level = upgradeDatasFromChest[id].level;

                        if(data.level == 7)
                        {
                            data.maxed = true;
                        }
                    }

                    inventorySlotsManager.WeaponSlotUpdate(weaponSlotsManager);
                }
                break;
            case 1: //ItemUpgrade
                {
                    upgradeDatasFromChest[id].acquired = true;
                    levelUpSelectBuff.ItemNextUpgradeInfo(upgradeDatasFromChest[id]);
                    itemsManager.AddItem(upgradeDatasFromChest[id].itemsData);
                    inventorySlotsManager.ItemSlotUpdate(itemSlotsManager);
                }
                break;
            case 2: //WeaponUnlock
                {
                    weaponSlotsManager.Add(upgradeDatasFromChest[id]);
                    weaponsManager.AddWeapon(upgradeDatasFromChest[id].weaponData);
                    inventorySlotsManager.WeaponSlotUpdate(weaponSlotsManager);
                    upgradeDatasFromChest[id].acquired = true;

                    levelUpSelectBuff.WeaponAcquired(upgradeDatasFromChest[id]);
                }
                break;
            case 3: //ItemUnlock
                {
                    itemSlotsManager.Add(upgradeDatasFromChest[id]);
                    itemsManager.AddItem(upgradeDatasFromChest[id].itemsData);
                    inventorySlotsManager.ItemSlotUpdate(itemSlotsManager);
                    upgradeDatasFromChest[id].acquired = true;

                    levelUpSelectBuff.ItemAcquired(upgradeDatasFromChest[id]);

                    if (upgradeDatasFromChest[id].itemsData.name == "SlowHealth")
                    {
                        slowHealthAcquired = true;
                    }
                }
                break;
            case 4: //StatUpgrade
                {
                    if (upgradeDatasFromChest[id].buffName.Contains("ATK"))
                    {
                        attackPercent += 0.1f;
                        statUpdate();
                    }
                    if (upgradeDatasFromChest[id].buffName.Contains("CRT"))
                    {
                        critPercent += 2;
                        statUpdate();
                    }
                    if (upgradeDatasFromChest[id].buffName.Contains("HP"))
                    {
                        healthPercent += 0.1f;
                        statUpdate();
                    }
                    if (upgradeDatasFromChest[id].buffName.Contains("SPD"))
                    {
                        speedPercent += 0.05f;
                        statUpdate();
                    }
                }
                break;
            case 5: //GainCoin
                {
                    coins += 50;
                    countSys.SetCoinCount(coins);
                }
                break;
        }
    }

    public void AddUpgrade(UpgradeData upgradeData, bool type)//true = weapon, false = item
    {
        if (type)
        {

            UpgradeData data = null;

            data = weaponSlotsManager.Find(item => item.weaponData.name == upgradeData.weaponData.name);

            if(data != null)
            {
                if (data.maxed)
                {
                    data.overLevel += 1;
                }
                else
                {
                    data.level += 1;
                    upgradeData.acquired = true;
                    weaponsManager.AddWeapon(upgradeData.weaponData);
                    levelUpSelectBuff.WeaponNextUpgradeInfo(upgradeData);
                    if (data.level == 7)
                    {
                        data.maxed = true;
                    }
                }
            }


            /*for (int i = 0; i < weaponSlotsManager.Count; i++)
            {
                if (weaponSlotsManager[i].weaponData.name == upgradeData.weaponData.name && upgradeData.maxed)
                {
                    upgradeData.overLevel += 1;
                    break;
                }

                if (weaponSlotsManager[i].weaponData.name == upgradeData.weaponData.name)
                {
                    weaponSlotsManager[i].level += 1;
                    if (weaponSlotsManager[i].level == 7)
                    {
                        weaponSlotsManager[i].maxed = true;
                    }
                    break;
                }
            }*/

            inventorySlotsManager.WeaponSlotUpdate(weaponSlotsManager);
        }
        else
        {
            upgradeData.acquired = true;
            levelUpSelectBuff.ItemNextUpgradeInfo(upgradeData);
            itemsManager.AddItem(upgradeData.itemsData);
            inventorySlotsManager.ItemSlotUpdate(itemSlotsManager);
        }
    }

    public void AddItem(UpgradeData upgradeData)
    {
        upgradeData.acquired = true;
        itemSlotsManager.Add(upgradeData);
        levelUpSelectBuff.ItemNextUpgradeInfo(upgradeData);
        itemsManager.AddItem(upgradeData.itemsData);
        inventorySlotsManager.ItemSlotUpdate(itemSlotsManager);
    }

    public void AddCollab(UpgradeData collab)
    {
        UpgradeData remove1 = null;
        UpgradeData remove2 = null;

        /*for(int i = 0; i < weaponSlotsManager.Count; i++)
        {
            if (weaponSlotsManager[i].weaponData == collab.colabInfo.weapon1 || weaponSlotsManager[i].weaponData == collab.colabInfo.weapon2)
            {
                if(remove1 == null)
                {
                    remove1 = weaponSlotsManager[i];
                }
                else if(remove2 == null)
                {
                    remove2 = weaponSlotsManager[i];
                }
            }
        }*/

        remove1 = weaponSlotsManager.Find(item => item.weaponData == collab.colabInfo.weapon1);
        remove2 = weaponSlotsManager.Find(item => item.weaponData == collab.colabInfo.weapon2);

        weaponSlotsManager.Remove(remove1);
        weaponSlotsManager.Remove(remove2);

        levelUpSelectBuff.RemoveEquipWeapon(remove1);
        weaponsManager.RemoveWeapon(remove1.weaponData);

        levelUpSelectBuff.RemoveEquipWeapon(remove2);
        weaponsManager.RemoveWeapon(remove2.weaponData);

        weaponsManager.AddWeapon(collab.weaponData);
        levelUpSelectBuff.AddCollabWeapon(collab);
        weaponSlotsManager.Add(collab);

        inventorySlotsManager.WeaponSlotUpdate(weaponSlotsManager);
    }

    public void TakeDamage(int damage)
    {
        if (UnityEngine.Random.value * 100 <= evasion)
        {
            MessengerSystem.instance.Miss(transform.position);
            return;
        }

        //Giam dmg nhan vao
        damage -= (int)Mathf.Ceil((float)damage * reduceDmgTake);

        if(!isInvincible)
        {
            if (shieldCurrentValue <= 0)
            {
                currentHealth -= damage;
                MissionCheck();

                //AudioManager
                audioManager.PlaySFX(audioManager.TakeDmg);

                if (slowHealthAcquired)
                {
                    if (currentHealth < 1)
                        currentHealth = 1;

                    if (currentSlowhealth == 1)
                        currentSlowhealth -= 1;

                    slowHealthBar.SetHealth(currentSlowhealth);
                }

                healthBar.SetHealth(currentHealth, slowHealthAcquired);

                if (currentHealth <= 0 || (currentSlowhealth <= 0 && slowHealthAcquired))
                {
                    if(respawnLife > 0)
                    {
                        menuManager.AdsScreen();
                        --respawnLife;
                    }
                    else
                    {
                        GameOver();
                    }
                }
            }
            else
            {
                if (shieldCurrentValue > damage)
                {
                    shieldCurrentValue -= damage;
                    shieldBar.SetShield(shieldCurrentValue);
                }
                else
                {
                    int dmg = damage - shieldCurrentValue;
                    shieldCurrentValue = 0;
                    shieldBar.SetShield(shieldCurrentValue);
                    TakeDamage(dmg);
                }
            }
        }
    }

    public void GameOver()
    {
        menuManager.GameOverScreen();
        overCoin.SetCoinGain(coins);
    }

    public void HealthByPercent(int health)
    {
        float heal =Mathf.Ceil((float)maxHealth * (float)health / 100);
        currentHealth += (int)heal;
        MessengerSystem.instance.Heal(transform.position, (int)heal);
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth, slowHealthAcquired);
    }

    public void HealthByNumber(int health)
    {
        currentHealth += health;
        MessengerSystem.instance.Heal(transform.position, health);
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth, slowHealthAcquired);
    }

    public int CoinGainPercent(int coins, int timer)
    {
        if (timer <= 600)
            return ((int)((float)(coins * 0.25)));
        else if (timer <= 900)
            return ((int)((float)(coins * 0.5)));
        else if (timer <= 1200)
            return ((int)((float)(coins * 0.75)));

        return coins;
    }

    public void statUpdate()
    {
        characterStats.strenght = baseAttack + Mathf.FloorToInt((float)baseAttack * attackPercent);
        characterStats.crit = baseCrit + critPercent;
        characterStats.critDmg=baseCritDmg+critDamagePercent;
        characterStats.maxHealth = baseHealth + Mathf.FloorToInt((float)baseHealth * healthPercent);
        characterStats.speed = baseSpeed + Mathf.FloorToInt((float)baseSpeed * speedPercent);
        statShow.SetAttack(characterStats.strenght);
        statShow.SetCrit(characterStats.crit);
        maxHealth = characterStats.maxHealth;
        statShow.SetHealth(characterStats.maxHealth);
        statShow.SetSpeed(Mathf.FloorToInt(characterStats.speed));

        if (slowHealthAcquired)
        {
            slowHealthBar.SetMaxHealth(characterStats.maxHealth);
            slowHealthBar.SetHealth(currentSlowhealth);
        }
        else
        {
            healthBar.SetMaxHealth(characterStats.maxHealth);
            healthBar.SetHealth(currentHealth);
        }
    }

    public void SlowHealthDe(int dmg)
    {
        if (currentSlowhealth > currentHealth)
        {
            currentSlowhealth -= dmg;

            slowHealthBar.SetHealth(currentSlowhealth);
        }
    }

    public float healthCheck;

    public void MissionCheck()
    {
        foreach(var item in missionObjects)
        {
            if (!item.missionInfo.completed)
            {
                if (item.missionInfo.missionType.ToString() == "HP")
                {
                    healthCheck = maxHealth * ((float)item.missionInfo.num / 100);
                    if (currentHealth <= healthCheck)
                        item.missionHolder.GetComponent<SetMission>().SetHPMissionComplete(item.missionInfo, true);
                }
                if (item.missionInfo.missionType.ToString() == "Kill")
                {
                    item.missionHolder.GetComponent<SetMission>().SetKillMissionProgress(item.missionInfo, numberMonsterKilled);
                    if(numberMonsterKilled >= item.missionInfo.num)
                        item.missionHolder.GetComponent<SetMission>().SetKillMissionComplete(item.missionInfo, true, numberMonsterKilled);
                }
            }
        }
    }
}

