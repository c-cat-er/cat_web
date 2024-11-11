using System.ComponentModel;
using modpack.Models;

namespace modpack.Wrap
{
    public class CMemberWrap
    {
        private Member _member;
        public Member member { get { return _member; } set { _member = value; } }


        public CMemberWrap()
        {
            _member = new Member();
        }

        [DisplayName("會員編號")]
        public int MemberId { get; set; }

        public int LevelId { get; set; }

        public int AccountId { get; set; }

        [DisplayName("會員姓名")]
        public string Name { get { return _member.Name; } set { _member.Name = value; } }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
    }
}
