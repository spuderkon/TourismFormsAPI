namespace TourismFormsAPI.ModelsDTO.Requests
{
    public class SurveyPut
    {
        public int FormId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime CompletionDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool Completed { get; set; }

        public string MunicipalityName { get; set; } = null!;

        public int MunicipalityId { get; set; }

        public string CityName { get; set; } = null!;

        public int CityId { get; set; }
    }
}
