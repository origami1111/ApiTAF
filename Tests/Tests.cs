using ApiTAF.Entities;
using ApiTAF.Services;
using ApiTAF.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;

namespace ApiTAF.Tests
{
    [Parallelizable(ParallelScope.All)]
    public class Tests
    {
        private ServiceCalls serviceCalls;

        [SetUp]
        public void Setup()
        {
            serviceCalls = new ServiceCalls();
        }

        [Test(Description = "Tasks #1. Validate that the list of users can be received successfully")]
        [Category("API")]
        public async Task ValidateThatListOfUsersReceivedSuccessfully()
        {
            string usersJsonSchema = await ReadJsonShemaFromFile.ReadJsonSchemaFromFileAsync(Config.GetUsersFilePath);
            var response = await serviceCalls.GetUsersAsync();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Response status code is not 200 OK");
            await CheckJsonSchema.CheckShemaAsync(response, usersJsonSchema);
        }

        [Test(Description = "Tasks #2. Validate response header for a list of users")]
        [Category("API")]
        public async Task ValidateResponseHeaderForListOfUsers()
        {
            var response = await serviceCalls.GetUsersAsync();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Response status code is not 200 OK");
            Assert.That(response.ContentType, Is.Not.Null, "Response Content-Type is null");
            Assert.That(response.ContentType, Is.EqualTo("application/json; charset=utf-8"), "Response Content-Type is not equal to 'application/json; charset=utf-8'");
        }

        [Test(Description = "Task #3.")]
        [Category("API")]
        public async Task ValidateThatUserShouldBeWithNonEmptyFields()
        {
            var response = await serviceCalls.GetUsersAsync();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Response status code is not 200 OK.");

            List<User> users = JsonConvert.DeserializeObject<List<User>>(response.Content);

            Assert.That(users.Count, Is.EqualTo(10), "The response body does not contain an array of 10 users.");

            List<int> userIds = new List<int>();

            // Iterate through the users and validate their properties
            foreach (User user in users)
            {
                userIds.Add(user.Id);

                Assert.That(string.IsNullOrEmpty(user.Name), Is.False, $"User {user.Id} has an empty Name.");
                Assert.That(string.IsNullOrEmpty(user.Username), Is.False, $"User {user.Id} has an empty Username.");
                Assert.That(string.IsNullOrEmpty(user.Company.Name), Is.False, $"User {user.Id} has an empty Company Name.");
            }

            Assert.That(userIds.Distinct().Count(), Is.EqualTo(10), "Some users have the same ID.");
        }

        [Test(Description = "Task #4. Validate that user can be created")]
        [TestCase("slava", "slava007")]
        [Category("API")]
        public async Task ValidateThatUserCanBeCreated(string name, string username)
        {
            var response = await serviceCalls.PostUserAsync(name, username);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created), "Response status code is not 201 Created.");

            ResponsePostUser createdUser = JsonConvert.DeserializeObject<ResponsePostUser>(response.Content);

            Assert.That(createdUser, Is.Not.Null, "The created user is null.");
            Assert.That(createdUser.Id, Is.GreaterThan(0), "The created user has invalid or missing ID.");
            Assert.That(createdUser.Name, Is.EqualTo(name), $"The created user's name is not '{name}'.");
            Assert.That(createdUser.Username, Is.EqualTo(username), $"The created user's username is not '{username}'.");
        }

        [Test(Description = "Task #5. Validate that user is notified if resource doesn’t exist")]
        [Category("API")]
        public async Task ValidateThatUserIsNotifiedIfResourceDoesNotExist()
        {
            var response = await serviceCalls.GetInvalidResourceAsync();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), "Response status code is not 404 Not Found");
        }
    }
}