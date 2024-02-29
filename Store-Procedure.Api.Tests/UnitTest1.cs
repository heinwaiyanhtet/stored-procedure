using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using store_procedure.Controllers;
using store_procedure.Models;
using Xunit;
using MySql.Data.MySqlClient;  // Add this line
using store_procedure.Models;
using Microsoft.EntityFrameworkCore;


namespace YourProject.Tests
{
    public class AuthorControllerTests
    {
        
        [Fact]
        public async Task Get_ReturnsAuthors_Success()
        {
            // Arrange
            // var loggerMock = new Mock<ILogger<AuthorController>>();
            // var contextMock = new Mock<storeprocedureContext>();
            // var controller = new AuthorController(loggerMock.Object, contextMock.Object);

            // var dataTable = new DataTable();
            // dataTable.Columns.Add("Id", typeof(int));
            // dataTable.Columns.Add("AuthorName", typeof(string));
            // dataTable.Columns.Add("BirthdayName", typeof(DateTime));
            // dataTable.Columns.Add("Bio", typeof(string));
            // dataTable.Columns.Add("CreatedAt", typeof(DateTime));
            // dataTable.Columns.Add("UpdatedAt", typeof(DateTime));

            // // Add sample data to the DataTable
            // dataTable.Rows.Add(1, "Author1", DateTime.Now, "Bio1", DateTime.Now, DateTime.Now);
            // contextMock.Setup(db => db.Database.OpenConnectionAsync()).Verifiable();
            // contextMock.Setup(db => db.Database.CloseConnectionAsync()).Verifiable();
            // contextMock.Setup(db => db.Database.GetDbConnection().CreateCommand().ExecuteReaderAsync())
            //     .ReturnsAsync(new Mock<IDataReader>().Object)
            //     .Verifiable();

            // contextMock.Setup(db => db.Database.GetDbConnection().CreateCommand().ExecuteReaderAsync())
            //     .ReturnsAsync(new Mock<IDataReader>().Object)
            //     .Verifiable();

            // // Act
            // var result = await controller.Get();

            // // Assert
            // Assert.IsType<ContentResult>(result);
            // var contentResult = (ContentResult)result;
            // Assert.Equal("application/json", contentResult.ContentType);

            // contextMock.Verify();
        }
    }
}
