using Domain.Enums;
using Domain.TaxParameters;
using Microsoft.AspNetCore.Components;

namespace UI.Components.Pages.AdminPanel;

public partial class AdminPanel
{   
    [Inject]
    HttpClient _Http { get; set; } = null!;

     // Tab
    private string ActiveTab { get; set; } = "ShowMinimumWage";

    // Message
    private string MinimumWageMessage { get; set; } = string.Empty;
    private List<MinimumWage> MinimumWages { get; set; } = new List<MinimumWage>();


    // ── Tab actions ──────────────────────────────────────────────────────────
    private void SetTab(string tab)
    {
        ActiveTab = tab;
        
    }


    // Minimum_wage Properties
    private int minimum_wageYear;
    private decimal grossSalary;
    private decimal netSalary;
    private decimal retiredNetSalary;
    private decimal ceiling;


    private async Task MinimumWageAdd()
    {
        MinimumWage newMinimumWage = new MinimumWage
        {
            Year = minimum_wageYear,
            GrossSalary = grossSalary,
            NetSalary = netSalary,
            RetiredNetSalary = retiredNetSalary,
            Ceiling = ceiling
        };

        MinimumWageMessage = PropertiesControl(newMinimumWage);

        if (!string.IsNullOrEmpty(MinimumWageMessage))
        {
            return;
        }

        var result = await _Http.PostAsJsonAsync("api/MinimumWage", newMinimumWage);

         if (result.IsSuccessStatusCode)
        {
            MinimumWageMessage = "Asgari ücret başarıyla eklendi.";

            await LoadMinimumWages();
            ClearMinimumWageFields();

            StateHasChanged();

            await Task.Delay(2000);
            MinimumWageMessage = string.Empty;
        }
        else
        {
            var error = await result.Content.ReadFromJsonAsync<ErrorMessage>();
            MinimumWageMessage = "Hata: " + error?.Message;
        }

    }

    private async Task MinimumWageUpdate()
    {
        MinimumWage updatedMinimumWage = new MinimumWage
        {
            Year = minimum_wageYear,
            GrossSalary = grossSalary,
            NetSalary = netSalary,
            RetiredNetSalary = retiredNetSalary,
            Ceiling = ceiling
        };

        MinimumWageMessage = PropertiesControl(updatedMinimumWage);

        if (!string.IsNullOrEmpty(MinimumWageMessage))
        {
            return;
        }

        var temp = await _Http.GetFromJsonAsync<MinimumWage>($"api/MinimumWage/{minimum_wageYear}");

        var result = await _Http.PutAsJsonAsync($"api/MinimumWage/{temp?.Id}", updatedMinimumWage);

        if (result.IsSuccessStatusCode)
        {
            MinimumWageMessage = "Asgari ücret başarıyla güncellendi.";

            await LoadMinimumWages();
            ClearMinimumWageFields();

            StateHasChanged();

            await Task.Delay(2000);
            MinimumWageMessage = string.Empty;
        }
        else
        {
            var error = await result.Content.ReadFromJsonAsync<ErrorMessage>();
            MinimumWageMessage = "Hata: " + error?.Message;
        }
    }

    private async Task MinimumWageDelete()
    {
        MinimumWage deletedMinimumWage = new MinimumWage
        {
            Year = minimum_wageYear,
            GrossSalary = grossSalary,
            NetSalary = netSalary,
            RetiredNetSalary = retiredNetSalary,
            Ceiling = ceiling
        };

        MinimumWageMessage = PropertiesControl(deletedMinimumWage);
        
        if (!string.IsNullOrEmpty(MinimumWageMessage))
        {
            return;
        }


        var temp = await _Http.GetFromJsonAsync<MinimumWage>($"api/MinimumWage/{minimum_wageYear}");

        var result = await _Http.DeleteAsync($"api/MinimumWage/{temp?.Id}");

        if (result.IsSuccessStatusCode)
        {
            MinimumWageMessage = "Asgari ücret başarıyla silindi.";

            await LoadMinimumWages();
            ClearMinimumWageFields();

            StateHasChanged();

            await Task.Delay(2000);
            MinimumWageMessage = string.Empty;
        }
        else
        {
            var error = await result.Content.ReadFromJsonAsync<ErrorMessage>();
            MinimumWageMessage = "Hata: " + error?.Message;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        await LoadMinimumWages();
        await LoadStampTaxes();
        await LoadSSParams();
        await LoadIncomeTaxBrackets();
        await LoadDisabilityDegrees();
        await LoadEconomicIndicators();
    }

    private async Task LoadMinimumWages()
    {
        var result = await _Http.GetFromJsonAsync<List<MinimumWage>>("api/MinimumWage");
        if (result != null)
        {
            MinimumWages = result;
        }
        
    }

    private void ClearMinimumWageFields()
    {
        minimum_wageYear = 0;
        grossSalary = 0;
        netSalary = 0;
        retiredNetSalary = 0;
        ceiling = 0;
    }

    private string PropertiesControl(MinimumWage wage)
    {
        var props = wage.GetType().GetProperties();

        foreach (var p in props)
        {
            if (p.PropertyType == typeof(Guid))
            {
                continue;
            }
                
            var value = p.GetValue(wage);

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