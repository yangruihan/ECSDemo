using Entitas;
using UnityEngine;

public class ChangePlayerVelocitySystem : IExecuteSystem
{
    // 每一帧都会执行
    public void Execute()
    {
        // 得到拥有 Player、Position、Velocity 组件的实体集合
        var playerCollection = Contexts.sharedInstance.game.GetGroup(
            GameMatcher.AllOf(
                GameMatcher.Player,
                GameMatcher.Position,
                GameMatcher.Velocity));

        var velocity = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            velocity.y += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            velocity.y -= 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            velocity.x -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            velocity.x += 1;
        }

        foreach (var player in playerCollection)
        {
            player.ReplaceVelocity(velocity);
        }
    }
}
