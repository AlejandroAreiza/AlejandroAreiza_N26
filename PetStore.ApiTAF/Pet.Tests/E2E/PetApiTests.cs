namespace Pet.Tests.E2E;

[AllureNUnit]
[TestFixture]
[AllureFeature("Pet Api Tests")]
public class PetApiTests : BaseTest
{
    [AllureFeature("Regression")]
    [Test, TestCaseSource(typeof(UpdatePetData), nameof(UpdatePetData.Pet))]
    public async Task GivenExistingPet_WhenUpdatingPet_ShouldReturnSuccessAndResultMatchesInputData(PetDto pet)
    {
        var result = await PetApi.UpdatePetAsync(pet);
        var updatedPet = result.Content;

        Assert.Multiple(() =>
        {
            result.StatusCode.Should().BeOneOf(HttpStatusCode.OK);
            updatedPet.Should().NotBeNull();
            updatedPet.Id.Should().Be(pet.Id);
            updatedPet.Name.Should().Be(pet.Name);
            updatedPet.Category.Should().BeEquivalentTo(pet.Category);
            updatedPet.Tags.Should().BeEquivalentTo(pet.Tags);
            updatedPet.Status.Should().Be(pet.Status);
        });
    }

    [AllureFeature("Regression")]
    [Test]
    public async Task GivenInvalidPet_WhenUpdatingPet_ShouldReturnsBadRequest()
    {
        var pet = new PetDto
        {
            Id = 999999
        };
        var result = await PetApi.UpdatePetAsync(pet);

        Assert.Multiple(() =>
        {
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            result.Error?.Content.Should().Be("Pet not found");
        });
    }

    [AllureFeature("Regression")]
    [Test, TestCaseSource(typeof(NewPetData), nameof(NewPetData.Pet))]
    public async Task GivenValidPetDetails_WhenCreatingPet_ShouldReturnsSuccessAndResultMatchesInputData(PetDto pet)
    {
        var result = await PetApi.CreatePetAsync(pet);
        var createdPet = result.Content;

        Assert.Multiple(() =>
        {
            result.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.Created);
            createdPet.Should().NotBeNull();
            createdPet.Id.Should().Be(pet.Id);
            createdPet.Name.Should().Be(pet.Name);
            createdPet.Category.Should().BeEquivalentTo(pet.Category);
            createdPet.Tags.Should().BeEquivalentTo(pet.Tags);
            createdPet.Status.Should().Be(pet.Status);
        });
    }

    [AllureFeature("Regression")]
    [Test, TestCaseSource(typeof(NewPetData), nameof(NewPetData.Pet))]
    public async Task GivenInValidPet_WhenCreatingPet_ShouldReturnBadRequest(PetDto pet)
    {
        pet.Id = -1;
        var result = await PetApi.CreatePetAsync(pet);
        var createdPet = result.Content;

        Assert.Multiple(() =>
        {
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Error?.Content.Should().Be("Invalid input");
        });
    }

    [AllureFeature("Regression")]
    [Test, TestCaseSource(typeof(PetStatusData), nameof(PetStatusData.RandomStatus))]
    public async Task GivenAValidStatus_WhenGettingPetsByStatus_ShouldReturnsSuccessWithListOfPetsMatchingTheStatus(string status)
    {
        var result = await PetApi.GetPetsByStatusAsync(status);

        Assert.Multiple(() =>
        {
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Content.Should().NotBeNullOrEmpty();
            result.Content.Should().AllSatisfy(p => p.Status.Should().Be(status));
        });
    }

    [AllureFeature("Regression")]
    [Test, TestCaseSource(typeof(PetTagsData), nameof(PetTagsData.RandomTags))]
    public async Task GivenSomeTags_WhenGettingPetsByTags_ShouldReturnsSuccessWithListOfPetsWithMatchingTags(List<string> tags)
    {
        var result = await PetApi.GetPetsByTagsAsync(tags);

        Assert.Multiple(() =>
        {
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Content.Should().NotBeNull();
            result.Content.Should().AllSatisfy(p =>
                p.Tags.Should().Contain(tag => tags.Contains(tag.Name))
            );
        });
    }

    [AllureFeature("Regression")]
    [Test, TestCaseSource(typeof(PetIdData), nameof(PetIdData.RandomIds))]
    public async Task GivenValidPetId_WhenGettingPet_ShouldReturnsSuccess(int petId)
    {
        var result = await PetApi.GetPetByIdAsync(petId);
        Assert.Multiple(() =>
        {
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Content.Should().NotBeNull();
            result.Content.Should().BeOfType<PetDto>();
            result.Content.Id.Should().Be(petId);
            result.Content.Name.Should().NotBeNullOrEmpty();
        });
    }

    [AllureFeature("Regression")]
    [Test]
    public async Task GivenInvalidPetId_WhenGettingPet_ShouldReturnsNotFound()
    {
        var petId = 999999;
        var result = await PetApi.GetPetByIdAsync(petId);
        Assert.Multiple(() =>
        {
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            result.Error?.Content.Should().Be("Pet not found");
        });
    }

    [AllureFeature("Regression")]
    [Test, TestCaseSource(typeof(PetFormData), nameof(PetFormData.FormPets))]
    public async Task GivenValidPetData_WhenUpdatingPetWithForm_ShouldReturnsSuccessPetWithMatchingForm(PetDto pet)
    {
        var result = await PetApi.UpdatePetWithFormAsync(pet.Id, pet.Name, pet.Status);
        Assert.Multiple(() =>
        {
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Content.Should().NotBeNull();
            result.Content.Name.Should().Be(pet.Name);
            result.Content.Status.Should().Be(pet.Status);
        });
    }

    [AllureFeature("Regression")]
    [Test, TestCaseSource(typeof(PetIdData), nameof(PetIdData.RandomIds))]
    public async Task GivenValidPetId_WhenDeletingPet_ShouldReturnsSuccess(int petId)
    {
        var result = await PetApi.DeletePetAsync(petId);
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [AllureFeature("Regression")]
    [Test]
    public async Task GivenInvalidPetId_WhenDeletingPet_ShouldReturnsNotFound()
    {
        var petId = 999999;
        var result = await PetApi.DeletePetAsync(petId);
        Assert.Multiple(() =>
        {
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            result.Error?.Content.Should().Be("Pet not found");
        });
    }
}
