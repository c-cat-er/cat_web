using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using modpack.Models;

namespace modpack.Wrap
{
    public class CWrapAdministrator
    {

        private Administrator _administrator;

        public Administrator administrator { get { return _administrator; } set { _administrator = value; } }

        public AdministratorTitle administratorTitle { get; set; }

        public ImageCarousel ImageCarousel { get; set; }

        public ServiceRecord ServiceRecord { get; set; }

        public CWrapAdministrator()
        {
            _administrator = new Administrator();
            administratorTitle = new AdministratorTitle();
            ImageCarousel = new ImageCarousel();
            ServiceRecord = new ServiceRecord();
        }

        // ImageCarousel
        [DisplayName("序")]
        public int ImageCarouselID { get; set; }

        [DisplayName("圖片")]
        public string ImageFile { get; set; }

        [DisplayName("描述")]
        [StringLength(50, MinimumLength = 1)]
        public string Description { get; set; }

        public IFormFile photo { get; set; }


        // admin
        [DisplayName("序")]
        public int AdminID { get; set; }

        [DisplayName("部門ID")]
        public int TitleID { get; set; }

        [DisplayName("姓名")]
        public string AdminName { get; set; }

        [DisplayName("帳號")]
        public string AdminAccount { get; set; }

        [DisplayName("密碼")]
        public string AdminPassword { get; set; }


        // AdministratorTitle

        [DisplayName("序")]
        public int AdminTitleID { get; set; }

        [DisplayName("部門")]
        public string AdminTitleName { get; set; }

        [DisplayName("權限")]
        public string AdminPermissions { get; set; }


        // ServiceRecord
        [DisplayName("序")]
        public int AdminRecordID { get; set; }

        [DisplayName("會員編號")]
        public string AdminMemberID { get; set; }

        [DisplayName("提問內容")]
        public string AdminQuestion { get; set; }

        [DisplayName("時間")]
        public DateTime AdminQuestionTime { get; set; }

        //[DisplayName("回復人")]
        //public string AdminID { get; set; }

        [DisplayName("回復內容")]
        public string AdminAnswer { get; set; }

        [DisplayName("時間")]
        public DateTime AdminAnswerTime { get; set; }

        [DisplayName("是否已解決")]
        public bool AdminIsResolved { get; set; }





        public virtual ICollection<AdministratorActivitylog> AdministratorActivitylogs { get; set; } = new List<AdministratorActivitylog>();

        public virtual AdministratorTitle Title { get; set; }

        public virtual ICollection<Administrator> Administrators { get; set; } = new List<Administrator>();
    }
}
