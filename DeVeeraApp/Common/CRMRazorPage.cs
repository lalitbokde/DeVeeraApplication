using CRM.Services.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace DeVeeraApp.Common
{
    public abstract class CRMRazorPage<TModel> : Microsoft.AspNetCore.Mvc.Razor.RazorPage<TModel>
    {
        private ILocalizationService _localizationService;
        private Localizer _localizer;

        public Localizer T
        {
            get
            {


                if (_localizationService == null)
                    _localizationService = Context.RequestServices.GetRequiredService<ILocalizationService>();

                if (_localizer == null)
                {
                    _localizer = (format, args) =>
                    {
                        var resFormat = _localizationService.GetResource(format);
                        if (string.IsNullOrEmpty(resFormat))
                        {
                            return new LocalizedString(format);
                        }
                        return new LocalizedString((args == null || args.Length == 0)
                            ? resFormat
                            : string.Format(resFormat, args));
                    };
                }
                return _localizer;
            }
        }

    }
}
