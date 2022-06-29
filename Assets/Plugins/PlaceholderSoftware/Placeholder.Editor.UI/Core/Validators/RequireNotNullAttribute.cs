namespace Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Core.Validators
{
    /// <summary>
    /// Mark a property as required to be non-null. If null, it will be highlighted in the inspector.
    /// </summary>
    public class RequiredNotNullAttribute
        : BaseValidationAttribute
    {
        public Highlight Error { get; }
        public Highlight OkColor { get; }

        public RequiredNotNullAttribute(Highlight err = Highlight.Error, Highlight ok = Highlight.None)
        {
            Error = err;
            OkColor = ok;
        }
    }
}