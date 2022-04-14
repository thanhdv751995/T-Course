using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Project.NguoiDungs
{
    public class UserManager : DomainService
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateAsync(
            [NotNull] string name,
            DateTime birthDay,
            [NotNull] string email,
            [NotNull] string phone,
            [NotNull] string username,
            [NotNull] string password,
            [NotNull] Guid idRole
            )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingNguoiDung = await _userRepository.FindByNameAsync(name);
            var existingUsername = await _userRepository.FindByUsernameAsync(username);
            var EmailUsername = await _userRepository.FindByEmailAsync(email);
            if (existingNguoiDung != null)
            {
                throw new UserAlreadyExistsException(name);
            }
            if (existingUsername != null)
            {
                throw new UsernameAlreadyExistsException(username);
            }
            if (existingUsername != null)
            {
                throw new UsernameAlreadyExistsException(username);
            }
            if (EmailUsername != null)
            {
                throw new EmailAlreadyExistsException(email);
            }
            return new User(
                GuidGenerator.Create(),
                name,
                birthDay,
                email,
                phone,
                username,
                password,
                idRole
            );
        }

        public async Task ChangeNameAsync(
            [NotNull] User user,
            [NotNull] string newname)
        {
            Check.NotNull(user, nameof(user));
            Check.NotNullOrWhiteSpace(newname, nameof(newname));

            var existingUser = await _userRepository.FindByNameAsync(newname);
            if (existingUser != null && existingUser.Id != user.Id)
            {
                throw new UserAlreadyExistsException(newname);
            }

            user.ChangeName(newname);
        }
    }
}
