using SevenDev.Domain.Core;
using System;

namespace SevenDev.Domain.Entities
{
    public class User
    {
        #region Construtor
        public User(string email,
                    string password,
                    string name,
                    DateTime birthday,
                    Gender gender,
                    string photo)
        {
            Email = email;
            CriptografyPassword(password);

            Name = name;
            Birthday = birthday;
            Gender = gender;
            Photo = photo;
        }

        public User(string name,
                     DateTime birthday,
                     Gender gender,
                     string photo)
        {
            Name = name;
            Birthday = birthday;
            Gender = gender;
            Photo = photo;
        }
        #endregion

        #region Propriedades
        public int Id { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Name { get; private set; }
        public DateTime Birthday { get; private set; }
        public Gender Gender { get; private set; }
        public string Photo { get; private set; }
        #endregion

        #region Metodo
        public bool IsValid()
        {
            bool valid = true;

            if (string.IsNullOrEmpty(Name) ||
                Birthday.ToShortDateString() == "01/01/0001" ||
                Gender.Id <= 0 ||
                string.IsNullOrEmpty(Photo))
            {
                valid = false;
            }

            return valid;
        }

        private void CriptografyPassword(string password)
        {
            Password = PasswordHasher.Hash(password);
        }

        public bool IsEqualPassword(string password)
        {
            return PasswordHasher.Verify(password, Password);
        }

        public void InformationLoginUser(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void UpdateInfo(string email,
                               string password,
                               string nome,
                               string foto,
                               int genderId)
        {
            Name = string.IsNullOrEmpty(nome) ? Name : nome;
            Photo = string.IsNullOrEmpty(foto) ? Photo : foto;
            Email = string.IsNullOrEmpty(email) ? Email : email;
            if (genderId >= 0 && genderId != Gender.Id)
            {
                Gender.SetId(genderId);
            }

            if (!string.IsNullOrEmpty(password) && password != Password)
                CriptografyPassword(password);
        }
        #endregion

    }
}
