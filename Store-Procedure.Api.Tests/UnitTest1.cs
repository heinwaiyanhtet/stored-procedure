namespace unit_test;



public class UnitTest1
{
    [Fact]
    public void TestStartsWithUpper()
    {
        // Tests that we expect to return true.
        var processor = new TicketBookingRequestProcessor();

            var request = new TicketBookingRequest
            {
                FirstName = "Abdul",
                LastName = "Rahman",
                Email = "abdulrahman@demo.com"
            };

            // Act
            TicketBookingResponse response = processor.Book(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(request.FirstName, response.FirstName);
            Assert.Equal(request.LastName, response.LastName);
            Assert.Equal(request.Email, response.Email);
    }
}