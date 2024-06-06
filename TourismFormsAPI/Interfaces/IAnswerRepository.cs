﻿

using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Interfaces
{
    public interface IAnswerRepository
    {
        public Task SaveMyAll(AnswerPost[] body);
        public Task UpdateMyAll(AnswerPut[] body);
    }
}
