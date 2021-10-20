using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Seshat.Application.Manufacturers.Models;
using Seshat.Application.Manufacturers.Queries.GetManufacturer;
using Seshat.Application.Manufacturers.Queries.GetManufacturers;

namespace Seshat.WebUI.Controllers
{
    public class ManufacturersController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<ManufacturerDto>> Get()
        {
            return await Mediator.Send(new GetManufacturersQuery());
        }

        [HttpGet("{id}")]
        public async Task<ManufacturerDto> Get(
            string id, 
            CancellationToken cancellationToken)
        {
            return await Mediator.Send(
                new GetManufacturerQuery(id), 
                cancellationToken);
        }
    }
}