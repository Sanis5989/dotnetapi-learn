namespace BuberBreakfast.Contracts.Breakfast;

public record UpsertBreakfastRequest(
    string Name,
    string Decription,
    DateTime StartDateTime,
    DateTime EndDateTime,
    List<string> Savory,
    List<string> Sweet
);