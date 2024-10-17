using ErrorOr;

namespace BuberBreakfast.ServiceErrors;

public static class Errors {

    public static class Breakfast{
        
        public static Error NotFound => Error.NotFound(
            code:"Breakfast.NotFound",
            description:"Breakfast Not found."
        );

        public static Error InvalidName => Error.Validation(
            code:"Breakfast.InvalidName",
            description:$"Breakfast name must be at least {Models.Breakfast.MinNameLength} or {Models.Breakfast.MaxNameLength} maximum characters."
        );

        public static Error InvalidDescription => Error.Validation(
            code:"Breakfast.InvalidDescription",
            description:$"Breakfast description must be at least {Models.Breakfast.MaxDescriptionLength} or {Models.Breakfast.MaxDescriptionLength} maximum characters."
        );
    }
}