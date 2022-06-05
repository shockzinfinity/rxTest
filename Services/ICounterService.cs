namespace rxTest4.Services
{
  public interface ICounterService
  {
    int Count { get; set; }

    void Increment();
  }
}