using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Monuments.API.Entities;
using Monuments.API.Models;
using Monuments.API.Services;
using System.Reflection.Metadata;

namespace Monuments.API.Controllers;

[ApiController]
[ApiVersion("2.0")]
[Authorize(Policy = "MustBeFromBauchi")]
[Route("api/v{version:apiVersion}/cities/{cityId}/monuments")]
public class MonumentsController(ILogger<MonumentsController> logger,
        IMailService mailService,
        IMonumentRepository monumentRepository,
        IMapper mapper) : ControllerBase
{
    private readonly ILogger<MonumentsController> _logger = logger ??
            throw new ArgumentNullException(nameof(logger));
    private readonly IMailService _mailService = mailService ??
            throw new ArgumentNullException(nameof(mailService));
    private readonly IMonumentRepository _monumentRepository = monumentRepository ??
            throw new ArgumentNullException(nameof(monumentRepository));
    private readonly IMapper _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
       

    //READ - CRUD claim-based authorization
    /// <summary>
    /// User can only Get monuments in their own city, that is the City Claim of a user must be equal to the 
    /// city the user wants to view, otherwise, 403 Forbidden is returned
    /// </summary>
    /// <param name="cityId"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MonumentsDto>>> GetMonuments(int cityId)
    {
        

        ////We look for the "city" claim, "city" being the name we gave to the
        ////claim when we constructed the token
        //var cityName = User.Claims.FirstOrDefault(c => c.Type == "city")?.Value;

        //if(await _monumentRepository.CityNameMatchesCityId(cityName, cityId) is false)
        //{
        //    return Forbid();//User is not allowed to access the resource 403vForbidden-status code
        //}

        //Check if city exists
        if(await _monumentRepository.CityExistsAsync(cityId) is false)
        {
            _logger.LogInformation($"City with Id {cityId} wasn't found when accessing monuments");
            return NotFound();
        }
        
           var monumentsForCity = await _monumentRepository.GetMonumentsForCityAsync(cityId);
        
            return Ok(_mapper.Map<IEnumerable<MonumentsDto>>(monumentsForCity));

    }


    //READ - CRUD
    [HttpGet("{monumentId}", Name = "GetMonument")]//Name needed by CreatedAtRoute
    public async Task<ActionResult<MonumentsDto?>> GetMonument(int cityId, int monumentId)
    {
        //Check if city exists
        if (await _monumentRepository.CityExistsAsync(cityId) is false)
        {
           return NotFound();
        }

        //Find monument
        var monumentToReturn = await _monumentRepository.
            GetMonumentForCityAsync(cityId, monumentId);

        if (monumentToReturn is null)
        {
            return NotFound();
        }
        else
        {
            return Ok(_mapper.Map<MonumentsDto>(monumentToReturn));
        }

    }


    //CREATE - CRUD
    [HttpPost]
    public async Task<ActionResult<MonumentsDto>> CreateMonument(int cityId, MonumentForCreationDto monument)
    {
        if (await _monumentRepository.CityExistsAsync(cityId) is false)
        {
            return NotFound();
        }

        //Map MonumentForCreationDto to Entity type
        var latestMonument = _mapper.Map<Entities.Monument>(monument);

        await _monumentRepository.AddMonumentToCityAsync(cityId, latestMonument);

        await _monumentRepository.SaveChangesAsync();

        //Map Entity type back to MonumentsDto (we'll need the generated Id)
        var createdMonumentToReturn = _mapper.Map<MonumentsDto>(latestMonument);

        //Return the DTO
        return CreatedAtRoute(nameof(GetMonument), new
        {
            cityId = cityId,
            monumentId = createdMonumentToReturn.Id
        },
        createdMonumentToReturn);
    }


    [HttpPut("{monumentId}")]
    public async Task<ActionResult> UpdateMonument(int cityId, int monumentId, MonumentForUpdateDto monument)
    {
       if (await _monumentRepository.CityExistsAsync(cityId) is false)
        {
            return NotFound();
        }

        //Find monument
        var monumentEntity = await _monumentRepository.GetMonumentForCityAsync(cityId, monumentId);
        if (monumentEntity is null)
        {
            return NotFound();
        }

        //To update, Automapper will overwrite the source with the destination
        //values, that is the values in the request body replace the data from the store.
        _mapper.Map(monument, monumentEntity);

        await _monumentRepository.SaveChangesAsync();

        return NoContent();

    }


    [HttpPatch("{monumentId}")]
    public async Task<ActionResult> PartiallyUpdateMonument(int cityId, int monumentId,
        JsonPatchDocument<MonumentForUpdateDto> patchDocument)
    {
        if (await _monumentRepository.CityExistsAsync(cityId) is false)
        {
            return NotFound();
        }

        //Find monument
        var monumentEntity = await _monumentRepository.GetMonumentForCityAsync(cityId, monumentId);
        if (monumentEntity is null)
        {
            return NotFound();
        }


        //Map to MonumentForUpdateDto
        //Since the patch document works on MonumentForUpdateDto (defined in method parameter)
        MonumentForUpdateDto monumentToPatch = _mapper.Map<MonumentForUpdateDto>(monumentEntity);

        //Apply the patch document
        patchDocument.ApplyTo(monumentToPatch, ModelState);

        //Check if modelstate is not invalidated after applying the patch document to the DTO
        if (ModelState.IsValid is false)
        {
            return BadRequest(ModelState);
        }

        //
        if (TryValidateModel(monumentToPatch) is false)
        {
            return BadRequest(ModelState);
        }

        //Map the DTO back to the Entity
        _mapper.Map(monumentToPatch, monumentEntity);

        await _monumentRepository.SaveChangesAsync();

        return NoContent();
    }


    [HttpDelete("{monumentId}")]
    public async Task<ActionResult> DeleteMonument(int cityId, int monumentId)
    {
        //Find city
        if (await _monumentRepository.CityExistsAsync(cityId) is false)
        {
            return NotFound();
        }

        //Find monument
        var monumentEntity = await _monumentRepository.GetMonumentForCityAsync(cityId, monumentId);
        if (monumentEntity is null)
        {
            return NotFound();
        }

        _monumentRepository.DeleteMonument(monumentEntity);
        await _monumentRepository.SaveChangesAsync();

        //We want to be notified whenever a resource is deleted
        _mailService.Send("Monument deleted",
            $"Monument {monumentEntity.Name} with Id {monumentEntity.Id} was deleted.");

        return NoContent();
    }
}



 