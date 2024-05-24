namespace TourismFormsAPI.ModelsDTO.Requests
{
    public class MunicipalityPost
    {
        public string Name { get; set; } = null!;

        public int RegionId { get; set; }

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool IsAdmin { get; set; }
    }
}
