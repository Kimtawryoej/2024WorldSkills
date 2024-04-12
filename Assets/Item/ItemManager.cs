using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

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
            {Item.SpeedUp,(Resources.Load<Sprite>($"UI_Skill_Icon_Dash"),car=>{StartCoroutine(car.SpeedChange(20,true,3)); StartCoroutine(ItemCheck(($"SpeedUp")));})},
            {Item.LittleSpeedUp,(Resources.Load<Sprite>($"Ability_Skill_Icons/UI_Skill_Icon_Dash"),car=>{StartCoroutine(car.SpeedChange(10, true,3));StartCoroutine(ItemCheck(($"LittleSpeedUp")));})},
            {Item.Hun,(Resources.Load<Sprite>($"Ability_Skill_Icons/UI_Skill_Icon_Fly"),car=>{GameManager.Instance.Gold = 1000000; StartCoroutine(ItemCheck(($"GetGoldLittle")));})},
            {Item.TwoHun,(Resources.Load<Sprite>($"Ability_Skill_Icons/UI_Skill_Icon_Fly"),car=>{GameManager.Instance.Gold = 2000000; StartCoroutine(ItemCheck(($"GetGoldMany")));})},
            {Item.Thou,(Resources.Load<Sprite>($"Ability_Skill_Icons/UI_Skill_Icon_Fly"),car=>{GameManager.Instance.Gold = 10000000; StartCoroutine(ItemCheck(($"GetGoldVeryMany")));})},
            {Item.ToShop,(Resources.Load<Sprite>($"Ability_Skill_Icons/UI_Skill_Icon_Heal"),car=>{GameManager.Instance.SwitchCamera(); Time.timeScale = 0; })}
        };
    }

    private IEnumerator ItemCheck(string itemName)
    {
        itemCheck.GetComponentInChildren<Text>().text = itemName;  
        itemCheck.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.4f);
        itemCheck.gameObject.SetActive(false);
    }
}
