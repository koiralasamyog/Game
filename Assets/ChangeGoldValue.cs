using System;
using UnityEngine;
using TMPro;

public class ChangeGoldValue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI myText;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private DragonBoss dragonBoss;

    private void Start()
    {
        myText.text = "0";
    }

    public void AddGold(int amount)
    {
        int currentGold = int.Parse(myText.text);
        currentGold += amount;
        myText.text = currentGold.ToString();
    }
}
