using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(UpdateStates(1f));
    }

    private bool Playing()
    {
        // Your condition logic here
        return Input.GetKeyDown(KeyCode.Space); // Example: wait for the space key to be pressed
    }

    IEnumerator UpdateStates(float interval)
    {
        while (!Playing())
        {
            Debug.Log("State Updated");

            UpdateStates();
            CheckNextStates();

            yield return new WaitForSeconds(interval);
        }
    }

    private void CheckNextStates()
    {
        Cell[] cells = FindObjectsOfType<Cell>();

        foreach (Cell cell in cells)
        {
            cell.CheckNextState();
        }
    }

    private void UpdateStates()
    {
        Cell[] cells = FindObjectsOfType<Cell>();

        foreach (Cell cell in cells)
        {
            cell.UpdateState();
        }
    }
}
