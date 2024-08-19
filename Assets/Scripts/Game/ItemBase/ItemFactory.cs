using System;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

public class ItemFactory : MonoBehaviour
{
    [Inject] private ItemBase.Factory _itemBaseFactory;
    [Inject] private DiContainer _diContainer;

    public Item Create(ItemType itemType, int layerCount = 2, ItemType itemTypeCliked = ItemType.None)
    {
        Assert.IsNotNull(_itemBaseFactory, "_itemBaseFactory is null");
        
        var itemBase = _itemBaseFactory.Create();
        Item item = null;
        switch (itemType)
        {
            case ItemType.None:
                break;
            case ItemType.GreenCube:
                item = CreateCubeItem(itemBase, MatchType.Green, itemType);
                break;
            case ItemType.YellowCube:
                item = CreateCubeItem(itemBase, MatchType.Yellow, itemType);
                break;
            case ItemType.BlueCube:
                item = CreateCubeItem(itemBase, MatchType.Blue, itemType);
                break;
            case ItemType.RedCube:
                item = CreateCubeItem(itemBase, MatchType.Red, itemType);
                break;
            case ItemType.PinkCube:
                item = CreateCubeItem(itemBase, MatchType.Pink, itemType);
                break;
            case ItemType.PurpleCube:
                item = CreateCubeItem(itemBase, MatchType.Purple, itemType);
                break;
            case ItemType.Balloon:
                break;
            case ItemType.GreenBalloon:
                break;
            case ItemType.YellowBalloon:
                break;
            case ItemType.BlueBalloon:
                break;
            case ItemType.RedBalloon:
                break;
            case ItemType.PinkBalloon:
                break;
            case ItemType.PurpleBalloon:
                break;
            case ItemType.Crate:
                break;
            case ItemType.Bomb:
                break;
            case ItemType.VerticalRocket:
                break;
            case ItemType.HorizontalRocket:
                break;
            case ItemType.Disco:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(itemType), itemType, null);
        }

        return item;
    }

    private Item CreateCubeItem(ItemBase itemBase, MatchType matchType, ItemType itemType)
    {
        var cubeItem = itemBase.gameObject.AddComponent<CubeItem>();
        _diContainer.Inject(cubeItem);
        cubeItem.Prepare(itemBase, matchType, itemType);

        return cubeItem;
    }
}
