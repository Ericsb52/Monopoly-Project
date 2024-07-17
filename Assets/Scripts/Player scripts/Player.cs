using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player 
{

    public enum PlayerType
    {
        HUMAN,
        AI
    }
    
    
    public PlayerType playerType;
    public string name;
    
    public int money;
    public Node currentNode;

    
    public bool isInJail;
    
    public int numTurnsInJail;

    
    public GameObject myToken;
    public Sprite icon;
    
    public List<Node> myProps = new List<Node>();
    public PlayerInfo playerInfo;
    

    public int aiMoneySavity;




    // player Methods

    public bool IsInJail => isInJail; // returns true or false if in jail
    public GameObject MyToken => myToken; // returns a refrence to players token
    public Node CurrentNode=> currentNode; // returns the current nod token is on

    // sets up the player when it is instantiated
    public void Init(Node startNode,int money,PlayerInfo info)
    {
        currentNode = startNode;
        this.money = money;
        playerInfo = info;
        
        if (playerType!=PlayerType.HUMAN)
        {
            int tempindex = Random.Range(0, GameManager.instance.aiNames.Count);
            name = GameManager.instance.aiNames[tempindex];
            GameManager.instance.aiNames.RemoveAt(tempindex);
            tempindex = Random.Range(0, GameManager.instance.token_img.Count);
            icon = GameManager.instance.token_img[tempindex];
            GameManager.instance.token_img.RemoveAt(tempindex);
            myToken = GameManager.instance.tokens[tempindex];
            GameManager.instance.tokens.RemoveAt(tempindex);
            aiMoneySavity = Random.Range(50,500);
        }

        playerInfo.updateInfo(name, money, icon);

    }

    public void setToken(GameObject token)
    {
        myToken = token;
    }

}
