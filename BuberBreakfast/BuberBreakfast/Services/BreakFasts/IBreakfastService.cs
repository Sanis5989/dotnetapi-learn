using BuberBreakfast.Models;

namespace BuberBreakfast.Services.Breakfasts;




public interface IBreakfastService{
    void CreateBreakfast(Breakfast breakfast);
    Breakfast GetBreakfast(Guid Id);


    void UpsertBreakfast(Breakfast breakfast, Guid id);

    void DeleteBreakfast(Guid id);
}