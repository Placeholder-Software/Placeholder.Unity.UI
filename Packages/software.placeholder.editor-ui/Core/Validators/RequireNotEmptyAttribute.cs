#nullable enable

namespace Placeholder.Editor.UI.Core.Validators
{
    /// <summary>
    /// Mark a property as required to be non-empty (strings/arrays)
    /// </summary>
    public class RequireNotEmptyAttribute
        : BaseValidationAttribute
    {
        public Highlight Error { get; }
        public Highlight OkColor { get; }

        public RequireNotEmptyAttribute(Highlight err = Highlight.Error, Highlight ok = Highlight.None)
        {
            Error = err;
            OkColor = ok;
        }
    }
}