using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers;

[ApiController]
[Route("breakfasts")]

public class BreakfastsController: ControllerBase{

    //post request to post a brakfast
    [HttpPost]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request){

        var breakfast = new Breakfast(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );

        //TODO:save the brakfast to the db


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

        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues:new {id= breakfast.Id},
            value: response);
    }

    //get request to get a breakfast with id
    [HttpGet("/{id:guid}")]
    public IActionResult GetBreakfast(Guid id){
        return Ok(id);
    }

    // update/put request to update a breakfast
    [HttpPut("/{id:guid}")]
    public IActionResult UpsertBreakfast(UpsertBreakfastRequest request, Guid id){
        return Ok(request);
    }


    //get request to get a breakfast with id
    [HttpDelete("/{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id){
        return Ok(id);
    }



}
    
