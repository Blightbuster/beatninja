using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public void AddCoins(int amount)
    {
        Config.Data.Progress.Coins += amount;
    }
}
