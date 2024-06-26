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
    public Board gameBoard; // Reference to the game board
    [SerializeField] internal new string name; // Name of the node
    [SerializeField] private NodeType type; // Type of the node
    public TextMeshProUGUI nameText; // UI element for the node's name
    public TextMeshProUGUI costText; // UI element for the node's cost
    public TextMeshProUGUI ownerText; // UI element for the node's owner
    private TextMeshProUGUI[] textfields; // Array of UI text elements
    public Image[] images; // Array of UI image elements
    public Color[] colors; // Array of colors for properties
    public Color propColor; // Color assigned to this property
    public Image colorField; // UI element for displaying the color
    public int route_pos; // Position of the node on the route

    // Method called when the script is loaded or a value is changed in the inspector
    private void OnValidate()
    {
        gameBoard = FindObjectOfType<Board>(); // Find and assign the game board
        for (int i = 0; i < gameBoard.route.Count; i++) // Loop through the route to find the position of this node
        {
            if (gameBoard.route[i] == this)
            {
                route_pos = i;
            }
        }

        name = gameObject.name; // Set the name of the node to the game object's name
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
            colorField = images[1]; // Assign the second image to colorField
            nameText.text = name; // Set the text of nameText to the node's name

            // Determine the property color based on the position in the route
            for (int i = 0; i < 8; i++)
            {
                if (route_pos > 5 * i && route_pos <= 5 * (i + 1))
                {
                    propColor = colors[i];
                    break;
                }
            }

            colorField.color = propColor; // Set the color of the colorField
        }

        // If the node is a utility or railroad, initialize its UI elements
        if (type == NodeType.Utility || type == NodeType.RailRoad)
        {
            nameText = textfields[0];
            costText = textfields[1];
            ownerText = textfields[2];
            nameText.text = name;
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
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialization code (if any) can go here
    }

    // Update is called once per frame
    void Update()
    {
        // Code to update the node each frame (if any) can go here
    }
}