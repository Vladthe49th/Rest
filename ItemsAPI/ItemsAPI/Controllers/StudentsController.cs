using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using ItemsApi.Models;

namespace ItemsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly TableServiceClient _tableServiceClient;

        public StudentsController(
            TableServiceClient tableServiceClient)
        {
            _tableServiceClient = tableServiceClient;
        }


        [HttpPost]
        public async Task<IActionResult> CreateStudent(
    StudentEntity student)
        {
            var tableClient =
                _tableServiceClient.GetTableClient("Students");

            await tableClient.CreateIfNotExistsAsync();

            student.RowKey = Guid.NewGuid().ToString();

            await tableClient.AddEntityAsync(student);

            return Ok(student);
        }


        [HttpGet]
        public IActionResult GetStudents()
        {
            var tableClient =
                _tableServiceClient.GetTableClient("Students");

            var students =
                tableClient.Query<StudentEntity>()
                           .ToList();

            return Ok(students);
        }
    }
}