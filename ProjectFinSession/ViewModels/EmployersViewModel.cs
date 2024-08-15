using ProjectFinSession.Models;

namespace ProjectFinSession.ViewModels
{
    public class EmployersViewModel
    {
        public  InscriptionViewModel InscriptionViewModel { get; set; }

        public List<AppUser> Employers { get; set; }

        public bool Admin { get; set; }

    }
}
