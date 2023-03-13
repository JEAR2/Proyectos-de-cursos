namespace EntryPoints.ServiceBus.Base.Gateway;

/// <summary>
/// ISubscription
/// </summary>
public interface ISubscription
{
    /// <summary>
    /// Subscribe
    /// </summary>
    /// <returns></returns>
    Task SubscribeAsync();
}