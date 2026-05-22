using System;
using Domain.TaxParameters;

namespace UI.Components.Pages.AdminPanel;

public partial class AdminPanel
{
    
    private string EconomicIndicatorsMessage { get; set; } = string.Empty;
    private List<EconomicIndicators> EconomicIndicatorsList { get; set; } = new List<EconomicIndicators>();


    private int economicIndicatorsYear;
    private decimal minimumWageIncreaseRate;
    private decimal inflationRate;
    private decimal RevaluationRate;


    private async Task EconomicIndicatorsAdd()
    {
        EconomicIndicators newEconomicIndicators = new EconomicIndicators
        {
            Year = economicIndicatorsYear / 100,
            MinimumWageIncreaseRate = minimumWageIncreaseRate / 100,
            InflationRate = inflationRate / 100,
            RevaluationRate = RevaluationRate /100
        };

        var validationMessage = PropertiesControl(newEconomicIndicators);

        if (validationMessage != null)
        {
            EconomicIndicatorsMessage = validationMessage;
            return;
        }

        var result = await _Http.PostAsJsonAsync("api/EconomicIndicators", newEconomicIndicators);

         if (result.IsSuccessStatusCode)
        {
            EconomicIndicatorsMessage = "Ekonomik göstergeler başarıyla eklendi.";

            await LoadEconomicIndicators();
            ClearEconomicIndicatorsFields();

            StateHasChanged();

            await Task.Delay(2000);
            EconomicIndicatorsMessage = string.Empty;
        }
        else
        {  
            var error = await result.Content.ReadFromJsonAsync<ErrorMessage>();
            EconomicIndicatorsMessage = "Hata: " + error?.Message;
        }
    }

    private async Task EconomicIndicatorsUpdate()
    {
        EconomicIndicators updatedEconomicIndicators = new EconomicIndicators
        {
            Year = economicIndicatorsYear / 100,
            MinimumWageIncreaseRate = minimumWageIncreaseRate / 100,
            InflationRate = inflationRate / 100,
            RevaluationRate = RevaluationRate / 100
        };

        var validationMessage = PropertiesControl(updatedEconomicIndicators);

        if (validationMessage != null)
        {
            EconomicIndicatorsMessage = validationMessage;
            return;
        }

        var temp = await _Http.GetFromJsonAsync<EconomicIndicators>($"api/EconomicIndicators/{economicIndicatorsYear}");

        var result = await _Http.PutAsJsonAsync($"api/EconomicIndicators/{temp?.Id}", updatedEconomicIndicators);

         if (result.IsSuccessStatusCode)
        {
            EconomicIndicatorsMessage = "Ekonomik göstergeler başarıyla güncellendi.";

            await LoadEconomicIndicators();
            ClearEconomicIndicatorsFields();

            StateHasChanged();

            await Task.Delay(2000);
            EconomicIndicatorsMessage = string.Empty;
        }
        else
        {  
            var error = await result.Content.ReadFromJsonAsync<ErrorMessage>();
            EconomicIndicatorsMessage = "Hata: " + error?.Message;
        }
    }

    private async Task EconomicIndicatorsDelete()
    {
        EconomicIndicators economicIndicators = new EconomicIndicators
        {
            Year = economicIndicatorsYear,
            MinimumWageIncreaseRate = minimumWageIncreaseRate,
            InflationRate = inflationRate,
            RevaluationRate = RevaluationRate
        };
        var validationMessage = PropertiesControl(economicIndicators);

        if (validationMessage != null)
        {
            EconomicIndicatorsMessage = validationMessage;
            return;
        }

        var temp = await _Http.GetFromJsonAsync<EconomicIndicators>($"api/EconomicIndicators/{economicIndicatorsYear}");

        var result = await _Http.DeleteAsync($"api/EconomicIndicators/{temp?.Id}");

         if (result.IsSuccessStatusCode)
        {
            EconomicIndicatorsMessage = "Ekonomik göstergeler başarıyla silindi.";

            await LoadEconomicIndicators();
            ClearEconomicIndicatorsFields();

            StateHasChanged();

            await Task.Delay(2000);
            EconomicIndicatorsMessage = string.Empty;
        }
        else
        {  
            var error = await result.Content.ReadFromJsonAsync<ErrorMessage>();
            EconomicIndicatorsMessage = "Hata: " + error?.Message;
        }
    }

    private async Task LoadEconomicIndicators()
    {
        var result = await _Http.GetFromJsonAsync<List<EconomicIndicators>>("api/EconomicIndicators");
        if (result != null)
        {
            EconomicIndicatorsList = result;
        }
    }

    private void ClearEconomicIndicatorsFields()
    {
        economicIndicatorsYear = 0;
        minimumWageIncreaseRate = 0;
        inflationRate = 0;
        RevaluationRate = 0;
    }

    private string PropertiesControl(EconomicIndicators ssParams)
    {
        var props = ssParams.GetType().GetProperties();

        foreach (var p in props)
        {
            if (p.PropertyType == typeof(Guid))
            {
                continue;
            }
                
            var value = p.GetValue(ssParams);

            if (p.PropertyType == typeof(int) && (int)value == 0)
            {
                return $"Tüm alanlar doldurulmalıdır.";
            }

            if (p.PropertyType == typeof(decimal) && (decimal)value == 0)
            {
                return $"Tüm alanlar doldurulmalıdır.";
            }
        }

        return null; // Hata yok
    }

}


