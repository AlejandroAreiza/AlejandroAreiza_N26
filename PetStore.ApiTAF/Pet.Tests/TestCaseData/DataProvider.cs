namespace Pet.Tests.Data;

public class UpdatePetData
{
    public static IEnumerable Pet
    {
        get
        {
            var provider = new TestData();
            yield return new object[] { provider.GetRandomPet() };
        }
    }
}

public class NewPetData
{
    public static IEnumerable Pet
    {
        get
        {
            var provider = new TestData();
            var pet = provider.GetRandomPet();
            pet.Id = 0;
            yield return new object[] { pet };
        }
    }
}

public class PetStatusData
{
    public static IEnumerable RandomStatus
    {
        get
        {
            var provider = new TestData();
            yield return new object[] { provider.GetRandomPetStatus() };
        }
    }
}

public class PetTagsData
{
    public static IEnumerable RandomTags
    {
        get
        {
            var provider = new TestData();
            yield return new object[] { new List<string> { "friendly" } };
        }
    }
}

public class PetIdData
{
    public static IEnumerable RandomIds
    {
        get
        {
            yield return new object[] { Random.Shared.Next(1, 10) };
        }
    }
}

public class PetFormData
{
    public static IEnumerable FormPets
    {
        get
        {
            var provider = new TestData();
            yield return new object[] { provider.GetRandomPet() };
        }
    }
}
