using System.Threading.Tasks;
using QuickToDo.Helpers;

namespace QuickToDo.Infrastructure
{
  public interface IAnalyticsTracker
  {
    Task TrackPageViewAsync(ViewTitle viewTitle);
    
    Task TrackEventAsync(ViewTitle viewTitle, string eventName);
  }
}