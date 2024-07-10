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
    [SerializeField]
    int money;
    public Node currentNode;
    [SerializeField]
    bool isInJail;
    [SerializeField]
    int numTurnsInJail;
    [SerializeField]
    GameObject myToken;
    public Sprite icon;
    [SerializeField]
    List<Node> myProps = new List<Node>();
    public PlayerInfo playerInfo;
    


    // AI Varialbles
    const int high = 500;
    const int low = 50;
    [Range(low,high)]
    int aiMoneySavity;




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
        playerInfo.updateInfo(name, money, icon);
        if (playerType!=PlayerType.HUMAN)
        {
            int tempindex = Random.Range(0, GameManager.instance.aiNames.Count);
            name = GameManager.instance.aiNames[tempindex];
            GameManager.instance.aiNames.RemoveAt(tempindex);
            tempindex = Random.Range(0, GameManager.instance.tokens.Count);
            icon = GameManager.instance.tokens[tempindex];
            GameManager.instance.tokens.RemoveAt(tempindex);
            aiMoneySavity = Random.Range(Player.low, Player.high);
        }
        
    }

}
