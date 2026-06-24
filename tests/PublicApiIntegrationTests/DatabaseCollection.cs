namespace PublicApiIntegrationTests;

[CollectionDefinition("Database Shared Collection")]
public class DatabaseCollection : ICollectionFixture<CustomWebApplicationFactory>
{
    // This class doesn't contain code. It's just a placeholder 
    // to give the collection a name and attach the WebApplicationFactory.

    // it ensures that we have one shared WebApplicationFactory for test classes in the collection.
}