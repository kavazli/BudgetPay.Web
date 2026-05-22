using Domain.Enums;
using Domain.TaxParameters;
using Microsoft.AspNetCore.Components;

namespace UI.Components.Pages.AdminPanel;

public partial class AdminPanel
{
    private string IncomeTaxMessage { get; set; } = string.Empty;
    private List<IncomeTaxBracket> IncomeTaxBrackets { get; set; } = null;

    private int ıncomeTaxYear;
    private int bracket;
    private decimal minAmount;
    private decimal maxAmount;
    private decimal taxRate;



    private async Task IncomeTaxAdd()
    {
        IncomeTaxBracket newIncomeTaxBracket = new IncomeTaxBracket
        {
            Year = ıncomeTaxYear,
            Bracket = bracket,
            MinAmount = minAmount,
            MaxAmount = maxAmount,
            Rate = taxRate / 100
        };

        IncomeTaxMessage = PropertiesControl(newIncomeTaxBracket);

        if (!string.IsNullOrEmpty(IncomeTaxMessage))
        {
            return;
        }




        var result = await _Http.PostAsJsonAsync("api/IncomeTaxBracket", newIncomeTaxBracket);

         if (result.IsSuccessStatusCode)
        {
            IncomeTaxMessage = "Gelir vergisi dilimi başarıyla eklendi.";

            await LoadIncomeTaxBrackets();
            ClearIncomeTaxFields();

            StateHasChanged();

            await Task.Delay(2000);
            IncomeTaxMessage = string.Empty;
        }
        else
        {
            var error = await result.Content.ReadFromJsonAsync<ErrorMessage>();
            IncomeTaxMessage = "Hata: " + error?.Message;
        }

    }

    private async Task IncomeTaxUpdate()
    {
        IncomeTaxBracket updatedIncomeTaxBracket = new IncomeTaxBracket
        {
            Year = ıncomeTaxYear,
            Bracket = bracket,
            MinAmount = minAmount,
            MaxAmount = maxAmount,
            Rate = taxRate / 100
        };

        IncomeTaxMessage = PropertiesControl(updatedIncomeTaxBracket);
        
        if (!string.IsNullOrEmpty(IncomeTaxMessage))
        {
            return;
        }

        var temp = await _Http.GetFromJsonAsync<IncomeTaxBracket>($"api/IncomeTaxBracket/{ıncomeTaxYear}/{bracket}");

        var result = await _Http.PutAsJsonAsync($"api/IncomeTaxBracket/{temp?.Id}", updatedIncomeTaxBracket);

        if (result.IsSuccessStatusCode)
        {
            IncomeTaxMessage = "Gelir vergisi dilimi başarıyla güncellendi.";

            await LoadIncomeTaxBrackets();
            ClearIncomeTaxFields();

            StateHasChanged();

            await Task.Delay(2000);
            IncomeTaxMessage = string.Empty;
        }
        else
        {
            var error = await result.Content.ReadFromJsonAsync<ErrorMessage>();
            IncomeTaxMessage = "Hata: " + error?.Message;
        }
    }

    private async Task IncomeTaxDelete()
    {
        IncomeTaxBracket deletedIncomeTaxBracket = new IncomeTaxBracket
        {
            Year = ıncomeTaxYear,
            Bracket = bracket,
            MinAmount = minAmount,
            MaxAmount = maxAmount,
            Rate = taxRate
        };

        IncomeTaxMessage = PropertiesControl(deletedIncomeTaxBracket);
        
        if (!string.IsNullOrEmpty(IncomeTaxMessage))
        {
            return;
        }

        var temp = await _Http.GetFromJsonAsync<IncomeTaxBracket>($"api/IncomeTaxBracket/{ıncomeTaxYear}/{bracket}");

        var result = await _Http.DeleteAsync($"api/IncomeTaxBracket/{temp?.Id}");

        if (result.IsSuccessStatusCode)
        {
            IncomeTaxMessage = "Gelir vergisi dilimi başarıyla silindi.";

            await LoadIncomeTaxBrackets();
            ClearIncomeTaxFields();

            StateHasChanged();

            await Task.Delay(2000);
            IncomeTaxMessage = string.Empty;
        }
        else
        {
            var error = await result.Content.ReadFromJsonAsync<ErrorMessage>();
            IncomeTaxMessage = "Hata: " + error?.Message;
        }
    }

    private async Task LoadIncomeTaxBrackets()
    {
        var result = await _Http.GetFromJsonAsync<List<IncomeTaxBracket>>("api/IncomeTaxBracket");
        var resultSorted = result?.OrderBy(x => x.Year).ThenBy(x => x.Bracket).ToList();


        if (resultSorted != null && resultSorted.Any())
        {
            IncomeTaxBrackets = resultSorted;
        }
        else
        {
            IncomeTaxBrackets = null;
        }
        
    }

    private void ClearIncomeTaxFields()
    {
        ıncomeTaxYear = 0;
        bracket = 0;
        minAmount = 0;
        maxAmount = 0;
        taxRate = 0;
    }

    private string PropertiesControl(IncomeTaxBracket taxBracket)
    {
        var props = taxBracket.GetType().GetProperties();

        foreach (var p in props)
        {
            if (p.PropertyType == typeof(Guid))
            {
                continue;
            }
                
            var value = p.GetValue(taxBracket);

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
