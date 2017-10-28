using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ChangePositionSystem : ReactiveSystem<GameEntity>
{
    public ChangePositionSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.Velocity));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPosition && entity.hasVelocity;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var velocity = entity.velocity.Value;
            var newPosition = entity.position.Value + velocity * Time.deltaTime;

            entity.ReplacePosition(newPosition);
        }
    }
}
