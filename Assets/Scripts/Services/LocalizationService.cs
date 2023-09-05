using Services.Interfaces;

namespace Services
{
    public class LocalizationService : ILocalizationService
    {
        public string CurrentLanguage { get; }
        
        public LocalizationService(string language)
        {
            // Service responsible for localization mechanics in game
            CurrentLanguage = language;
        }
    }
}