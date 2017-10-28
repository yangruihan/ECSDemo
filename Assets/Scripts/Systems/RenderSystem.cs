using System.Collections.Generic;
using Entitas;

public class RenderSystem : ReactiveSystem<GameEntity>
{
    public RenderSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var pos = entity.position.Value;
            entity.sprite.Value.transform.position = pos;
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasSprite && entity.hasPosition;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.Sprite));
    }
}
