namespace Pet.Domain.DTOs;
public class PetDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("category")]
    public Category Category { get; set; }

    [JsonPropertyName("photoUrls")]
    public List<string> PhotoUrls { get; set; }

    [JsonPropertyName("tags")]
    public List<Tag> Tags { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }
}
public class Category
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}
public class Tag
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}
