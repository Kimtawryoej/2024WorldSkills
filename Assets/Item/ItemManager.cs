using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Runtime.ConstrainedExecution;

public enum Item { None, SpeedUp, LittleSpeedUp, Hun, TwoHun, Thou, ToShop }
public class ItemManager : MonoSingleTone<ItemManager>
{
    [SerializeField] private Item item;
    public Item Item => item;
    [SerializeField] private GameObject itemCheck;
    private Dictionary<Item, (Sprite, Action<Car>)> itemList;
    public Dictionary<Item, (Sprite, Action<Car>)> ItemList => itemList;

    private void Start()
    {
        ItemSet();
    }

    private void ItemSet()
    {
        itemList = new Dictionary<Item, (Sprite, Action<Car>)>()
        {
            {Item.SpeedUp,(Resources.Load<Sprite>($"UI_Skill_Icon_Dash"),car=>{car.SpeedControlPro.SpeedChange(20, true, 3); ItemCheck(($"SpeedUp"));})},
            {Item.LittleSpeedUp,(Resources.Load<Sprite>($"Ability_Skill_Icons/UI_Skill_Icon_Dash"),car=>{car.SpeedControlPro.SpeedChange(10, true, 3);ItemCheck(($"LittleSpeedUp"));})},
            {Item.Hun,(Resources.Load<Sprite>($"Ability_Skill_Icons/UI_Skill_Icon_Fly"),car=>{GameManager.Instance.Gold = 1000000; ItemCheck(($"GetGoldLittle"));})},
            {Item.TwoHun,(Resources.Load<Sprite>($"Ability_Skill_Icons/UI_Skill_Icon_Fly"),car=>{GameManager.Instance.Gold = 2000000; ItemCheck(($"GetGoldMany"));})},
            {Item.Thou,(Resources.Load<Sprite>($"Ability_Skill_Icons/UI_Skill_Icon_Fly"),car=>{GameManager.Instance.Gold = 10000000; ItemCheck(($"GetGoldVeryMany"));})},
            {Item.ToShop,(Resources.Load<Sprite>($"Ability_Skill_Icons/UI_Skill_Icon_Heal"),car=>{GameManager.Instance.SwitchCamera(); Time.timeScale = 0; })}
        };
    }

    private void ItemCheck(string itemName)
    {
        TimerSystem.Instance.AddTimer(new TimeAgent(1.4f, (TimeAgent) =>
        {
            itemCheck.GetComponentInChildren<Text>().text = itemName;
            itemCheck.gameObject.SetActive(true);
        }
        , (TimeAgent) => { }, (TimeAgent) => { itemCheck.gameObject.SetActive(false); }));
    }
}
