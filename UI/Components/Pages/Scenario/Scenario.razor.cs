using Microsoft.AspNetCore.Components;
using Domain.Scenario;
using Domain.Enums;
using Domain.TaxParameters;

namespace UI.Components.Pages.Scenario;

public partial class Scenario
{
    
    [Inject]
    HttpClient _Http { get; set; } = null!;

    Random rnd = new Random();

    // Message
    private string ScenarioMessage { get; set; } = string.Empty;
    private List<ScenarioModel> Scenarios { get; set; } = new List<ScenarioModel>();
    
    private EconomicIndicators Indicators = new EconomicIndicators();

    private Company company = Company.Aster;
    private int scenarioYear = DateTime.Now.Year;
    private string scenarioName = string.Empty;
    private decimal economicIndicator = 0;
    private decimal welfarShare = 0;
    private decimal rafeOfIncrase = 0;
    private decimal totalRate => economicIndicator + welfarShare / 100 + rafeOfIncrase / 100;
    private decimal overtime_50;
    private decimal overtime_100;
    private decimal bonus;
    private decimal bonusMonth;
    private decimal shoppingVoucher;
    private decimal shoppingVoucherMonth;


    private async Task ScenarioAdd()
    {
        ScenarioModel newScenario = new ScenarioModel
        {
            
            Company = company,
            Year = scenarioYear,
            ScenarioName = scenarioName,
            EconomicIndicator = economicIndicator,
            WelfarShare = welfarShare / 100,
            RafeOfIncrase = rafeOfIncrase / 100,
            TotalRate = totalRate,
            Overtime_50 = overtime_50,
            Overtime_100 = overtime_100,
            Bonus = bonus,
            BonusMonth = bonusMonth,
            ShoppingVoucher = shoppingVoucher,
            ShoppingVoucherMonth = shoppingVoucherMonth
        };

        if(string.IsNullOrWhiteSpace(newScenario.ScenarioName))
        {
            ScenarioMessage = "Hata: Senaryo adı boş olamaz.";
            await Task.Delay(2000);
            ScenarioMessage = string.Empty;
            return;

            
        }

        var checkResponse = await _Http.GetAsync($"api/Scenario/GetByName/{newScenario.ScenarioName}");
        if (checkResponse.IsSuccessStatusCode)
        {
            ScenarioMessage = "Hata: Bu senaryo adı zaten mevcut.";
            await Task.Delay(2000);
            ScenarioMessage = string.Empty;
            return;
        }

        var result = await _Http.PostAsJsonAsync("api/Scenario", newScenario);

         if (result.IsSuccessStatusCode)
        {
            ScenarioMessage = "Senaryo başarıyla eklendi.";
            await LoadScenarios();
            ClearScenarioFields();

            StateHasChanged();

            await Task.Delay(2000);
            ScenarioMessage = string.Empty;

        }
        else
        {
            var error = await result.Content.ReadFromJsonAsync<ScenarioErrorMessage>();
            ScenarioMessage = "Hata: " + error?.Message;
        }



    }

    private async Task DeleteScenario()
    {
        ScenarioModel scenarioDelete = new ScenarioModel
        {   
            Company = company,
            Year = scenarioYear,
            ScenarioName = scenarioName,
            EconomicIndicator = economicIndicator,
            WelfarShare = welfarShare,
            RafeOfIncrase = rafeOfIncrase,
            TotalRate = totalRate,
            Overtime_50 = overtime_50,
            Overtime_100 = overtime_100,
            Bonus = bonus,
            BonusMonth = bonusMonth,
            ShoppingVoucher = shoppingVoucher,
            ShoppingVoucherMonth = shoppingVoucherMonth
        };

        var result = await _Http.GetFromJsonAsync<ScenarioModel>($"api/Scenario/GetByName/{scenarioName}");

        var deleteResult = await _Http.DeleteAsync($"api/Scenario/{result.Id}");

            if (deleteResult.IsSuccessStatusCode)
            {
                ScenarioMessage = "Senaryo başarıyla silindi.";
                await LoadScenarios();
                ClearScenarioFields();
    
                StateHasChanged();
    
                await Task.Delay(2000);
                ScenarioMessage = string.Empty;
    
            }
            else
            {
                var error = await deleteResult.Content.ReadFromJsonAsync<ScenarioErrorMessage>();
                ScenarioMessage = "Hata: " + error?.Message;
            }

    
    }

