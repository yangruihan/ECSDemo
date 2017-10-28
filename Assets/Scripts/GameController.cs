using UnityEngine;
using Entitas;

public class GameController : MonoBehaviour
{
    public GameObject PlayerSprite;

    private Systems _systems;

    private void Start()
    {
        Contexts contexts = Contexts.sharedInstance;

        // 创建系统
        _systems = CreateSystems(contexts);

        // 创建我们的玩家实体
        var player = contexts.game.CreateEntity();
        // 为其添加相应的组件
        player.isPlayer = true;
        player.AddPosition(Vector2.zero);
        player.AddVelocity(Vector2.zero);
        player.AddSprite(Instantiate(PlayerSprite, player.position.Value, Quaternion.identity));

        // 初始化系统
        _systems.Initialize();
    }

    private void Update()
    {
        _systems.Execute();
        _systems.Cleanup();
    }

    private void OnDestroy()
    {
        _systems.TearDown();
    }

    private Systems CreateSystems(Contexts contexts)
    {
        // Feature 是 Entitas 框架提供的在 Editor 下进行调试的类
        return new Feature("Game")
            .Add(new ChangePlayerVelocitySystem())
            .Add(new ChangePositionSystem(contexts))
            .Add(new RenderSystem(contexts));
    }
}
