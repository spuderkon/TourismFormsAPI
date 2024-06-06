namespace TourismFormsAPI.ModelsDTO.Requests
{
    public class SurveyPost
    {
        public int FormId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MunicipalityId { get; set; }
        public int CityId { get; set; }
    }
}
