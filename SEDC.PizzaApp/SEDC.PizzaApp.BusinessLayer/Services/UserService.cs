using SEDC.PizzaApp.BusinessLayer.Interfaces;
using SEDC.PizzaApp.BusinessModels.ViewModels;
using SEDC.PizzaApp.DataAccess.Repositories;
using SEDC.PizzaApp.Domain.Models;
using System.Linq;

namespace SEDC.PizzaApp.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Feedback> _feedbackRepository;

        public UserService(IRepository<User> userRepository,
            IRepository<Feedback> feedbackRepository)
        {
            _userRepository = userRepository;
            _feedbackRepository = feedbackRepository;
        }

        public string GetLastUsername()
        {
            return _userRepository.GetAll().LastOrDefault().FirstName;
        }

        public void GiveFeedback(FeedbackViewModel feedbackVm)
        {
            Feedback feedback = new Feedback()
            {
                Email = feedbackVm.Email,
                Message = feedbackVm.Message,
                Name = feedbackVm.Name
            };
            _feedbackRepository.Insert(feedback);
        }
    }
}
