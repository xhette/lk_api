using lk_api.LkDatabase.Models;

namespace lk_api.LkDatabase.ApiModels
{
    public class AbonentInfo
    {
        public int Id { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string PersonalNumber { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public bool SendReceiptEmail { get; set; }
        public bool SendReceiptPost { get; set; }

        public static explicit operator AbonentInfo(Abonent abonent)
        {
            if (abonent == null)
                return null;
            else return new AbonentInfo
            {
                Id = abonent.Id,
                Surname = abonent.Surname,
                Name = abonent.Name,
                Patronymic = abonent.Patronymic,
                PersonalNumber = abonent.PersonalNumber,
                Email = abonent.Email,
                Address = abonent.Address,
                Phone = abonent.Phone,
                SendReceiptEmail = abonent.SendReceiptEmail.HasValue ? abonent.SendReceiptEmail.Value : false,
                SendReceiptPost = abonent.SendReceiptPost.HasValue ? abonent.SendReceiptPost.Value : false
            };
        }

        public static explicit operator Abonent (AbonentInfo abonent)
        {
            if (abonent == null)
                return null;
            else return new Abonent
            {
                Id = abonent.Id,
                Surname = abonent.Surname,
                Name = abonent.Name,
                Patronymic = abonent.Patronymic,
                Address = abonent.Address,
                PersonalNumber = abonent.PersonalNumber,
                Email = abonent.Email,
                Phone = abonent.Phone,
                SendReceiptEmail = abonent.SendReceiptEmail,
                SendReceiptPost = abonent.SendReceiptPost
            };
        }
    }
}
