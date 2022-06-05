using rxTest4.Model;
using System.Reactive.Linq;

namespace rxTest4.Components
{
  public partial class CounterComponent
  {
    private int count { get; set; } = 0;
    private IDisposable _subscriber;

    protected override void OnInitialized()
    {
      count = CounterService.Count;
      _subscriber = _subscriber ?? SubscribeForMessageUpdates(UpdateCount);
    }

    public void Dispose() => _subscriber?.Dispose();

    private void UpdateCount(string msg) => count = CounterService.Count;

    private IDisposable SubscribeForMessageUpdates(Action<string> updateContent) => MessageService
      .Subscribe(action => action.AsStringMessage()
        .Do(updateContent)
        .Do(_ => StateHasChanged())
        .Subscribe());
  }
}