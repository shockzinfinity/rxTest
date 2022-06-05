using System.Reactive.Linq;

namespace rxTest4.Model
{
  public class ActionMessage
  { }

  public static class ExtensionMethods
  {
    public static IObservable<string> AsStringMessage(this ActionMessage actionMessage) =>
      actionMessage is StringMessage sm ? Observable.Return(sm.Message) : Observable.Empty<string>();
  }

  public class StringMessage : ActionMessage
  {
    public readonly string Message;

    public StringMessage(string msg) => Message = msg;
  }
}