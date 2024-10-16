namespace BuberBreakfast.Services.Breakfasts;
using ErrorOr;

using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;

public class BreakfastService : IBreakfastService{

    private static readonly Dictionary<Guid,Breakfast> _breakfasts = new(); 

    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast){
        _breakfasts.Add(breakfast.Id, breakfast);

        return Result.Created;
    }

    public  ErrorOr<Breakfast> GetBreakfast(Guid Id){

        if(_breakfasts.TryGetValue(Id, out var breakfast)){
            return breakfast;
        }

        return Errors.Breakfast.NotFound;

        
    }

    public ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfast breakfast){

        var isNewlyCreated = !_breakfasts.ContainsKey(breakfast.Id);
           
        _breakfasts[breakfast.Id]= breakfast;

        return new UpsertedBreakfast(isNewlyCreated);

    }

    public ErrorOr<Deleted> DeleteBreakfast(Guid id){

        _breakfasts.Remove(id);

        return Result.Deleted;
    }
}