namespace BuberBreakfast.Services.Breakfasts;

using BuberBreakfast.Models;


public class BreakfastService : IBreakfastService{

    private static readonly Dictionary<Guid,Breakfast> _breakfasts = new(); 

    public void CreateBreakfast(Breakfast breakfast){
        _breakfasts.Add(breakfast.Id, breakfast);
        foreach (var item in _breakfasts)
    {
        Console.WriteLine($"Key: {item.Key}, Value: {item.Value.Name}");
    }
    }

    public Breakfast GetBreakfast(Guid Id){
        foreach (var item in _breakfasts)
    {
        Console.WriteLine($"Keyis this: {item.Key}, Value: {item.Value.Name}");
    }
        return _breakfasts[Id];
    }

    public void UpsertBreakfast(Breakfast breakfast, Guid id){

        _breakfasts[id]= breakfast;

        return;

    }

    public void DeleteBreakfast(Guid id){

        _breakfasts.Remove(id);

        return;
    }
}