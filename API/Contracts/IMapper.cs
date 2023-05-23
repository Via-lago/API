using API.ViewModels.Accounts;

namespace API.Contracts
{
    public interface IMapper<TModel,TViewModel>
    {
        TViewModel Map(TModel model);
        TModel Map(TViewModel viewmodel);
        object Map(AccountEmployeeVM accountEmployeeVM);
    }
}
