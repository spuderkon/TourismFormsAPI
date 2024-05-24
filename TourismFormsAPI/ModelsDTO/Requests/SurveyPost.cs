namespace TourismFormsAPI.ModelsDTO.Requests
{
    public class SurveyPost
    {
        public int FormId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Completed { get; set; }
        public int MunicipalityId { get; set; }
        public int CityId { get; set; }
    }
}
