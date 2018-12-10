using EcomProject_JimmyRebecca.Models;
using Xunit;

namespace Sprint3_CRUD_Tests
{
    public class ApplicationUserTests
    {
        /// <summary>
        /// Tests getting Application User's FirstName property.
        /// </summary>
        [Fact]
        public void GetUserProps()
        {
            ApplicationUser au = new ApplicationUser
            {
                FirstName = "Test",
                LastName = "User"
            };

            Assert.Equal("Test", au.FirstName);
        }

        /// <summary>
        /// Test setting ApplicationUser property
        /// </summary>
        [Fact]
        public void SetUserProps()
        {
            ApplicationUser au = new ApplicationUser
            {
                FirstName = "Test",
                LastName = "User"
            };

            au.FirstName = "Daniel";
        }
    }
}
