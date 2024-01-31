using Repositories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<LoginDTO?> getUserforLogin(LoginDTO loginDTO);
        Task<int> UpsRegistration(AccountDTO model);

        Task<int> UspContact(ContactDTO model);
        Task<ForgotPasswordDTO?> UspForgetPassword(ForgotPasswordDTO model);
        Task<AccountDTO?> UspGetProfile(int? id);

		void UpdateProfile(AccountDTO model);
		Task ActivedAccount(int? id);

		Task<int> Uspgetuseridbyemail(AccountDTO model);
	}
}
