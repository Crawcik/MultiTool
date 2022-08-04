using Flax.Build;

public class MultiTool : GameModule
{
    /// <inheritdoc />
    public override void Init()
    {
        base.Init();
        BuildNativeCode = false;
    }
}
