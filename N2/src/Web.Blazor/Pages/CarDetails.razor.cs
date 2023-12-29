using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Components;
using Web.Blazor.Services;

namespace Web.Blazor.Pages;

public partial class CarDetails
{
    [Inject]
    public ICarDetailsService CarDetailsService { get; set; }

    private IList<CarDetail> _bucket;

    private IEnumerable<CarDetail> _carDetails;

    private bool _isLoadingFinish;

    private const int NUMBER_OF_SALE_DETAIL = 3;

    protected override async Task OnInitializedAsync()
    {
        _bucket = new List<CarDetail>();
        _carDetails = await CarDetailsService.GetListAsync();
        _isLoadingFinish = true;
    }

    private void AddToBucket(CarDetail carDetail)
    {
        if (_bucket.FirstOrDefault(x => x.Id == carDetail.Id) is null)
        {
            _bucket.Add(carDetail);
        }
        else
        {
            _bucket.Remove(carDetail);
        }
    }

    private decimal CalculateTotalPrice()
    {
        decimal sum = 0;

        foreach (var (item, index) in _bucket.Select((x, index) => (x, index)))
        {
            sum += index == NUMBER_OF_SALE_DETAIL - 1
                ? item.Price - (item.Price * 40 / 100)
                : item.Price;
        }

        return sum;
    }
}
