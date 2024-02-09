using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using store_procedure.Models;
namespace store_procedure.Controllers;

public class AuthorController : ControllerBase
{
    public readonly ILogger<AuthorController> _logger;
    private readonly storeprocedureContext _applicationDbContext;

    public AuthorController(ILogger<AuthorController> logger,storeprocedureContext applicationDbContext)
    {
        _logger = logger;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IActionResult> Index()
    {
        return Ok("ok");
    }

    public async Task<DataTable> GetDataTableFromSP(string storedProcedure,List<SqlParameter> sqlParameters)
    {
            using (var command = _applicationDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;

                command.CommandText = storedProcedure;
                command.Parameters.AddRange(sqlParameters.ToArray());
                await _applicationDbContext.Database.OpenConnectionAsync();

                using (var result = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(result);
                    await _applicationDbContext.Database.CloseConnectionAsync();
                    return dataTable;
                }
            }
    }




}