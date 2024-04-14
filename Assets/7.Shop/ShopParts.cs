using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum Parts { DesertWheel = 1000000, MountainWheel = 2000000, CityWheel = 3000000, SixEngine = 3500000, EightEngine = 4000000, None }
public class ShopParts : MonoBehaviour
{
    private Parts buyItem;
    public static bool checkCheat = false;
    [SerializeField] private Button[] ItemList = new Button[5];
    [SerializeField] private Text partExpl;
    [SerializeField] private Button buy;
    [SerializeField] private Text buyText;
    [SerializeField] private Button exit;
    private Dictionary<Parts, (string, Action)> PartDict;
    [SerializeField] private List<Parts> boughtItem = new List<Parts>();
    public List<Image> boughtItemImg = new List<Image>();
    private void Start()
    {

        ItemSet();
        foreach (var item in ItemList)
        {
            item.TryGetComponent(out CarPart part);
            item.onClick.AddListener(() =>
            {
                buyItem = part.Part; ItemFind(part.Part); if (boughtItem.Contains(part.Part)) { buyText.text = $"SoldOut"; }
                else if (!boughtItem.Contains(part.Part)) { buyText.text = $"Buy"; }
            });
        }
        buy.onClick.AddListener(() => { BuyItem(); });
        exit.onClick.AddListener(() => { StartCoroutine(Dele.Instance.ShopAni()); });
    }

    private void ItemSet()
    {
        PartDict = new Dictionary<Parts, (string, Action)>()
        {
            {Parts.DesertWheel,($"DesertWheel",()=>{Dele.Instance.Shop(Parts.DesertWheel,Color.red); })},
            {Parts.MountainWheel,($"MountainWheel",()=>{Dele.Instance.Shop(Parts.MountainWheel,Color.green); })},
            {Parts.CityWheel,($"CityWheel",()=>{Dele.Instance.Shop(Parts.CityWheel,Color.blue); })},
            {Parts.SixEngine,($"SixEngine",()=>{Dele.Instance.Shop(Parts.SixEngine,Color.black); })},
            {Parts.EightEngine,($"EightEngine",()=>{Dele.Instance.Shop(Parts.EightEngine,Color.white); })}
        };
    }

    private void ItemFind(Parts parts)
    {
        foreach (var part in PartDict)
        {
            if (part.Key.Equals(parts))
            {
                partExpl.text = part.Value.Item1;
                part.Value.Item2();
            }
        }
    }

    private void BuyItem()
    {
        int itemPrice = (int)buyItem;
        Action buyFunc = () =>
        {
            GameManager.Instance.Gold -= itemPrice;
            Dele.Instance.PartsApply(Dele.Instance.PartsRead());
            boughtItem.Add(buyItem);
            buyText.text = $"SoldOut";
            AddPanel(buyItem);
        };
        if (boughtItem.Contains(buyItem))
        {
            Dele.Instance.PartsApply(Dele.Instance.PartsRead());
        }
        else if (itemPrice <= GameManager.Instance.Gold)
        {
            buyFunc();
        }
    }

    private void AddPanel(Parts parts)
    {
        foreach (var item in ItemList)
        {
            item.TryGetComponent(out CarPart part);
            if (part.Part.Equals(parts))
            {
                for (int i = 0; i < boughtItemImg.Count; i++)
                {
                    if (ReferenceEquals(boughtItemImg[i].sprite, null))
                    {
                        item.transform.GetChild(0).TryGetComponent(out Image image);
                        boughtItemImg[i].sprite = image.sprite;
                        boughtItemImg[i].color = image.color;
                        break;
                    }
                }
            }
        }
    }
}
