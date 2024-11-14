using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using MonkeyFinderHybrid.Components.Controls;

namespace MonkeyFinderHybrid.Components.Pages;

public partial class Home(MonkeyService monkeyService, IDialogService dialogService) : ComponentBase
{
    private List<Monkey> monkeys = [];
    private Monkey DialogData { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        monkeys = await monkeyService.GetMonkeys();
    }

    private async Task AddMonkey()
    {
        var data = new Monkey();
        var dialog = await dialogService.ShowDialogAsync<SimpleCustomizedDialog>(data, new DialogParameters()
        {
            Title = $"Add a New Monkey",
            PreventDismissOnOverlayClick = true,
            PreventScroll = true
        });

        var result = await dialog.Result;
        if (!result.Cancelled && result.Data is not null)
        {
            DialogData = (Monkey)result.Data;
            monkeyService.AddMonkey(DialogData);
            monkeys = await monkeyService.GetMonkeys();
        }
    }
}
