using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public int Id;
    public int Price;
    public bool Purchased
    {
        get
        {
            if (ItemType == ShopItemType.CharacterSkin) return Config.Data.Progress.CharacterSkinOwned.TryGetValue(Id, out var skin) && skin;
            else if (ItemType == ShopItemType.BackgroundSkin) return Config.Data.Progress.BackgroundSkinOwned.TryGetValue(Id, out var skin) && skin;
            return false;
        }
    }
    public bool Selected
    {
        get
        {
            if (ItemType == ShopItemType.CharacterSkin) return Config.Data.Progress.SelectedCharacterSkin == Id;
            else if (ItemType == ShopItemType.BackgroundSkin) return Config.Data.Progress.SelectedBackgroundSkin == Id;
            return false;
        }
    }

    public ShopItemType ItemType;

    public enum ShopItemType
    {
        CharacterSkin,
        BackgroundSkin,
    }

    private GameObject _selected;
    private GameObject _gem;
    private TextMeshProUGUI _priceText;
    private Button _button;

    void Start()
    {
        _selected = this.transform.Find("Badge").gameObject;
        _gem = this.transform.Find("Group_Cost/Gem").gameObject;
        _priceText = this.transform.Find("Group_Cost/Text_Cost").GetComponent<TextMeshProUGUI>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Select);
    }

    private void Update()
    {
        _selected.SetActive(Selected);
        _gem.SetActive(Price != 0 && !Purchased);
        _priceText.text = Purchased ? "PURCHASED" : (Price == 0 ? "FREE" : Price.GroupInt());
    }

    public void Select()
    {
        if (Purchased)
        {
            if (ItemType == ShopItemType.CharacterSkin) Config.Data.Progress.SelectedCharacterSkin = Id;
            else if (ItemType == ShopItemType.BackgroundSkin) Config.Data.Progress.SelectedBackgroundSkin = Id;
            Config.SaveConfig();
            return;
        }
        if (Config.Data.Progress.Coins < Price) return;
        Config.Data.Progress.Coins -= Price;

        if (ItemType == ShopItemType.CharacterSkin) Config.Data.Progress.CharacterSkinOwned[Id] = true;
        else if (ItemType == ShopItemType.BackgroundSkin) Config.Data.Progress.BackgroundSkinOwned[Id] = true;
        Config.SaveConfig();
    }
}
