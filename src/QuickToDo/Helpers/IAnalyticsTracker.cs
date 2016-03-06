using System.Threading.Tasks;

namespace QuickToDo.Helpers
{
  public interface IAnalyticsTracker
  {
    Task TrackPageViewAsync(ViewTitle viewTitle);
    Task TrackEventAsync(ViewTitle viewTitle, string eventName);
  }
}