using Microsoft.AspNetCore.Components;

namespace MonkeyFinderHybrid.Components.Pages;

public partial class Home(MonkeyService monkeyService) : ComponentBase
{
    private List<Monkey> monkeys = [];

    protected override async Task OnInitializedAsync()
    {
        monkeys = await monkeyService.GetMonkeys();
    }

    private void AddMonkey()
    {

    }
}
