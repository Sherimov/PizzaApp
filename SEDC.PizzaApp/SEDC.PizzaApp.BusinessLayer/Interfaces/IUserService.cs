using SEDC.PizzaApp.BusinessModels.ViewModels;

namespace SEDC.PizzaApp.BusinessLayer.Interfaces
{
    public interface IUserService
    {
        string GetLastUsername();
        void GiveFeedback(FeedbackViewModel feedbackVm);
    }
}
