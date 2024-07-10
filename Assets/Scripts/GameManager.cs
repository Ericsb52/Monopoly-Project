using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    Board gameBoard;
    [SerializeField]
    List<Player> playerList = new List<Player>();
    [SerializeField]
    int currentPlayerIndex = 0;
    [SerializeField]
    int maxTurnesInJail = 3;
    [SerializeField]
    int startMoney = 2000;
    [SerializeField] int passGoMoney = 200;

    [SerializeField]
    GameObject playerInfoPrefab;
    [SerializeField] Transform playerPanel;
    public List<string> aiNames = new List<string>();
    public List<Sprite> tokens = new List<Sprite>();






    private void Awake()
    {
        instance = this;
    }




    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            GameObject infoObj = Instantiate(playerInfoPrefab, playerPanel,false);
            PlayerInfo info = infoObj.GetComponent<PlayerInfo>();
            playerList[i].Init(gameBoard.route[0], startMoney, info);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
