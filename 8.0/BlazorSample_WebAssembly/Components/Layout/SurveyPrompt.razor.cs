using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorSample.Components.Layout;

public partial class SurveyPrompt : 
    ComponentBase, IObserver<ElementReference>, IDisposable
{
    private IDisposable? subscription = null;

    [Parameter]
    public IObservable<ElementReference>? Parent { get; set; }

    [Inject]
    public IJSRuntime? JS { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        subscription?.Dispose();
        subscription = 
            Parent is not null ? Parent.Subscribe(this) : null;
    }

    public void OnCompleted()
    {
        subscription = null;
    }

    public void OnError(Exception error)
    {
        subscription = null;
    }

    public void OnNext(ElementReference value)
    {
        JS?.InvokeAsync<object>(
            "setElementClass", new object[] { value, "red" });
    }

    public void Dispose()
    {
        subscription?.Dispose();
    }
}