using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Cell : MonoBehaviour
{
    private string DebugPath = Application.dataPath + "/Debug.txt";

    [SerializeField] private SpriteRenderer SpriteRenderer;
    public bool State;
    public bool ?NextState;
    public int xPos, yPos;

    private void Start()
    {
        State = false;
    }

    private void OnMouseDown()
    {
        ToggleState(State);   
    }

    private void ToggleState(bool? state)
    {
        if (state.HasValue && state.Value)
        {
            SpriteRenderer.color = Color.black;
            State = false;
        }
        else
        {
            SpriteRenderer.color = Color.white;
            State = true;
        }
    }

    public void CheckNextState()
    {
        int aliveCount = GetNeighobourAliveCount();

        if (aliveCount < 2 || aliveCount > 3)
            NextState = false;
        else
            NextState = true;

        if (aliveCount == 3)
            NextState = true;


    }

    private int GetNeighobourAliveCount ()
    {
        int count = 0;
        List<Cell> neighbours = new List<Cell>();

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;
                GameObject cellGameObject = GameObject.Find($"{xPos + dx},{yPos + dy}");
                if (cellGameObject != null)
                {
                    if (cellGameObject.GetComponent<Cell>().State == true)
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }

    public void UpdateState()
    {
        // Only toggle state if NextState has a value
        if (NextState.HasValue)
        {
            ToggleState(NextState.Value);
        }
    }
}
