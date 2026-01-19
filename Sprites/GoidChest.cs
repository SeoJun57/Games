using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldChest : MonoBehaviour
{
    private AudioSource _as;
    private Animator _animator;
    public GameObject ChestPanal;
    public GameObject ChestButtonImage;

    public AudioClip ChestOpenSound;
    public AudioClip ChestCloseSound;

    public ItemSO ItemData;
    private Item _item;

    public Image ItemImage;
    public Text ItemNameText;
    public Text ItemDescriptionText;
    public Button ItemButton;


    bool isOpened = false;
    bool isPanal = false;
    bool isReword = true;
    int itemCreatCount = 0;
    void Start()
    {
        _as = GetComponent<AudioSource>();
        //아이템 다 먹었으면 생성 X
        for (int i = 0; i < ItemData.items.Length; i++)
        {
            if (PlayerStats.Instance.ItemTrue[i])
            {
                itemCreatCount++;
            }
        }
        if (itemCreatCount == PlayerStats.Instance.ItemTrue.Length)
        {
            ItemButton.gameObject.SetActive(false);
        }
        else
        {

          
            int ran = Random.Range(0, 101);
            if (ran != 111)
            {
                SetItem();
            }
            else
            {
                ItemButton.gameObject.SetActive(false);
            }
        }

        _animator = GetComponent<Animator>();
    }


    void Update()
    {
        float dis = Vector3.Distance(transform.position, PlayerStats.Instance.PlayerTransform);
        //f버튼 이미지 활성화,비활성화 로직
        if (dis < 2.5f)
        {
            ChestButtonImage.gameObject.SetActive(true);
            isPanal = true;
        }
        else
        {
            ChestButtonImage.gameObject.SetActive(false);
            isPanal = false;
        }
        OnChestPanal();
        ChestOpen();
    }
    void ChestOpen()
    {
        float dis = Vector3.Distance(transform.position, PlayerStats.Instance.PlayerTransform);
        if (Input.GetKeyDown(KeyCode.F) && isOpened == false && dis <= 2.5)
        {
            _as.PlayOneShot(ChestOpenSound);
            _animator.SetBool("isOpen", true);
            isOpened = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && isOpened)
        {
            _as.PlayOneShot(ChestCloseSound);
            _animator.SetBool("isOpen", false);
            isOpened = false;
        }
    }
    void OnChestPanal()
    {
        float dis = Vector3.Distance(transform.position, PlayerStats.Instance.PlayerTransform);
        if (Input.GetKeyDown(KeyCode.F) && dis < 2.5f && ChestPanal.activeSelf == false && isPanal)
        {
            ChestPanal.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.F) && dis < 2.5f && ChestPanal.activeSelf || isPanal == false)
        {
            ChestPanal.SetActive(false);
        }
    }
    void SetItem()
    {
        int ran = Random.Range(0, ItemData.items.Length);
        while (PlayerStats.Instance.ItemTrue[ran])
        {
            ran = Random.Range(0, ItemData.items.Length);
        }
        _item = ItemData.items[ran];
        //아이템 이미지 스프라이트, 이름, 설명 변경
        ItemImage.sprite = _item.itemIcon;
        ItemNameText.text = _item.itemName;
        ItemDescriptionText.text = _item.description;
        ItemButton.onClick.AddListener(OnClickItemButton);
       
    }
    void OnClickItemButton()
    {
        ItemButton.gameObject.SetActive(false);
        PlayerStats.Instance.ItemTrue[_item.itemID] = true;
        for (int i = 0; i < _item.type.Count; i++)
        {
            switch (_item.type[i])
            {
                case Type.AttackPowerUp:
                    PlayerStats.Instance.BonusAttackPower += (int)_item.value[i];
                    break;
                case Type.MaxHpUp:
                    PlayerStats.Instance.MaxHp += _item.value[i];
                    PlayerStats.Instance.CurrentHp += _item.value[i];
                    break;
                case Type.MaxStaminaUp:
                    PlayerStats.Instance.MaxStamina += _item.value[i];
                    PlayerStats.Instance.CurrentStamina += _item.value[i];
                    break;
                case Type.RunSpeedUp:
                    PlayerStats.Instance.RunSpeed += _item.value[i];
                    break;
                case Type.SkillDamageUp:
                    PlayerStats.Instance.SkillDamage += (int)_item.value[i];
                    break;
                default:
                    break;
            }
        }
        PlayerStatsBar.Instance.AddItem(_item.itemIcon);
    }
}
