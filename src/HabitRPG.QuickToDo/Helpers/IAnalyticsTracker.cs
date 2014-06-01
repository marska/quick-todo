using System.Threading.Tasks;

namespace HabitRPG.QuickToDo.Helpers
{
  public interface IAnalyticsTracker
  {
    Task TrackPageViewAsync(ViewTitle viewTitle);
    Task TrackEventAsync(ViewTitle viewTitle, string eventName);
  }
}