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
        private readonly ILogger<AuthorController> _logger;
        private readonly storeprocedureContext _db;

        public AuthorController(ILogger<AuthorController> logger,storeprocedureContext applicationDbContext)
        {
            _logger = logger;
            _db = applicationDbContext;
        }



        public class MysqlParameterBuilder
        {
                private readonly List<MySqlParameter> _mySqlParameters;

                public MysqlParameterBuilder()
                {
                    _mySqlParameters = new  List<MySqlParameter>()
                    {
                        new("@p_Id", default),
                        new("@p_authorName", ""),
                        new("@p_birthdayName", null),
                        new("@p_bio", ""),
                        new("@p_created_at", null),
                        new("@p_updated_at", null),
                        new("@p_statementType", "SELECT"),
                    };
                }

                public MysqlParameterBuilder Id(int id)
                {
                    _mySqlParameters[0].Value = id;
                    return this;
                }

                public MysqlParameterBuilder AuthorName(string? authorName)
                {
                    _mySqlParameters[1].Value = authorName;
                    return this;
                }

                public MysqlParameterBuilder BirthdayName(DateTime? birthdayName)
                {
                    _mySqlParameters[2].Value = birthdayName;
                    return this;
                }

                public MysqlParameterBuilder Bio(string? bio)
                {
                    _mySqlParameters[3].Value = bio;
                    return this;
                }
               
                public MysqlParameterBuilder CreatedAt(DateTime? dateTime){
                    _mySqlParameters[4].Value = dateTime;
                    return this;
                }

                public MysqlParameterBuilder UpdatedAt(DateTime? dateTime){
                    _mySqlParameters[5].Value = dateTime;
                    return this;
                }

                public MysqlParameterBuilder StateMentType(string type){
                    _mySqlParameters[6].Value = type;
                    return this;
                }
           
                public List<MySqlParameter> Build()
                {
                    return _mySqlParameters;
                }
        }
 

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var mysqlParameters = new MysqlParameterBuilder()
                                     .Build();
            
            var authorsSP = await GetDataTableFromSP("AUTHORCRUD",mysqlParameters);
            string jsonAuthors = JsonConvert.SerializeObject(authorsSP);
            return Content(jsonAuthors, "application/json");
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var getCurrentAuthor = _db.Authors.FirstOrDefault(a => a.Id == id);

            if(getCurrentAuthor == null){
                return BadRequest("author does not exists");
            }

            var mysqlParameters = new MysqlParameterBuilder()
                                    .Id(id)
                                    .StateMentType("GETBYID")
                                    .Build();

            var data = new List<Author>();
            var departmentsSP = await GetDataTableFromSP("AUTHORCRUD", mysqlParameters);
            var authorDto = MapToAuthorDto(departmentsSP);
            return Ok(authorDto);
        }


        [HttpPost]
        public async Task<IActionResult> Post(Author author)
        {

            var mySqlParameters = new MysqlParameterBuilder()
                                .Id(author.Id)
                                .AuthorName(author.AuthorName)
                                .BirthdayName(author.BirthdayName)
                                .Bio(author.Bio)
                                .CreatedAt(author.CreatedAt)
                                .UpdatedAt(author.UpdatedAt)
                                .StateMentType("INSERT")
                                .Build();

            var data = new List<Author>();
            var departmentsSP = await GetDataTableFromSP("AUTHORCRUD", mySqlParameters);
            return Ok();
        }

         
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {

            var getCurrentAuthor = _db.Authors.FirstOrDefault(a => a.Id == id);

            if(getCurrentAuthor == null){
                return BadRequest("author does not exists");
            }

            var mysqlParameters = new MysqlParameterBuilder()
                                    .Id(id)
                                    .StateMentType("DELETE")
                                    .Build();

            var data = new List<Author>();
            var departmentsSP = await GetDataTableFromSP("AUTHORCRUD", mysqlParameters);
            return NoContent();
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Author author)
        {
            var getCurrentAuthor = _db.Authors.FirstOrDefault(a => a.Id == author.Id);

            if(getCurrentAuthor != null)
            {
                string? authorName = author.AuthorName ?? getCurrentAuthor.AuthorName;
                DateTime? BirthdayName = author.BirthdayName ?? author.BirthdayName;
                string? Bio = author.Bio ?? author.Bio;
                DateTime? CreatedAt = author.CreatedAt ?? author.CreatedAt;
                DateTime? UpdatedAt = author.UpdatedAt ?? author.UpdatedAt;
                

                var mySqlParameters = new MysqlParameterBuilder()
                            .Id(author.Id)
                            .AuthorName(authorName)
                            .BirthdayName(BirthdayName)
                            .Bio(Bio)
                            .CreatedAt(CreatedAt)
                            .UpdatedAt(UpdatedAt)
                            .StateMentType("UPDATE")
                            .Build();

                var departmentsSP = await GetDataTableFromSP("AUTHORCRUD", mySqlParameters);
                return Ok();
            }

            return BadRequest("id not foud!");
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
            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedProcedure;

                if (sqlParameters?.Count > 0)
                {
                    command.Parameters.AddRange(sqlParameters.ToArray());
                }

                await _db.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(result);
                    await _db.Database.CloseConnectionAsync();
                    return dataTable;
                }
            }
        } 




}