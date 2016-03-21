using Cqrs.Infrastructure.Dto;

namespace Cqrs.ViewModels
{
    public class CustomersViewModel : ViewModel<CustomersDto>
    {
    }

    public class ViewModel<TDto>
    {
        public TDto Data { get; set; }
    }
}