using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class SpawnObjects : MonoBehaviour
{

    public static EntityManager manager;
    [SerializeField] GameObject _ballPrefab;
    int _noBalls = 5000;
    BlobAssetStore store;
    void Start()
    {
        store = new BlobAssetStore();
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, store);
        Entity ball = GameObjectConversionUtility.ConvertGameObjectHierarchy(_ballPrefab,settings);

        for (int i = 0; i < _noBalls; i++){
            var instance = manager.Instantiate(ball);
            float x = UnityEngine.Random.Range(-50,50);
            float z = UnityEngine.Random.Range(-50,50);
            float3 position = new float3(x,1,z);
            manager.SetComponentData(instance, new Translation {Value = position});
        }
    }

    void OnDestroy(){
        store.Dispose();
    }
}
