namespace Pet.Domain.Interfaces;

public interface IPetApi
{
    [Put("/pet")]
    Task<ApiResponse<PetDto>> UpdatePetAsync([Body] PetDto pet);

    [Post("/pet")]
    Task<ApiResponse<PetDto>> CreatePetAsync([Body] PetDto pet);

    [Get("/pet/findByStatus")]
    Task<ApiResponse<List<PetDto>>> GetPetsByStatusAsync([Query] string status);

    [Get("/pet/findByTags")]
    Task<ApiResponse<List<PetDto>>> GetPetsByTagsAsync([Query] List<string> tags);

    [Get("/pet/{petId}")]
    Task<ApiResponse<PetDto>> GetPetByIdAsync(int petId);

    [Post("/pet/{petId}")]
    Task<ApiResponse<PetDto>> UpdatePetWithFormAsync(int petId, [Query] string name, [Query] string status);

    [Delete("/pet/{petId}")]
    Task<IApiResponse> DeletePetAsync(int petId);

    [Post("/pet/{petId}/uploadImage")]
    Task<ApiResponse<PetDto>> UploadImageAsync(int petId, [Body] string additionalMetadata);
}
