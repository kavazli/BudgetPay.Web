using Domain.Enums;
using Domain.TaxParameters;
using Microsoft.AspNetCore.Components;

namespace UI.Components.Pages.AdminPanel;

public partial class AdminPanel
{
    
    private string StampTaxMessage { get; set; } = string.Empty;
    private List<StampTax> StampTaxes { get; set; } = new List<StampTax>();


    private int StampTaxYear;
    private decimal rate;



    private async Task StampTaxAdd()
    {
        StampTax newStampTax = new StampTax
        {
            Year = StampTaxYear,
            Rate = rate / 1000
        };

        StampTaxMessage = PropertiesControl(newStampTax);

        if(!string.IsNullOrEmpty(StampTaxMessage))
        {
            return;
        }

        var result = await _Http.PostAsJsonAsync("api/StampTax", newStampTax);

         if (result.IsSuccessStatusCode)
        {
            StampTaxMessage = "Damga vergisi başarıyla eklendi.";

            await LoadStampTaxes();
            ClearStampTaxFields();

            StateHasChanged();

            await Task.Delay(2000);
            StampTaxMessage = string.Empty;
        }
        else
        {  
            var error = await result.Content.ReadFromJsonAsync<ErrorMessage>();
            StampTaxMessage = "Hata: " + error?.Message;
        }
    }

    private async Task StampTaxUpdate()
    {
        StampTax updateStampTax = new StampTax
        {
            
            Year = StampTaxYear,
            Rate = rate / 1000
        };

        StampTaxMessage = PropertiesControl(updateStampTax);

        if(!string.IsNullOrEmpty(StampTaxMessage))
        {
            return;
        }

        var temp = await _Http.GetFromJsonAsync<StampTax>($"api/StampTax/{StampTaxYear}");

        var result = await _Http.PutAsJsonAsync($"api/StampTax/{temp?.Id}", updateStampTax);

        if (result.IsSuccessStatusCode)
        {
            StampTaxMessage = "Damga vergisi başarıyla güncellendi.";

            await LoadStampTaxes();
            ClearStampTaxFields();

            StateHasChanged();

            await Task.Delay(2000);
            StampTaxMessage = string.Empty;
        }
        else
        {   
            var error = await result.Content.ReadFromJsonAsync<ErrorMessage>();
            StampTaxMessage = "Hata: " + error?.Message;
        }
    }

    private async Task StampTaxDelete()
    {   

        StampTax deleteStampTax = new StampTax
        {
           Year = StampTaxYear,
           Rate = rate
        };


        StampTaxMessage = PropertiesControl(deleteStampTax);

        if(!string.IsNullOrEmpty(StampTaxMessage))
        {
            return;
        }

        var temp = await _Http.GetFromJsonAsync<StampTax>($"api/StampTax/{StampTaxYear}");

        var result = await _Http.DeleteAsync($"api/StampTax/{temp?.Id}");

        if (result.IsSuccessStatusCode)
        {
            StampTaxMessage = "Damga vergisi başarıyla silindi.";

            await LoadStampTaxes();
            ClearStampTaxFields();

            StateHasChanged();

            await Task.Delay(2000);
            StampTaxMessage = string.Empty;
        }
        else
        {   var error = await result.Content.ReadFromJsonAsync<ErrorMessage>();
            StampTaxMessage = "Hata: " + error?.Message;
        }
    }

    private async Task LoadStampTaxes()
    {
        var result = await _Http.GetFromJsonAsync<List<StampTax>>("api/StampTax");
        if (result != null)
        {
            StampTaxes = result;
        }
    }

    private void ClearStampTaxFields()
    {
        StampTaxYear = 0;
        rate = 0;
    }

    private string PropertiesControl(StampTax stampTax)
    {
        var props = stampTax.GetType().GetProperties();

        foreach (var p in props)
        {
            if (p.PropertyType == typeof(Guid))
            {
                continue;
            }
                
            var value = p.GetValue(stampTax);

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