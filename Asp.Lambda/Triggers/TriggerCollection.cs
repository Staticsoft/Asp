using System.Collections;

namespace Staticsoft.Asp.Lambda;

public class TriggerCollection(
    IEnumerable<TriggerSource> registrations
) : IEnumerable<TriggerSource>
{
    readonly IEnumerable<TriggerSource> Registrations = registrations;
    readonly Lazy<IList<TriggerSource>> Triggers = new(() => registrations.ToArray());

    public TriggerCollection AddTrigger<Trigger>()
        where Trigger : TriggerSource, new()
        => new(Registrations.Append(new Trigger()));

    public IEnumerator<TriggerSource> GetEnumerator()
        => Triggers.Value.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}
