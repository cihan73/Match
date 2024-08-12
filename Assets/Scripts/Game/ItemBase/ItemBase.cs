using UnityEngine;
using Zenject;

public class ItemBase : MonoBehaviour
{
    public class Factory : PlaceholderFactory<ItemBase> { }
}
