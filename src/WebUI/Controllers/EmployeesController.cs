using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recapi.Application.Employees.Commands.DeleteEmployee;
using Recapi.Application.Employees.Commands.UpsertEmployee;
using Recapi.Application.Employees.Queries.GetEmployeeDetail;
using Recapi.Application.Employees.Queries.GetEmployeesList;

namespace Recapi.WebUI.Controllers
{
    public class EmployeesController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<EmployeeLookupDto>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetEmployeesListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EmployeeDetailVm>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetEmployeeDetailQuery { Id = id }));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertEmployeeCommand command)
        {
            var id = await Mediator.Send<int>(command);

            return Ok(id);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send<Unit>(new DeleteEmployeeCommand { Id = id });

            return NoContent();
        }
    }
}
