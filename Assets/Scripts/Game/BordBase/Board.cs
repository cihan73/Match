using UnityEngine;
using Zenject;

public class Board : MonoBehaviour
{
    [SerializeField] private Transform cellParent;
    
    [Inject] private Cell.CellFactory _cellFactory;
    
    public int Rows { get; private set; }
    public int Cols { get; private set; }
    public Cell[,] Cells { get; private set; }

    public void Prepare(int row, int col)
    {
        Rows = row;
        Cols = col;
        Cells = new Cell[Rows, Cols];

        CreateCells();
        PrepareCells();
    }

    private void CreateCells()
    {
        for (int x = 0; x < Rows; x++)
        {
            for (int y = 0; y < Cols; y++)
            {
                var cell = _cellFactory.Create();
                cell.transform.SetParent(cellParent);
                Cells[x, y] = cell;
            }
        }
    }
    
    private void PrepareCells()
    {
        for (int x = 0; x < Rows; x++)
        {
            for (int y = 0; y < Cols; y++)
            {
                Cells[x, y].Prepare(x, y);
            }
        } 
    }
}
