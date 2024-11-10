using Microsoft.AspNetCore.Mvc;
using modpack.Models;
using modpack.ViewModels;
using modpack.Wrap;

namespace modpack.Controllers
{
    public class PromoCodeController : Controller
    {
        public IActionResult List()
        {
            ModPackContext db = new ModPackContext();
            var datas = from p in db.PromoCodes
                        select new CPromoCodeWrap
                        {
                            PromoCodeId = p.PromoCodeId,
                            Code = p.Code,
                            Method = p.Method,
                            Description = p.Description,
                            Limitation = p.Limitation,
                            StartDate = p.StartDate,
                            EndDate = p.EndDate,
                            Status = p.Status,
                        };
            return View(datas);
        }

        public IActionResult Edit(int? id) 
        {
            ModPackContext db = new ModPackContext();
            PromoCode pc = db.PromoCodes.FirstOrDefault(p=> p.PromoCodeId== id);
            if (pc == null)
                return RedirectToAction("List");
            CPromoCodeWrap cpc = new CPromoCodeWrap();
            cpc.promoCode = pc;
            return View(cpc);
        }
        [HttpPost]
        public IActionResult Edit(CPromoCodeWrap cpcIn)
        {
            ModPackContext db = new ModPackContext();
            PromoCode cpcEdit = db.PromoCodes.FirstOrDefault(p => p.PromoCodeId == cpcIn.PromoCodeId);
            if(cpcEdit != null)
            {
                cpcEdit.Code = cpcIn.Code;
                cpcEdit.Method = cpcIn.Method;
                cpcEdit.Description = cpcIn.Description;
                cpcEdit.Limitation = cpcIn.Limitation;
                cpcEdit.StartDate = cpcIn.StartDate;
                cpcEdit.EndDate = cpcIn.EndDate;
                cpcEdit.Status = cpcIn.Status;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PromoCode cpc)
        {
            ModPackContext db = new ModPackContext();
            db.PromoCodes.Add(cpc);
            db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
