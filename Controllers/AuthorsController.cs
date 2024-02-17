using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using store_procedure.Models;
namespace store_procedure.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
        public readonly ILogger<AuthorController> _logger;
        private readonly storeprocedureContext _applicationDbContext;

        public AuthorController(ILogger<AuthorController> logger,storeprocedureContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var mysqlParameters = new List<MySqlParameter>()
            {
                new MySqlParameter("@p_Id", default),
                new MySqlParameter("@p_authorName", default),
                new MySqlParameter("@p_birthdayName", default),
                new MySqlParameter("@p_bio", default),
                new MySqlParameter("@p_created_at", default),
                new MySqlParameter("@p_updated_at", default),
                new MySqlParameter("@p_statementType", "SELECT"),
            };
            
            var authorsSP = await GetDataTableFromSP("AUTHORCRUD",mysqlParameters);
            string jsonAuthors = JsonConvert.SerializeObject(authorsSP);
            return Content(jsonAuthors, "application/json");
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            
            var sqlParameters = new List<MySqlParameter>()
            {
                new MySqlParameter("@p_Id", id),
                new MySqlParameter("@p_authorName", default),
                new MySqlParameter("@p_birthdayName", default),
                new MySqlParameter("@p_bio", default),
                new MySqlParameter("@p_created_at", default),
                new MySqlParameter("@p_updated_at", default),
                new MySqlParameter("@p_statementType", "GETBYID"),
            };

            var data = new List<Author>();
            var departmentsSP = await GetDataTableFromSP("AUTHORCRUD", sqlParameters);
            var authorDto = MapToAuthorDto(departmentsSP);
            return Ok(authorDto);
        }


        [HttpPost]
        public async Task<IActionResult> Post(Author author)
        {
            var sqlParameters = new List<MySqlParameter>()
            {
                new MySqlParameter("@p_Id", author.Id),
                new MySqlParameter("@p_authorName", author.AuthorName),
                new MySqlParameter("@p_birthdayName", author.BirthdayName),
                new MySqlParameter("@p_bio", author.Bio),
                new MySqlParameter("@p_created_at", author.CreatedAt),
                new MySqlParameter("@p_updated_at", author.UpdatedAt),
                new MySqlParameter("@p_statementType", "INSERT"),
            };

            var data = new List<Author>();
            var departmentsSP = await GetDataTableFromSP("AUTHORCRUD", sqlParameters);
            return Ok();
        }

         
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            
            var sqlParameters = new List<MySqlParameter>()
            {
                new MySqlParameter("@p_Id", id),
                new MySqlParameter("@p_authorName", default),
                new MySqlParameter("@p_birthdayName", default),
                new MySqlParameter("@p_bio", default),
                new MySqlParameter("@p_created_at", default),
                new MySqlParameter("@p_updated_at", default),
                new MySqlParameter("@p_statementType", "DELETE"),
            };

            var data = new List<Author>();
            var departmentsSP = await GetDataTableFromSP("AUTHORCRUD", sqlParameters);
            return NoContent();
            // var authorDto = MapToAuthorDto(departmentsSP);
            // return Ok(authorDto);
        }

        


        private Author MapToAuthorDto(DataTable dataTable)
        {
            var firstRow = dataTable.Rows.Cast<DataRow>().FirstOrDefault();
            if (firstRow != null)
            {
                return new Author
                {
                    Id = Convert.ToInt32(firstRow["Id"]),
                    AuthorName = Convert.ToString(firstRow["AuthorName"]),
                    BirthdayName = Convert.ToDateTime(firstRow["BirthdayName"]),
                    Bio = Convert.ToString(firstRow["Bio"]),
                    CreatedAt = Convert.ToDateTime(firstRow["CreatedAt"]),
                    UpdatedAt = Convert.ToDateTime(firstRow["UpdatedAt"])
                };
            }
            return null;
        }

        private async Task<DataTable> GetDataTableFromSP(string storedProcedure, List<MySqlParameter>? sqlParameters = null)
        {
            using (var command = _applicationDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedProcedure;

                if (sqlParameters?.Count > 0)
                {
                    command.Parameters.AddRange(sqlParameters.ToArray());
                }

                await _applicationDbContext.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(result);
                    await _applicationDbContext.Database.CloseConnectionAsync();
                    return dataTable;
                }
            }
        } 




}