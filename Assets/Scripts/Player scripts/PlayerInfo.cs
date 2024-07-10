using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerCashText;
    public Image icon;
    public Sprite img;
    public GameObject indecator;
    public bool myTurn =true;

    public void SetName(string name)
    {
        playerNameText.text = name;
    }

    public void SetCash(int cash) 
    { 
        playerCashText.text = "$ " +cash.ToString();
    }

    public void SetIcon(Sprite icon)
    { 
        img= icon;
        this.icon.sprite = img;
    }

    public void ToggleTurn()
    {
        myTurn = !myTurn;
        indecator.SetActive(myTurn);
    }

    public void updateInfo(string name,int cash,Sprite img)
    {
        SetName(name);
        SetCash(cash);
        SetIcon(img);
    }

    private void Start()
    {
        ToggleTurn();
    }


}
