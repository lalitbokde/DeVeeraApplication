using Microsoft.AspNetCore.Html;

namespace CRM.Services.Localization
{
    public class LocalizedString : HtmlString
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="localized">Localized value</param>
        public LocalizedString(string localized) : base(localized)
        {
            Text = localized;
        }

        /// <summary>
        /// Text
        /// </summary>
        public string Text { get; }
    }
}
