internal class TestData
{
    public PetDto GetRandomPet()
    {
        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        return new PetDto
        {
            Id = Random.Shared.Next(1, 10),
            Name = $"Pet_{timestamp}",
            Category = GetRandomPetCategory(),
            PhotoUrls = new List<string> { "https://example.com/photo1.jpg", "https://example.com/photo2.jpg" },
            Tags = GetRandomPetTags(),
            Status = GetRandomPetStatus()
        };
    }

    public string GetRandomPetStatus()
    {
        var petStatuses = new[] { PetStatus.Available, PetStatus.Pending, PetStatus.Sold };
        var index = Random.Shared.Next(0, petStatuses.Length);
        return petStatuses[index].ToString().ToLowerInvariant();
    }

    public List<Tag> GetRandomPetTags()
    {
        var possibleTags = new[]
        {
            new Tag { Id = 1, Name = "friendly" },
            new Tag { Id = 2, Name = "trained" },
            new Tag { Id = 3, Name = "playful" },
            new Tag { Id = 4, Name = "aggressive" },
            new Tag { Id = 5, Name = "quiet" }
        };
        int tagCount = Random.Shared.Next(1, possibleTags.Length + 1);
        return possibleTags.OrderBy(_ => Random.Shared.Next()).Take(tagCount).ToList();
    }

    private Category GetRandomPetCategory()
    {
        var categories = new[]
        {
            new Category { Id = 1, Name = "Dog" },
            new Category { Id = 2, Name = "Cat" },
            new Category { Id = 3, Name = "Bird" },
            new Category { Id = 4, Name = "Fish" },
            new Category { Id = 5, Name = "Reptile" }
        };
        return categories[Random.Shared.Next(0, categories.Length)];
    }
}