using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectSceneManager : MonoBehaviour
{

    public Text NameText;
    public Text HpText;
    public Text StaminaText;
    public Text SkillText;
    public Text SkillDescriptionText;
    public Image SelectImage;

    public Button NextButton;
    public Button[] Select;
    public Sprite[] SelectImages;


    bool isSelect = false;
    /*1번 전사
     * 2번 마법사
     */
    void Start()
    {
        NextButton.onClick.AddListener(OnClickNextButton);
        Select[0].onClick.AddListener(OnClickSelectWarrior);
        //Select[1].onClick.AddListener(OnClickSelectWizard);
        //기본 전사
        PlayerStats.Instance.Select = 0;
        PlayerStats.Instance.Level = 2;
        PlayerStats.Instance.BaseAttackPower = 20;
        PlayerStats.Instance.SkillDamage = 70;
        PlayerStats.Instance.SkillCost = 70;
        PlayerStats.Instance.MaxHp = 150;
        PlayerStats.Instance.CurrentHp = 150;
        PlayerStats.Instance.MaxStamina = 150;
        PlayerStats.Instance.CurrentStamina = 100;
        PlayerStats.Instance.RunSpeed = 7;
        PlayerStats.Instance.Gold = 790;
        NameText.text = "Warrior";
        HpText.text = $"Hp : {PlayerStats.Instance.MaxHp}";
        StaminaText.text = $"Stamina : {PlayerStats.Instance.MaxStamina}";
        SkillText.text = "Skill";
        SkillDescriptionText.text = "Sword Aura!";
        SelectImage.gameObject.SetActive(true);
        SelectImage.sprite = SelectImages[0];

        isSelect = true;
    }
    void OnClickSelectWarrior()
    {
        PlayerStats.Instance.Level = 1;
        PlayerStats.Instance.IsDead = false;
        PlayerStats.Instance.Select = 0;
        PlayerStats.Instance.BaseAttackPower = 20;
        PlayerStats.Instance.SkillDamage = 70;
        PlayerStats.Instance.SkillCost = 70;
        PlayerStats.Instance.MaxHp = 150;
        PlayerStats.Instance.CurrentHp = 150;
        PlayerStats.Instance.MaxStamina = 150;
        PlayerStats.Instance.CurrentStamina = 100;
        PlayerStats.Instance.RunSpeed = 7;
        PlayerStats.Instance.Gold = 100;


        NameText.text = "Warrior";
        HpText.text = $"Hp : {PlayerStats.Instance.MaxHp}";
        StaminaText.text = $"Stamina : {PlayerStats.Instance.MaxStamina}";
        SkillText.text = "Skill";
        SkillDescriptionText.text = "Sword Aura!";
        SelectImage.gameObject.SetActive(true);
        SelectImage.sprite = SelectImages[0];

        isSelect = true;
    }
    //void OnClickSelectWizard()
    //{
        
    //    PlayerStats.Instance.Select = 1;
    //    PlayerStats.Instance.BaseAttackPower = 10;
    //    PlayerStats.Instance.MaxHp = 50;
    //    PlayerStats.Instance.CurrentHp = 50;
    //    PlayerStats.Instance.MaxStamina = 150;
    //    PlayerStats.Instance.CurrentStamina = 150;

    //    PlayerStats.Instance.SkillDamage = 20;

    //    NameText.text = "Wizard";
    //    HpText.text = $"Hp : {PlayerStats.Instance.MaxHp}";
    //    StaminaText.text = $"Stamina : {PlayerStats.Instance.MaxStamina}";
    //    SkillText.text = "Skill";
    //    SkillDescriptionText.text = "MagicBallAttack!";
    //    SelectImage.gameObject.SetActive(true);
    //    SelectImage.sprite = SelectImages[1];

    //    isSelect = true;
    //}
    void OnClickNextButton()
    {
        if (isSelect)
        {
            SceneManager.LoadScene("GameStart");
        }
        else
        {

        }
    }
}
