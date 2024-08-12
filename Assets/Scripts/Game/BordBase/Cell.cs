using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class Cell : MonoBehaviour, ITouchable
{
    [SerializeField] private TextMeshPro labelText;

    [Inject] private Board _board;
    
    public bool IsFillingCell { get; private set; }
    public int X { get; private set; }
    public int Y { get; private set; }
    public List<Cell> Neighbors { get; private set; } = new();

    public Item Item
    {
        get => _item;
        set
        {
            if (_item == value) return;
            
            var oldItem = _item;
            _item = value;

            if (oldItem != null && Equals(oldItem.Cell, this))
            {
                oldItem.Cell = null;
            }

            if (value != null)
            {
                value.Cell = this;
            }
        }
    }
    private Item _item;
    
    public void Prepare(int x, int y)
    {
        X = x;
        Y = y;

        IsFillingCell = Y == _board.Cols - 1;
        transform.localPosition = new Vector3(x, y);
        
        SetLabel();
        UpdateNeighbors();
    }

    public bool HasItem()
    {
        return Item != null;
    }
    
    public bool IsFalling()
    {
        //todo: update here
        return false;
    }
    
    private void UpdateNeighbors()
    {
        var up = _board.GetNeighbourWithDirection(this, Direction.Up);
        var down = _board.GetNeighbourWithDirection(this, Direction.Down);
        var left = _board.GetNeighbourWithDirection(this, Direction.Left);
        var right = _board.GetNeighbourWithDirection(this, Direction.Right);
        
        if(up != null) Neighbors.Add(up);
        if(down != null) Neighbors.Add(down);
        if(left != null) Neighbors.Add(left);
        if(right != null) Neighbors.Add(right);
    }

    private void SetLabel()
    {
        var cellName = $"{X} : {Y}";
        labelText.text = cellName;
        gameObject.name = $"Cell {cellName}";
    }
    
    public class CellFactory : PlaceholderFactory<Cell> { }
}
