using System.Threading.Tasks;
using MarqueesAssistant.API.Data;
using MarqueesAssistant.API.Helpers;
using MarqueesAssistant.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarqueesAssistant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlacesController : ControllerBase
    {

        private readonly IPlaceRepo _repo;

        public PlacesController(IPlaceRepo repo)
        {
            _repo = repo;
        }


        [Authorize(Roles = "admin, kierownik, pracownik")]
        [HttpGet]
        public async Task<IActionResult> GetPlacesSearchedAndSorted([FromQuery] PageParameters pageParameters, string searchString, int sortBy)
        {
            if (searchString == null)
            {
                searchString = "";
            }
            if (sortBy.Equals(null))
            {
                sortBy = 0;
            }
           
            var places = await _repo.GetPlacesListedSearchedSorted(pageParameters, searchString, sortBy);
            Pagger<Place> placesToReturn = new Pagger<Place>(places);
            return Ok(placesToReturn);
        }



        [Authorize(Roles = "admin, kierownik, pracownik")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlace(int id)
        {
            var place = await _repo.GetPlace(id);
            return Ok(place);
        }

        [HttpPost]
        [Authorize(Roles = "admin, kierownik")]
        public async Task<IActionResult> AddPlace(Place place)
        {
            var placeToCreate = new Place
            {
                Street = place.Street,
                Number = place.Number,
                Town = place.Town,
                FirstGradeDivision = place.FirstGradeDivision,
                SecondGradeDivision = place.SecondGradeDivision,
                ThirdGradeDivision = place.ThirdGradeDivision,
                PostCode = place.PostCode
            };

            _repo.Add<Place>(placeToCreate);

            if (await _repo.SaveAll())
            {
                return Ok();
            }

            return BadRequest("Nie można dodać miejsca");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin, kierownik")]
        public async Task<IActionResult> DeletePlace(int id)
        {
            Place place = await _repo.GetPlace(id);
            _repo.Delete(place);

            if (await _repo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("Nie można usunąć miejsca");
            
        }

        [HttpPut]
        [Authorize(Roles = "admin, kierownik")]
        public async Task<IActionResult> EditPlace(Place place)
        {
            _repo.Edit<Place>(place);
            if (await _repo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("Nie można edytować miejsca");
        }
    }
}