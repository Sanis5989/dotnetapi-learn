using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.Services.Breakfasts;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using BuberBreakfast.ServiceErrors;

namespace BuberBreakfast.Controllers;



public class BreakfastsController: ApiController{


    private readonly IBreakfastService _breakfastService;

    public BreakfastsController(IBreakfastService breakfastService) {
        _breakfastService = breakfastService;
    }

    public static BreakfastResponse MapBreakfastResponse(Breakfast breakfast){
    var response = new BreakfastResponse(
        breakfast.Id,
        breakfast.Name,
        breakfast.Description,
        breakfast.StartDateTime,
        breakfast.EndDateTime,
        breakfast.LastModifiedDateTime,
        breakfast.Savory,
        breakfast.Sweet
    );

    return response;
    }

    public CreatedAtActionResult createdAtGetBreakfast(Breakfast breakfast){
        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues:new {id= breakfast.Id},
            value: MapBreakfastResponse(breakfast));
    }


    //post request to post a brakfast
    [HttpPost]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request){


        ErrorOr<Breakfast> requestToBreakfastResult = Breakfast.Create(
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            request.Savory,
            request.Sweet
        );
       

       if(requestToBreakfastResult.IsError){
        return Problem(requestToBreakfastResult.Errors);
       }

        
        ErrorOr<Created> createBreakfastResult = _breakfastService.CreateBreakfast(requestToBreakfastResult.Value);

        //TODO:save the brakfast to the db

        return createBreakfastResult.Match(
            created => createdAtGetBreakfast(requestToBreakfastResult.Value),
            errors => Problem(errors)
        );
    }

    //get request to get a breakfast with id
    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id){


        ErrorOr<Breakfast> getBreakfastResult = _breakfastService.GetBreakfast(id);


        return getBreakfastResult.Match(
            breakfast => Ok(MapBreakfastResponse(breakfast)),
            errors => Problem(errors)
            );

    }



    // update/put request to update a breakfast
    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(UpsertBreakfastRequest request, Guid id){

        ErrorOr<Breakfast> resultByUpsertbreakfast = Breakfast.Create(
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            request.Savory,
            request.Sweet,
            id
            );


        if(resultByUpsertbreakfast.IsError){
            return Problem(resultByUpsertbreakfast.Errors);
        }

        ErrorOr<UpsertedBreakfast> upsertBreakfastResult = _breakfastService.UpsertBreakfast(resultByUpsertbreakfast.Value);


        return upsertBreakfastResult.Match(
            upserted => upserted.IsNewlyCreated ? createdAtGetBreakfast(resultByUpsertbreakfast.Value) : NoContent(),
            errors => Problem(errors)
        );
    }


    //get request to get a breakfast with id
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id){
        
        ErrorOr<Deleted> deleteBreakfastResult =  _breakfastService.DeleteBreakfast(id);

        return deleteBreakfastResult.Match(
            deleted => NoContent(),
            error => Problem(error)
        );

    }



}
    
