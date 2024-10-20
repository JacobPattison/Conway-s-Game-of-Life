using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int Width, Height;
    [SerializeField] private Cell CellPrefab;

    private void Awake()
    {
        this.gameObject.GetComponent<SpriteRenderer>().bounds = new Bounds(new Vector3(Width / 2, Height / 2), new Vector3(Width, Height));
    }

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                var spawnedCell = Instantiate(CellPrefab, new Vector3(x, y), Quaternion.identity);
                spawnedCell.name = $"{x},{y}";
                spawnedCell.xPos = x; spawnedCell.yPos = y;
            }
        }
    }
}
