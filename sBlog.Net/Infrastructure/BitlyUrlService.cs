using System;
using BitlyDotNET.Implementations;
using sBlog.Net.Domain.Interfaces;

namespace sBlog.Net.Infrastructure
{
    public static class BitlyUrlService
    {
        public static string GetBiltyPostUrl(ISettings settingsRepository, string postUrl)
        {
            return GetBitlyUrl(settingsRepository, postUrl, "{0}/{1}");
        }

        public static string GetBitlyPageUrl(ISettings settingsRepository, string pageUrl)
        {
            return GetBitlyUrl(settingsRepository, pageUrl, "{0}/pages/{1}");
        }

        private static string GetBitlyUrl(ISettings settingsRepository, string url, string urlFormat)
        {
            var bitlyUsername = ApplicationConfiguration.BitlyUserName;
            var bitlyApiKey = ApplicationConfiguration.BitlyApiKey;

            if (string.IsNullOrEmpty(bitlyUsername) || string.IsNullOrEmpty(bitlyApiKey))
            {
                return null;
            }

            try
            {
                var bitlyService = new BitlyService(bitlyUsername, bitlyApiKey);
                string shortUrl;
                bitlyService.Shorten(string.Format(urlFormat, settingsRepository.BlogAkismetUrl, url), out shortUrl);
                return shortUrl;
            }
            catch
            {
                return null;
            }
        }
    }
}
