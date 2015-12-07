using UnityEngine;
using System.Collections;

public class VisualTile : MonoBehaviour
{
    public delegate void OnClickedDelegate(int x, int y);
    public OnClickedDelegate OnClicked;

    public int x;
    public int y;


    void OnMouseDown()
    {
        if (OnClicked != null)
        {
            OnClicked(x, y);
        }
    }
}
