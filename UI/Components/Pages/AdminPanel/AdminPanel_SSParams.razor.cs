using Domain.Enums;
using Domain.TaxParameters;
using Microsoft.AspNetCore.Components;

namespace UI.Components.Pages.AdminPanel;

public partial class AdminPanel
{    
    // Message
    private string SSParamsMessage { get; set; } = string.Empty;
    private List<SSParams> SSParamsList { get; set; } = new List<SSParams>();

    private int ssParamsYear;
    private decimal ActiveEmployeeSSRate;
    private decimal ActiveEmployeeUIRate;
    private decimal ActiveEmployerSSRate;
    private decimal ActiveEmployerUIRate;
    private decimal RetiredEmployeeSSRate;
    private decimal RetiredEmployerSSRate;



    private async Task SSParamsAdd()
    {
        SSParams newSSParams = new SSParams
        {
            Year = ssParamsYear,
            ActiveEmployeeSSRate = ActiveEmployeeSSRate / 100,
            ActiveEmployeeUIRate = ActiveEmployeeUIRate / 100,
            ActiveEmployerSSRate = ActiveEmployerSSRate / 100,
            ActiveEmployerUIRate = ActiveEmployerUIRate / 100,
            RetiredEmployeeSSRate = RetiredEmployeeSSRate / 100,
            RetiredEmployerSSRate = RetiredEmployerSSRate / 100
        };

        var validationMessage = PropertiesControl(newSSParams);

        if (validationMessage != null)
        {
            SSParamsMessage = validationMessage;
            return;
        }

        var result = await _Http.PostAsJsonAsync("api/SSParams", newSSParams);

         if (result.IsSuccessStatusCode)
        {
            SSParamsMessage = "SS parametreleri başarıyla eklendi.";

            await LoadSSParams();
            ClearSSParamsFields();

            StateHasChanged();

            await Task.Delay(2000);
            SSParamsMessage = string.Empty;
        }
        else
        {  
            var error = await result.Content.ReadFromJsonAsync<ErrorMessage>();
            SSParamsMessage = "Hata: " + error?.Message;
        }
    }

    private async Task SSParamsUpdate()
    {
        SSParams updateSSParams = new SSParams
        {
            
            Year = ssParamsYear,
            ActiveEmployeeSSRate = ActiveEmployeeSSRate / 100,
            ActiveEmployeeUIRate = ActiveEmployeeUIRate / 100,
            ActiveEmployerSSRate = ActiveEmployerSSRate / 100,
            ActiveEmployerUIRate = ActiveEmployerUIRate / 100,
            RetiredEmployeeSSRate = RetiredEmployeeSSRate / 100,
            RetiredEmployerSSRate = RetiredEmployerSSRate / 100
        };

        var validationMessage = PropertiesControl(updateSSParams);
        
        if (validationMessage != null)
        {
            SSParamsMessage = validationMessage;
            return;
        }

        var temp = await _Http.GetFromJsonAsync<SSParams>($"api/SSParams/{ssParamsYear}");

        var result = await _Http.PutAsJsonAsync($"api/SSParams/{temp?.Id}", updateSSParams);

        if (result.IsSuccessStatusCode)
        {
            SSParamsMessage = "SS parametreleri başarıyla güncellendi.";

            await LoadSSParams();
            ClearSSParamsFields();

            StateHasChanged();

            await Task.Delay(2000);
            SSParamsMessage = string.Empty;
        }
        else
        {   
            var error = await result.Content.ReadFromJsonAsync<ErrorMessage>();
            SSParamsMessage = "Hata: " + error?.Message;
        }
    }

    private async Task SSParamsDelete()
    {   

        SSParams deleteSSParams = new SSParams
        {
            Year = ssParamsYear,
            ActiveEmployeeSSRate = ActiveEmployeeSSRate,
            ActiveEmployeeUIRate = ActiveEmployeeUIRate,
            ActiveEmployerSSRate = ActiveEmployerSSRate,
            ActiveEmployerUIRate = ActiveEmployerUIRate,
            RetiredEmployeeSSRate = RetiredEmployeeSSRate,
            RetiredEmployerSSRate = RetiredEmployerSSRate

        };

        var validationMessage = PropertiesControl(deleteSSParams);
        
        if (validationMessage != null)
        {
            SSParamsMessage = validationMessage;
            return;
        }

        var temp = await _Http.GetFromJsonAsync<SSParams>($"api/SSParams/{ssParamsYear}");

        var result = await _Http.DeleteAsync($"api/SSParams/{temp?.Id}");

        if (result.IsSuccessStatusCode)
        {
            SSParamsMessage = "SS parametreleri başarıyla silindi.";

            await LoadSSParams();
            ClearSSParamsFields();

            StateHasChanged();

            await Task.Delay(2000);
            SSParamsMessage = string.Empty;
        }
        else
        {   var error = await result.Content.ReadFromJsonAsync<ErrorMessage>();
            SSParamsMessage = "Hata: " + error?.Message;
        }
    }

    private async Task LoadSSParams()
    {
        var result = await _Http.GetFromJsonAsync<List<SSParams>>("api/SSParams");
        if (result != null)
        {
            SSParamsList = result;
        }
    }

    private void ClearSSParamsFields()
    {
        ssParamsYear = 0;
        ActiveEmployeeSSRate = 0;
        ActiveEmployeeUIRate = 0;
        ActiveEmployerSSRate = 0;
        ActiveEmployerUIRate = 0;
        RetiredEmployeeSSRate = 0;
        RetiredEmployerSSRate = 0;
    }

    private string PropertiesControl(SSParams ssParams)
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
