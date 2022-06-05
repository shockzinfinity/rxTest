using rxTest4.Model;

namespace rxTest4.Services
{
  [Injectable(typeof(ICounterService))]
  public class CounterService : ICounterService
  {
    private readonly IMessageService _messageService;

    public CounterService(IMessageService messageService)
    {
      _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
    }

    public int Count { get; set; } = 0;

    public void Increment() => _messageService.SendMessage(new StringMessage($"CounterService.Increment.{++Count}"));
  }
}