using rxTest4.Model;

namespace rxTest4.Services
{
  public interface IMessageService
  {
    void SendMessage(ActionMessage message);

    IObservable<ActionMessage> MessageStream();

    IDisposable Subscribe(Action<ActionMessage> onNext);
  }
}