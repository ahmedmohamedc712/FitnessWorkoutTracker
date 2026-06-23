using Application.Abstraction;
using Domain.Entities;

namespace PublicApiIntegrationTests.Helpers;

public static class DataSeedHelper
{
    public static User CreateLoginUser(IPasswordHasher passwordHasher)
    {
        return new User("test", "test@gmail.com", passwordHasher.HashPassword("Test@1234"));
    }

    public static User CreateUser()
    {
        return new User("test", "test@gmail.com", "hashedpassword");
    }
}
