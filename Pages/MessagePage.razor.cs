using rxTest4.Model;
using System.Reactive.Linq;

namespace rxTest4.Pages
{
  public partial class MessagePage
  {
    private IDisposable _subscriber1;
    private IDisposable _subscriber2;

    private string Message1 { get; set; } = "None yet!";
    private string Message2 { get; set; } = "None yet!";

    protected override void OnInitialized()
    {
      _subscriber1 = _subscriber1 ?? SubscribeForMessageUpdates(UpdateMessage1);
      _subscriber2 = _subscriber2 ?? SubscribeForMessageUpdates(UpdateMessage2);
    }

    public void Unsubscribe1()
    {
      _subscriber1?.Dispose();
      _subscriber1 = null;
    }

    public void Unsubscribe2()
    {
      _subscriber2?.Dispose();
      _subscriber2 = null;
    }

    private void UpdateMessage1(string msg) => Message1 = msg;

    private void UpdateMessage2(string msg) => Message2 = msg;

    private void LogMessage(string message) => Console.WriteLine($"StringMessage.Message = {message}");

    private IDisposable SubscribeForMessageUpdates(Action<string> updateMessage) =>
      MessageService.Subscribe(action => action.AsStringMessage()
        .Do(updateMessage)
        .Do(_ => StateHasChanged())
        .Do(LogMessage)
        .Subscribe());

    public void Dispose()
    {
      Console.WriteLine("Disposing subscribers");
      _subscriber1?.Dispose();
      _subscriber2?.Dispose();
    }
  }
}