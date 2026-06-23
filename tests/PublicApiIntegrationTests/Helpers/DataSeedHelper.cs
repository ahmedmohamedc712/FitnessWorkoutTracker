using Application.Abstraction;
using Domain.Entities;

namespace PublicApiIntegrationTests.Helpers;

public static class DataSeedHelper
{
    public static User CreateTestUser(IPasswordHasher passwordHasher)
    {
        return new User("test", "test@gmail.com", passwordHasher.HashPassword("Test@1234"));
    }
}
