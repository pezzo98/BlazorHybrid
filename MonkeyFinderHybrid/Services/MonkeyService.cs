using System.Net.Http.Json;

namespace MonkeyFinderHybrid.Services;

public class MonkeyService
{
    private readonly HttpClient httpClient;
    private List<Monkey> monkeysList = [];

    public MonkeyService()
    {
        httpClient = new HttpClient();
    }

    public async Task<List<Monkey>> GetMonkeys()
    {
        if (monkeysList.Count > 0)
        {
            return monkeysList;
        }

        var response = await httpClient.GetAsync("https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/MonkeysApp/monkeydata.json");
        if (response.IsSuccessStatusCode)
        {
            var monkeyResult = await response.Content.ReadFromJsonAsync(MonkeyContext.Default.ListMonkey);
            if (monkeyResult is not null)
            {
                monkeysList = monkeyResult;
            }
        }

        return monkeysList;
    }

    public List<Monkey> AddMonkey(Monkey monkey)
    {
        monkeysList.Add(monkey);
        return monkeysList;
    }
}