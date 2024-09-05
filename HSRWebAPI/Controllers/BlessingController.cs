using HSRWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HSRWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlessingController : ControllerBase
    {
        [HttpGet("getAllMyBlessings")]
        public ActionResult<List<Blessing>> GetAllMyBlessings()
        {
            var myBlessings = MyDBContext.MyBlessings;
            int statusCode = MyDBContext.GetAllMyBlessings();

            switch (statusCode)
            {
                case 404:
                    return NotFound("Blessing list empty or not found.");
            }

            return Ok(myBlessings);
        }

        [HttpPost("AddBlessing-{id}")]
        public ActionResult<List<Blessing>> AddBlessingById(string id)
        {
            var myBlessings = MyDBContext.MyBlessings;
            int statusCode = MyDBContext.AddBlessingById(id);

            switch (statusCode) // Indicates Not Found status
            {
                case 404:
                    return NotFound("Blessing does not exist.");
                case 400:
                    return BadRequest("Blessing already obtained.");
            }

            return CreatedAtAction(nameof(AddBlessingById), myBlessings);
        }

        [HttpPost("DeleteBlessing-{id}")]
        public IActionResult DeleteBlessingById(string id)
        {
            var myBlessings = MyDBContext.MyBlessings;
            int statusCode = MyDBContext.DeleteBlessingById(id);

            switch (statusCode)
            {
                case 400:
                    return BadRequest("The Blessing list is empty.");
                case 404:
                    return NotFound("Blessing does not exist.");
            }
            return Ok(myBlessings);
        }

        [HttpGet("ShowUnobtainedBlessings")]
        public ActionResult<List<Blessing>> ShowUnobtainedBlessings()
        {
            var missingBlessings = MyDBContext.ShowUnobtainedBlessings();
            if (missingBlessings.Count == 0)    // No Blessing is missing => All Blessings have been obtained
            {
                return Ok("All Blessings have been obtained.");
            }
            return Ok(missingBlessings);
        }

        [HttpGet("GetBlessingsCountByPath")]
        public IActionResult GetBlessingsCountByPath()
        {
            var myBlessings = MyDBContext.MyBlessings;
            var blessingCounts = MyDBContext.GetBlessingsCountByPath();

            if (blessingCounts.Count == 0)
            {
                return BadRequest("List is empty.");
            }

            return Ok(blessingCounts);
        }
    }
}
