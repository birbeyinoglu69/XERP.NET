
// Toolkit namespace

//XERP namespaces
using XERP.Domain.LogInDomain.Services;
using XERP.Client.WPF.LogIn.ViewModels;


namespace XERP.Client.WPF.LogIn
{
    public class ViewModelLocator
    {
        // Create MainPageViewModel on demand
        public MainPageViewModel MainPageViewModel
        {
            get { return new MainPageViewModel(); }
        }

        public LogInViewModel LogInViewModel
        {
            get
            {
                ILogInServiceAgent serviceAgent = new LogInServiceAgent();
                return new LogInViewModel(serviceAgent);
            }
        }

    }
}