using Flax.Build;

public class MultiToolEditorTarget : GameProjectEditorTarget
{
    /// <inheritdoc />
    public override void Init()
    {
        base.Init();
        Modules.Add("MultiTool");
    }
}
