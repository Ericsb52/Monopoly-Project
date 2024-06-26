using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public List<Node> route = new List<Node>();

    private void OnValidate()
    {
        route.Clear();
        foreach(Transform node in transform.GetComponentInChildren<Transform>())
        {
            route.Add(node.GetComponent<Node>());
        }
    }

    // This method is called by Unity to allow you to draw Gizmos in the scene view.
    private void OnDrawGizmos()
    {
        /*This method uses Unity's Gizmos class to draw lines between nodes in the scene view, 
         * which helps visualize the route. The loop iterates through the nodes, drawing 
         * a green line from each node to the next. If the node is the last in the list, 
         * it draws a line to itself, effectively not drawing a new segment.*/
        // Check if the route has more than one node.
        if (route.Count > 1)
        {
            // Loop through each node in the route.
            for (int i = 0; i < route.Count; i++)
            {
                // Get the position of the current node.
                Vector3 current = route[i].transform.position;

                // Get the position of the next node if it exists, otherwise use the current node's position.
                Vector3 next = (i + 1 < route.Count) ? route[i + 1].transform.position : current;

                // Set the color of the Gizmos to green.
                Gizmos.color = Color.green;

                // Draw a line between the current node and the next node.
                Gizmos.DrawLine(current, next);
            }
        }
    }









}
