using System.Net.Http.Json;

namespace MonkeyFinderHybrid.Services;

public class MonkeyService(HttpClient httpClient)
{
    List<Monkey> monkeyList = [];

    public async Task<List<Monkey>> GetMonkeys()
    {
        if (monkeyList.Count > 0)
        {
            return monkeyList;
        }

        var response = await httpClient.GetAsync("https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/MonkeysApp/monkeydata.json");
        if (response.IsSuccessStatusCode)
        {
            var resultMonkeys = await response.Content.ReadFromJsonAsync(MonkeyContext.Default.ListMonkey);

            if (resultMonkeys is not null)
            {
                monkeyList = resultMonkeys;
            }
        }

        return monkeyList;
    }

    public List<Monkey> AddMonkey(Monkey monkey)
    {
        monkeyList.Add(monkey);
        return monkeyList;
    }

    public Monkey FindMonkeyByName(string name)
    {
        var monkey = monkeyList.FirstOrDefault(m => m.Name == name);

        if (monkey is null)
        {
            throw new Exception("Monkey not found");
        }

        return monkey;
    }
}