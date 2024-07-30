using UnityEngine;
using Zenject;

public class ItemBase : MonoBehaviour
{
    public class ItemBaseFactory : PlaceholderFactory<ItemBase> { }
}