    private async Task DeleteAllScenarios()
    {
        var result = await _Http.DeleteAsync($"api/Scenario");
        if (result.IsSuccessStatusCode)
        {
            ScenarioMessage = "Senaryolar başarıyla silindi.";
            await LoadScenarios();
            ClearScenarioFields();
    
            StateHasChanged();
    
            await Task.Delay(2000);
            ScenarioMessage = string.Empty;
    
        }
        else
        {
            var error = await result.Content.ReadFromJsonAsync<ScenarioErrorMessage>();
            ScenarioMessage = "Hata: " + error?.Message;
        }
    }

    private async Task UpdateScenario()
    {
        ScenarioModel scenarioUpdate = new ScenarioModel
        {   
            Company = company,
            Year = scenarioYear,
            ScenarioName = scenarioName,
            EconomicIndicator = economicIndicator,
            WelfarShare = welfarShare / 100,
            RafeOfIncrase = rafeOfIncrase / 100,
            TotalRate = totalRate,
            Overtime_50 = overtime_50,
            Overtime_100 = overtime_100,
            Bonus = bonus,
            BonusMonth = bonusMonth,
            ShoppingVoucher = shoppingVoucher,
            ShoppingVoucherMonth = shoppingVoucherMonth
        };


        var updateResult = await _Http.PutAsJsonAsync($"api/Scenario", scenarioUpdate);

        if (updateResult.IsSuccessStatusCode)
        {
            ScenarioMessage = "Senaryo başarıyla güncellendi.";
            await LoadScenarios();
            ClearScenarioFields();

            StateHasChanged();

            await Task.Delay(2000);
            ScenarioMessage = string.Empty;

        }
        else
        {
            var error = await updateResult.Content.ReadFromJsonAsync<ScenarioErrorMessage>();
            ScenarioMessage = "Hata: " + error?.Message;
        }

    }

    protected override async Task OnInitializedAsync()
    {
        await LoadScenarios();
        await LoadEconomicIndicator();
        await LoadScenarioDetail();
    }

    private async Task LoadScenarios()
    {
        var result = await _Http.GetFromJsonAsync<List<ScenarioModel>>("api/Scenario");

        if (result != null)
        {
            Scenarios = result;
        }
   
    }

    private async Task OnScenarioYearChanged()
    {
        await LoadEconomicIndicator();
        economicIndicator = 0;
    }
    
    private void ClearScenarioFields()
    {
        company = Company.Aster;
        scenarioYear = DateTime.Now.Year;
        scenarioName = string.Empty;
        economicIndicator = 0;
        welfarShare = 0;
        rafeOfIncrase = 0;
        overtime_50 = 0;
        overtime_100 = 0;
        bonus = 0;
        bonusMonth = 0;
        shoppingVoucher = 0;
        shoppingVoucherMonth = 0;
    }

    private async Task LoadEconomicIndicator()
    {
        var result = await _Http.GetFromJsonAsync<EconomicIndicators>(
            $"api/EconomicIndicators/{scenarioYear}");

        if (result is not null)
        {
            Indicators = result;
        }
        
        
    }

    private async Task LoadScenarioDetail()
    {
        if (string.IsNullOrWhiteSpace(scenarioName))
        {
            return;
        }

        var result = Scenarios.FirstOrDefault(s =>
            string.Equals(s.ScenarioName, scenarioName, StringComparison.OrdinalIgnoreCase));

        if (result is not null)
        {
            company = result.Company;
            scenarioYear = result.Year;
            scenarioName = result.ScenarioName;
            economicIndicator = result.EconomicIndicator;
            // API'de oranlar 0-1 aralığında tutuluyor; formda yüzde olarak gösteriyoruz.
            welfarShare = result.WelfarShare * 100;
            rafeOfIncrase = result.RafeOfIncrase * 100;
            overtime_50 = result.Overtime_50;
            overtime_100 = result.Overtime_100;
            bonus = result.Bonus;
            bonusMonth = result.BonusMonth;
            shoppingVoucher = result.ShoppingVoucher;
            shoppingVoucherMonth = result.ShoppingVoucherMonth;

            await LoadEconomicIndicator();
        }
    }
}
