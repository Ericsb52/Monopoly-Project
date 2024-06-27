using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

// Enumeration for different types of nodes on the board
public enum NodeType
{
    Property,
    Utility,
    RailRoad,
    Tax,
    Chance,
    CommunityChest,
    Go,
    Jail,
    FreeParking,
    GoToJail
}

// Class representing a node on the board
public class Node : MonoBehaviour
{
    [Header("Board refrance")]
    public Board gameBoard; // Reference to the game board

    [Header("Node Type")]
    public NodeType type; // Type of the node

    [Header("Node Data")]
    public string name; // Name of the node
    public string ownerName;
    public Color propColor; // Color assigned to this property
    public int route_pos; // Position of the node on the route
    public int price;

    [Header("Rent")]
    public int currentRent;
    public int baseRent;
    public int[] rentWithHouses;

    [Header("Motgage")]
    public bool isMotgaged;
    public int mortgageValue;
    public Color baseColor;
    public Color mortgageColor;

    [Header("Node Components")]
    public TextMeshProUGUI[] textfields; // Array of UI text elements
    public Image[] images; // Array of UI image elements
    public Color[] colors; // Array of colors for properties
    public TextMeshProUGUI nameText; // UI element for the node's name
    public TextMeshProUGUI costText; // UI element for the node's cost
    public TextMeshProUGUI ownerText; // UI element for the node's owner
    public Image colorField; // UI element for displaying the color
    public Image ownerField;
    public Image background;
    
   


    // Method called when the script is loaded or a value is changed in the inspector
    private void OnValidate()
    {
        name = gameObject.name; // Set the name of the node to the game object's name

        gameBoard = FindObjectOfType<Board>(); // Find and assign the game board
        for (int i = 0; i < gameBoard.route.Count; i++) // Loop through the route to find the position of this node
        {
            if (gameBoard.route[i] == this)
            {
                route_pos = i;
            }
        }

        
        try
        {
            textfields = GetComponentsInChildren<TextMeshProUGUI>(); // Get all text fields in children
            images = GetComponentsInChildren<Image>(); // Get all images in children
        }
        catch
        {
            // Handle exceptions (if any)
        }

        // If the node is a property, initialize its UI elements
        if (type == NodeType.Property)
        {
            nameText = textfields[0]; // Assign the first text field to nameText
            costText = textfields[1]; // Assign the second text field to costText
            ownerText = textfields[2]; // Assign the third text field to ownerText
            background = images[0];
            colorField = images[1]; // Assign the second image to colorField
            ownerField = images[2];
           

            // Determine the property color based on the position in the route
            for (int i = 0; i < 8; i++)
            {
                if (route_pos > 5 * i && route_pos <= 5 * (i + 1))
                {
                    propColor = colors[i];
                    break;
                }
            }


            nameText.text = name; // Set the text of nameText to the node's name
            costText.text = "$ " + price;
            colorField.color = propColor; // Set the color of the colorField
            mortgageValue = (int)price/2;
        }

        // If the node is a utility or railroad, initialize its UI elements
        if (type == NodeType.Utility || type == NodeType.RailRoad)
        {
            nameText = textfields[0];
            costText = textfields[1];
            ownerText = textfields[2];
            background = images[0];
            ownerField = images[2];

            nameText.text = name;
            costText.text = "$ " + price;
            mortgageValue = (int)price / 2;
        }

        // If the node is a community chest or chance, initialize its UI element
        if (type == NodeType.CommunityChest || type == NodeType.Chance)
        {
            nameText = textfields[0];
            nameText.text = name;
        }

        // If the node is a tax, initialize its UI elements
        if (type == NodeType.Tax)
        {
            nameText = textfields[0];
            costText = textfields[1];
            nameText.text = name;
            costText.text = "Pay $ " + price;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialization code (if any) can go here
        if (isMotgaged)
        {
            MortgageProperty();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        // Code to update the node each frame (if any) can go here
    }


    // Mortgage Methods
    public int MortgageProperty()
    {
        isMotgaged = true;
        background.color = mortgageColor;
        return mortgageValue;
    }

    public void UnMortgageProp()
    {
        isMotgaged = true;
        background.color = baseColor;
    }


    public bool IsMortgaged()
    {
        return isMotgaged;
    }
    public int MortgageValue()
    {
        return mortgageValue;
    }

    // owner methods
    public void OnOwnerUpdate()
    {
        if (ownerField!= null)
        {
            if(ownerText.text!=null)
            {
                ownerField.gameObject.SetActive(true);
            }
            else
            {
                ownerField.gameObject.SetActive(false);
                ownerText.text = "";
            }
        }
    }
}