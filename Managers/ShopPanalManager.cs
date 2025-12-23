using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanalManager : MonoBehaviour
{
    public Button Choice1;
    public Button Choice2;
    public Button Choice3;

    public Image[] ChoiceImage;
    public Text[] Description;
    public Text[] Price;

    public SaleSO SaleData;
    private Sales[] _sales = new Sales[3];



    void Start()
    {
        SetChoice1();
        SetChoice2();
        SetChoice3();

    }
    //아이템 이미지 스프라이트, 이름, 설명 변경  
    void SetChoice1()
    {
        int ran = Random.Range(0, SaleData.saleItem.Length);
        _sales[0] = SaleData.saleItem[ran];
        ChoiceImage[0].sprite = _sales[0].itemSprite;
        Description[0].text = _sales[0].description;
        Price[0].text = _sales[0].price.ToString();
        Choice1.onClick.AddListener(OnClick1);
        void OnClick1()
        {
            int price = _sales[0].price;
            if (PlayerStats.Instance.Gold >= price)
            {
                Choice1.gameObject.SetActive(false);
                PlayerStats.Instance.Gold -= price;
                for (int i = 0; i < _sales[0].type.Count; i++)
                {
                    switch (_sales[0].type[i])
                    {
                        case Type.AttackPowerUp:
                            PlayerStats.Instance.BonusAttackPower += (int)_sales[0].value[i];
                            break;
                        case Type.MaxHpUp:
                            PlayerStats.Instance.MaxHp += _sales[0].value[i];
                            PlayerStats.Instance.CurrentHp += _sales[0].value[i];
                            break;
                        case Type.MaxStaminaUp:
                            PlayerStats.Instance.MaxStamina += _sales[0].value[i];
                            PlayerStats.Instance.CurrentStamina += _sales[0].value[i];
                            break;
                        case Type.RunSpeedUp:
                            PlayerStats.Instance.RunSpeed += _sales[0].value[i];
                            break;
                        case Type.SkillDamageUp:
                            PlayerStats.Instance.SkillDamage += (int)_sales[0].value[i];
                            break;
                        case Type.HpRecovery:
                            PlayerStats.Instance.CurrentHp += (int)_sales[0].value[i];
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
    void SetChoice2()
    {
        int ran = Random.Range(0, SaleData.saleItem.Length);
        _sales[1] = SaleData.saleItem[ran];
        ChoiceImage[1].sprite = _sales[1].itemSprite;
        Description[1].text = _sales[1].description;
        Price[1].text = _sales[1].price.ToString();
        Choice2.onClick.AddListener(OnClick2);
        void OnClick2()
        {
            int price = _sales[1].price;
            if (PlayerStats.Instance.Gold >= price)
            {
                Choice2.gameObject.SetActive(false);
                PlayerStats.Instance.Gold -= price;
                for (int i = 0; i < _sales[1].type.Count; i++)
                {
                    switch (_sales[1].type[i])
                    {
                        case Type.AttackPowerUp:
                            PlayerStats.Instance.BonusAttackPower += (int)_sales[1].value[i];
                            break;
                        case Type.MaxHpUp:
                            PlayerStats.Instance.MaxHp += _sales[1].value[i];
                            PlayerStats.Instance.CurrentHp += _sales[1].value[i];
                            break;
                        case Type.MaxStaminaUp:
                            PlayerStats.Instance.MaxStamina += _sales[1].value[i];
                            PlayerStats.Instance.CurrentStamina += _sales[1].value[i];
                            break;
                        case Type.RunSpeedUp:
                            PlayerStats.Instance.RunSpeed += _sales[1].value[i];
                            break;
                        case Type.SkillDamageUp:
                            PlayerStats.Instance.SkillDamage += (int)_sales[1].value[i];
                            break;
                        case Type.HpRecovery:
                            PlayerStats.Instance.CurrentHp += (int)_sales[1].value[i];
                            break;
                        default:
                            break;
                    }
                }
            }
        }

    }
    void SetChoice3()
    {
        int ran = Random.Range(0, SaleData.saleItem.Length);
        _sales[2] = SaleData.saleItem[ran];
        ChoiceImage[2].sprite = _sales[2].itemSprite;
        Description[2].text = _sales[2].description;
        Price[2].text = _sales[2].price.ToString();
        Choice3.onClick.AddListener(OnClick3);
        void OnClick3()
        {
            int price = _sales[2].price;
            if (PlayerStats.Instance.Gold >= price)
            {
                Choice3.gameObject.SetActive(false);
                PlayerStats.Instance.Gold -= price;
                for (int i = 0; i < _sales[2].type.Count; i++)
                {
                    switch (_sales[2].type[i])
                    {
                        case Type.AttackPowerUp:
                            PlayerStats.Instance.BonusAttackPower += (int)_sales[2].value[i];
                            break;
                        case Type.MaxHpUp:
                            PlayerStats.Instance.MaxHp += _sales[2].value[i];
                            PlayerStats.Instance.CurrentHp += _sales[2].value[i];
                            break;
                        case Type.MaxStaminaUp:
                            PlayerStats.Instance.MaxStamina += _sales[2].value[i];
                            PlayerStats.Instance.CurrentStamina += _sales[2].value[i];
                            break;
                        case Type.RunSpeedUp:
                            PlayerStats.Instance.RunSpeed += _sales[2].value[i];
                            break;
                        case Type.SkillDamageUp:
                            PlayerStats.Instance.SkillDamage += (int)_sales[2].value[i];
                            break;
                        case Type.HpRecovery:
                            PlayerStats.Instance.CurrentHp += (int)_sales[2].value[i];
                            break;  
                        default:
                            break;
                    }
                }
            }
        }

    }
}
