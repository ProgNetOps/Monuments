using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monuments.API.Models;
using Monuments.API.Services;
using System.Text.Json;

namespace Monuments.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Authorize]
[Route("api/v{version:apiVersion}/cities")]
public class CitiesController(IMonumentRepository monumentRepository,
        IMapper mapper) : ControllerBase
{
    private readonly IMonumentRepository _monumentRepository = monumentRepository ?? throw new ArgumentNullException(nameof(monumentRepository));
    private readonly IMapper _mapper = mapper;
    const int maxCitiesPageSize = 20;
       
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CityWithoutMonumentsDto>?>> GetCities(string? name,
        string? searchQuery, int pageNumber = 1, int pageSize = 10)
    {
        pageSize = pageSize > maxCitiesPageSize ?
            maxCitiesPageSize : pageSize;

        //Retrieves the tuple containing the paging meta information and the entities
        var (cityEntities,paginationMetadata) = await _monumentRepository
            .GetCitiesAsync(name,searchQuery,pageNumber,pageSize);

        //Add the pagination meta information to the response header
        Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(paginationMetadata));

        //Maps the entities to DTO and returns the collection of DTO
        return Ok(_mapper.Map<IEnumerable<CityWithoutMonumentsDto>>(cityEntities));                
    }


    /// <summary>
    /// Get a city by id
    /// </summary>
    /// <param name="id">The id of the city to retrieve</param>
    /// <param name="includeMonuments">Boolean value indicating if monuments are returned with the city</param>
    /// <returns>An iActionResult</returns>
    /// <response code="200">Returns the requested city</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //The return type is IActionResult because the types returned implement IActionResult
    public async Task<IActionResult> GetCity(int id, bool includeMonuments=false)
    {
        //includeMonuments can be passed using querystring in the uri
        var city = await _monumentRepository.GetCityAsync(id, includeMonuments);

        if (city is null)
        {
            return NotFound();
        }

        if (includeMonuments is true)
        {
            return Ok(_mapper.Map<CityDto>(city));
        }
        else
        {
            return Ok(_mapper.Map<CityWithoutMonumentsDto>(city));

        }
    }

}
