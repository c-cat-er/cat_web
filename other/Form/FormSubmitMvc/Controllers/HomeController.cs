using FormSubmit.Models;
using FormSubmit.VM;
using FormSubmit.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Humanizer;

namespace FormSubmit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult FormSubmit()
        {
            return View();
        }

        ///POST: /Home/FormSubmit
        [HttpPost]
        public ActionResult FormSubmit(string name, string email)
        {///��X�찣����X���f.

            System.Diagnostics.Debug.WriteLine("�m�W�G" + name);
            System.Diagnostics.Debug.WriteLine("�l�c�G" + email);

            return RedirectToAction("SubmitSuccess");
        }

        public IActionResult FormSubmit2()
        {
            return View();
        }

        ///POST: /Home/FormSubmit2
        [HttpPost]
        public IActionResult FormSubmit2(tUser tUser)
        {///�x�s�� Model.

            return RedirectToAction("SubmitSuccess");
        }

        public IActionResult FormSubmit3()
        {
            return View();
        }

        ///POST: /Home/FormSubmit3
        [HttpPost]
        public IActionResult FormSubmit3(TUserVM userVM)
        {///use VM

            tUser tUser = new()
            {
                Name = userVM.UserName,
                Email = userVM.Email,
            };
            //save to db.
            return RedirectToAction("SubmitSuccess");
        }

        public IActionResult FormSubmit4()
        {
            return View();
        }

        ///POST: /Home/FormSubmit4
        [HttpPost]
        public async Task<IActionResult> FormSubmit4(TUserDTO userDTO)
        {///use API + DTO

            if (!ModelState.IsValid) return View(userDTO);

            //�O�s���檺�ƾڨ�|��
            HttpContext.Session.SetString("SubmittedUserName", userDTO.Name);
            Console.WriteLine(userDTO.Name);
            HttpContext.Session.SetString("SubmittedEmail", userDTO.Email);
            Console.WriteLine(userDTO.Email);

            ///HttpClient to Api
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7282/api/Home", userDTO);
            if (response.IsSuccessStatusCode) return RedirectToAction("SubmitSuccess2");
            else
            {
                ///�Y�ШD���ѡAŪ�����~�H������ܨ���Ϥ�.
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", errorMessage);
                return View(userDTO);   ///���s��ܪ�歶��.
            }
        }


        /*
         ������ҡG �ϥθ�ƪ`�ѡ]Data Annotations�^��Fluent���ҡ]Fluent Validation�^���޳N�ӹ�{������ҡA�H�T�O�Τ��J����ƲŦX�n�D�C
�A�ȼh�G �N�~���޿��H��A�ȼh���A�H�����N�X���i���թʡB�iŪ�ʩM�i���@�ʡC�b������եΪA�ȼh����k�Ӱ���~���޿�A�Ӥ��O�b�������������~���޿�C
�D�P�B�ާ@�G ���ݭn���ݥ~���귽�]�p��Ʈw�BWeb�A�Ⱦ����^�T�����ާ@�A�ϥβ��B�ާ@�Ӵ������ε{�����ʯ�M�]�R�q�C�b����ʧ@��k���ϥβ��B�׹��š]async�^�M�D�P�B��k�Ӱ���o�Ǿާ@�C
�̿�`�J�G �ϥΨ̿�`�J�e���]Dependency Injection Container�^�Ӻ޲z���ε{�������̿����Y�A�H��{�P�����X�M������ժ��N�X�C�b������q�L�c�y�禡�`�J�A�Ȩ̿�A�Ӥ��O�b����������ЫتA�ȹ�ҡC
��x�O���G �ϥΤ�x�O���ج[�]�pSerilog�BNLog���^�ӰO�����ε{�������ާ@�M���~�A�H�K�l�ܩM�ոհ��D�C
�Τ@�����~�B�z�G �ϥβΤ@�����~�B�z����ӳB�z���ε{���������~�A�H���Ѥ@�P���Τ�����M��K�����~�l�ܡC
�椸���աG �s�g�椸���ըӴ��ձ���B�A�ȼh�M��L�ե󪺦欰�A�H�T�O���̥��T�a����w�����\��C
         
         
         */


        public ActionResult SubmitSuccess()
        {
            return View();
        }

        public ActionResult SubmitSuccess2()
        {
            //�q�|�ܤ��˯��ƾ�
            var submittedUserName = HttpContext.Session.GetString("SubmittedUserName");
            var submittedEmail = HttpContext.Session.GetString("SubmittedEmail");

            var vm = new TUserVM { UserName = submittedUserName, Email = submittedEmail };
            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
