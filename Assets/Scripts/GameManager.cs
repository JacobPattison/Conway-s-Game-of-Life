using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool IsPlaying = false;
    void Start()
    {
        StartCoroutine(UpdateStates(3f));
    }

    private void Playing()
    {
        IsPlaying = Input.GetKeyDown(KeyCode.Space); // Example: wait for the space key to be pressed
    }

    IEnumerator UpdateStates(float interval)
    {
        while (!IsPlaying)
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
