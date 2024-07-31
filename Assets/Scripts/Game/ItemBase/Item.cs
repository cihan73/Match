using UnityEngine;

public class Item : MonoBehaviour
{
    public Cell Cell
    {
        get => _cell;
        set
        {
            if (_cell == value) return;

            var oldCell = _cell;
            _cell = value;

            if (oldCell != null && Equals(oldCell.Item, this))
            {
                oldCell.Item = null;
            }

            if (value == null) return;

            value.Item = this;
            gameObject.name = _cell.gameObject.name + " " + GetType().Name;
        }
    }
    private Cell _cell;

}
