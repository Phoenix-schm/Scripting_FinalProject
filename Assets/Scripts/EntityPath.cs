using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Just the list of waypoints for an entity to run through
/// </summary>
public class EntityPath : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
    [SerializeField] private Color debugColor = Color.blue;
    [SerializeField] private bool drawNumbers;
    [SerializeField] private bool drawAsLoops;
    [SerializeField] private bool alwaysDrawPathOnDeselect;

    public void OnDrawGizmos()
    {
        if (alwaysDrawPathOnDeselect)
        {
            DrawPath();
        }
    }

    /// <summary>
    /// Unrelated to gameplay. Draws a line between waypoints[] in editor and numbers them
    /// </summary>
    public void DrawPath()
    {
        for (int i = 0; i < waypoints.Count; i++)
        {
            GUIStyle labelStyle = new GUIStyle();
            labelStyle.fontSize = 20;
            labelStyle.normal.textColor = debugColor;

            if (drawNumbers)
            {
                Handles.Label(waypoints[i].position, i.ToString(), labelStyle);
            }

            // draw lines between points

            if (i >= 1)
            {
                Gizmos.color = debugColor;
                Gizmos.DrawLine(waypoints[i - 1].position, waypoints[i].position);

                if (drawAsLoops)
                {
                    Gizmos.DrawLine(waypoints[waypoints.Count - 1].position, waypoints[0].position);
                }
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        if (alwaysDrawPathOnDeselect)
        {
            return;
        }
        else
        {
            DrawPath();
        }
    }
}
