
using Game.ItemBase;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameplayInstaller", menuName = "Installers/GameplayInstaller")]
public class GameplayInstaller : ScriptableObjectInstaller<GameplayInstaller>
{
    [Header("Prefabs")]
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private ItemBase itemBasePrefab;
    [Space, Header("Scriptable Objects")]
    [SerializeField] private ItemStatsSO itemStatsSO;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.Bind<Board>().FromComponentInHierarchy()
            .AsSingle();

        Container.Bind<Borders>().FromComponentInHierarchy()
            .AsSingle();

        Container.Bind<ItemFactory>().FromComponentInHierarchy()
            .AsSingle();

        Container.Bind<ImageLibService>().FromComponentInHierarchy()
            .AsSingle();

        Container.BindFactory<Cell, Cell.CellFactory>()
            .FromComponentInNewPrefab(cellPrefab)
            .AsSingle();

        Container.BindFactory<ItemBase, ItemBase.Factory>()
            .FromComponentInNewPrefab(itemBasePrefab)
            .AsSingle();

        Container.Bind<ItemStatsSO>()
            .FromInstance(itemStatsSO)
            .AsSingle();

        Container.DeclareSignal<OnElementTappedSignal>();
        Container.DeclareSignal<OnEmptyTappedSignal>();
    }
}
