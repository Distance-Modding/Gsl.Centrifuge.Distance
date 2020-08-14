using Centrifuge.Distance;

public static class UIRectExtensions
{
    public static void DisplayAnchors(this UIRect rect)
    {
        GameAPI.Instance.Logger.Warning($"Displaying anchors for {rect.name} ({rect.GetType().Name})");
        GameAPI.Instance.Logger.Info($"Top    : {rect.topAnchor?.target?.name} {rect.topAnchor?.absolute} absolute {rect.topAnchor?.relative} relative");
        GameAPI.Instance.Logger.Info($"Bottom : {rect.bottomAnchor?.target?.name} {rect.bottomAnchor?.absolute} absolute {rect.bottomAnchor?.relative} relative");
        GameAPI.Instance.Logger.Info($"Left   : {rect.leftAnchor?.target?.name} {rect.leftAnchor?.absolute} absolute {rect.leftAnchor?.relative} relative");
        GameAPI.Instance.Logger.Info($"Right  : {rect.rightAnchor?.target?.name} {rect.rightAnchor?.absolute} absolute {rect.rightAnchor?.relative} relative");

        if (rect is UIWidget)
        {
            UIWidget widget = rect as UIWidget;

            GameAPI.Instance.Logger.Info($"Width  : {widget.width}");
            GameAPI.Instance.Logger.Info($"Height : {widget.height}");
        }
    }
}
