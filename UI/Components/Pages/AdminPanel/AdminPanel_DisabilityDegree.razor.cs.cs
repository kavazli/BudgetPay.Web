using Domain.Enums;
using Domain.TaxParameters;
using Microsoft.AspNetCore.Components;

namespace UI.Components.Pages.AdminPanel;

public partial class AdminPanel
{
    private string DisabilityDegreeMessage { get; set; } = string.Empty;
    private List<DisabilityDegree> DisabilityDegrees { get; set; } = new List<DisabilityDegree>();

    private int disabilityDegreeYear;
    private Degree degree;
    private decimal amount;

    private async Task DisabilityDegreeAdd()
    {
        DisabilityDegree newDisabilityDegree = new DisabilityDegree
        {
            Year = disabilityDegreeYear,
            Degree = degree,
            Amount = amount
        };

        DisabilityDegreeMessage = PropertiesControl(newDisabilityDegree);

        if (!string.IsNullOrEmpty(DisabilityDegreeMessage))
        {
            return;
        }
    

        var result = await _Http.PostAsJsonAsync("api/DisabilityDegree", newDisabilityDegree);

         if (result.IsSuccessStatusCode)
        {
            DisabilityDegreeMessage = "Engellilik derecesi başarıyla eklendi.";

            await LoadDisabilityDegrees();
            ClearDisabilityDegreeFields();

            StateHasChanged();

            await Task.Delay(2000);
            DisabilityDegreeMessage = string.Empty;
        }
        else
        {
            var error = await result.Content.ReadFromJsonAsync<ErrorMessage>();
            DisabilityDegreeMessage = "Hata: " + error?.Message;
        }

    }

    private async Task DisabilityDegreeUpdate()
    {
        DisabilityDegree updatedDisabilityDegree = new DisabilityDegree
        {
            Year = disabilityDegreeYear,
            Degree = degree,
            Amount = amount
        };

        DisabilityDegreeMessage = PropertiesControl(updatedDisabilityDegree);
        
        if (!string.IsNullOrEmpty(DisabilityDegreeMessage))
        {
            return;
        }

        var temp = await _Http.GetFromJsonAsync<DisabilityDegree>($"api/DisabilityDegree/{disabilityDegreeYear}/{degree}");

        var result = await _Http.PutAsJsonAsync($"api/DisabilityDegree/{temp?.Id}", updatedDisabilityDegree);

        if (result.IsSuccessStatusCode)
        {
            DisabilityDegreeMessage = "Engellilik derecesi başarıyla güncellendi.";

            await LoadDisabilityDegrees();
            ClearDisabilityDegreeFields();

            StateHasChanged();

            await Task.Delay(2000);
            DisabilityDegreeMessage = string.Empty;
        }
        else
        {
            var error = await result.Content.ReadFromJsonAsync<ErrorMessage>();
            DisabilityDegreeMessage = "Hata: " + error?.Message;
        }
    }

    private async Task DisabilityDegreeDelete()
    {
        DisabilityDegree deletedDisabilityDegree = new DisabilityDegree
        {
            Year = disabilityDegreeYear,
            Degree = degree,
            Amount = amount
        };

        DisabilityDegreeMessage = PropertiesControl(deletedDisabilityDegree);
        
        if (!string.IsNullOrEmpty(DisabilityDegreeMessage))
        {
            return;
        }

        var temp = await _Http.GetFromJsonAsync<DisabilityDegree>($"api/DisabilityDegree/{disabilityDegreeYear}/{degree}");

        var result = await _Http.DeleteAsync($"api/DisabilityDegree/{temp?.Id}");

        if (result.IsSuccessStatusCode)
        {
            DisabilityDegreeMessage = "Engellilik derecesi başarıyla silindi.";

            await LoadDisabilityDegrees();
            ClearDisabilityDegreeFields();

            StateHasChanged();

            await Task.Delay(2000);
            DisabilityDegreeMessage = string.Empty;
        }
        else
        {
            var error = await result.Content.ReadFromJsonAsync<ErrorMessage>();
            DisabilityDegreeMessage = "Hata: " + error?.Message;
        }
    }

    private async Task LoadDisabilityDegrees()
    {
        var result = await _Http.GetFromJsonAsync<List<DisabilityDegree>>("api/DisabilityDegree");
        var resultSorted = result.OrderByDescending(x => x.Year).ThenByDescending(x => x.Degree).ToList();
        if (result != null && result.Any())
        {
            DisabilityDegrees = resultSorted;

        }

        
    }

    private void ClearDisabilityDegreeFields()
    {
        disabilityDegreeYear = 0;
        degree = Degree.None;
        amount = 0;
    } 

    private string PropertiesControl(DisabilityDegree degree)
        {
            var props = degree.GetType().GetProperties();

            foreach (var p in props)
            {
                if (p.PropertyType == typeof(Guid))
                {
                    continue;
                }

                if(p.PropertyType == typeof(Degree))
                {
                    continue;
                }
   
                var value = p.GetValue(degree);

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
