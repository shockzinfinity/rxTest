using rxTest4.Model;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace rxTest4.Services
{
  [Injectable(typeof(IMessageService))]
  public class MessageService : IMessageService
  {
    private readonly Subject<ActionMessage> _subject = new Subject<ActionMessage>();

    public IObservable<ActionMessage> MessageStream()
    {
      return _subject.AsObservable();
    }

    public void SendMessage(ActionMessage message)
    {
      _subject.OnNext(message);
    }

    public IDisposable Subscribe(Action<ActionMessage> onNext)
    {
      return MessageStream().Subscribe(Observer.Create(onNext));
    }
  }
}