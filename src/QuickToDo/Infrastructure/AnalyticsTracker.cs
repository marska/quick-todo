using System;
using System.Globalization;
using System.Threading.Tasks;
using GoogleAnalyticsTracker.Simple;
using QuickToDo.Helpers;

namespace QuickToDo.Infrastructure
{
  public class AnalyticsTracker : IAnalyticsTracker, IDisposable
  {
    private const string TrackingAccount = "UA-51548375-2";

    private const string TrackingDomain = "marska.github.io";

    private readonly SimpleTracker _tracker;

    public AnalyticsTracker(ISettingsService settingsService)
    {
      if (settingsService == null)
      {
        throw new ArgumentNullException(nameof(settingsService));
      }

      _tracker = new SimpleTracker(TrackingAccount, TrackingDomain);

      var userId = settingsService.GetSettings().UserId;
      
      _tracker.UserAgent = userId;
      
      _tracker.Language = CultureInfo.CurrentCulture.Name;
    }

    public async Task TrackPageViewAsync(ViewTitle viewTitle)
    {
      await _tracker.TrackPageViewAsync(viewTitle.ToString(), "View" + viewTitle);
    }

    public async Task TrackEventAsync(ViewTitle viewTitle, string eventName)
    {
      await _tracker.TrackEventAsync(viewTitle.ToString(), eventName);
    }

    public void Dispose()
    {
      _tracker.Dispose();
    }
  }
}