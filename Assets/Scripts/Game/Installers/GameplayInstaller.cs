using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameplayInstaller", menuName = "Installers/GameplayInstaller")]
public class GameplayInstaller : ScriptableObjectInstaller<GameplayInstaller>
{
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private ItemBase itemBasePrefab;
    
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
        
        Container.DeclareSignal<OnElementTappedSignal>();
        Container.DeclareSignal<OnEmptyTappedSignal>();
    }
}