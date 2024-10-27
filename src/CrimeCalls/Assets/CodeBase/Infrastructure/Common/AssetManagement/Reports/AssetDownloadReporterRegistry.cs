using System.Collections.Generic;

namespace CodeBase.Infrastructure.Common.AssetManagement.Reports
{
    public class AssetDownloadReporterRegistry
    {
        private readonly Dictionary<string, AssetDownloadReporter> _reporters;

        public AssetDownloadReporterRegistry() => 
            _reporters = new Dictionary<string, AssetDownloadReporter>();

        public AssetDownloadReporter this[string key] => CreateOrGet(key);

        public void UpdateDownloadSize(string key, long downloadSize)
        {
            var reporter = _reporters[key];
            reporter.DownloadSize = downloadSize;
        }

        public AssetDownloadReporter CreateOrGet(string key)
        {
            _reporters.TryAdd(key, new AssetDownloadReporter());
            return _reporters[key];
        }
    }
}