using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OgretmenYonetimSistemi.Data;
using OgretmenYonetimSistemi.Models;
using OgretmenYonetimSistemi.Models.Domain;

namespace OgretmenYonetimSistemi.Controllers
{
    public class OgretmenlerController : Controller
    {
        private readonly OgtDbContext ogretmenDbContext;

        public OgretmenlerController(OgtDbContext OgretmenDbContext)
        {
            ogretmenDbContext = OgretmenDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var ogretmenler = await ogretmenDbContext.Ogretmenler.ToListAsync();
            return View(ogretmenler);
        }
        [HttpGet]
        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ekle(OgretmenEkleViewModel ogretmenEkleRequest)
        {
            var Ogretmen = new Ogretmen()
            {
                Id = Guid.NewGuid(),
                Ad = ogretmenEkleRequest.Ad,
                Soyad = ogretmenEkleRequest.Soyad,
                TcNo = ogretmenEkleRequest.TcNo,
                Email = ogretmenEkleRequest.TcNo,
                Telefon = ogretmenEkleRequest.Telefon,
                Ders = ogretmenEkleRequest.Ders,
                Maas = ogretmenEkleRequest.Maas,
            };
            await ogretmenDbContext.Ogretmenler.AddAsync(Ogretmen);
            await ogretmenDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var ogretmen = await ogretmenDbContext.Ogretmenler.FirstOrDefaultAsync(x => x.Id == id);

            if (ogretmen != null)
            {
                var viewModel = new OgretmenGuncelleViewModel()
                {
                    Id = ogretmen.Id,
                    Ad = ogretmen.Ad,
                    Soyad = ogretmen.Soyad,
                    TcNo = ogretmen.TcNo,
                    Email = ogretmen.Email,
                    Telefon = ogretmen.Telefon,
                    Ders = ogretmen.Ders,
                    Maas = ogretmen.Maas,
                };
                return await Task.Run(() => View("View", viewModel));
            }

            


            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> View(OgretmenGuncelleViewModel model)
        {
            var ogretmen = await ogretmenDbContext.Ogretmenler.FindAsync(model.Id);

            if (ogretmen != null)
            {
                ogretmen.Ad = model.Ad;
                ogretmen.Soyad = model.Soyad;
                ogretmen.TcNo = model.TcNo;
                ogretmen.Email = model.Email;
                ogretmen.Telefon = model.Telefon;
                ogretmen.Ders = model.Ders;
                ogretmen.Maas= model.Maas;

                await ogretmenDbContext.SaveChangesAsync();
                return RedirectToAction("Index");   
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(OgretmenGuncelleViewModel model)
        {
            var ogretmen = await ogretmenDbContext.Ogretmenler.FindAsync(model.Id);

            if (ogretmen !=null )
            {
                ogretmenDbContext.Ogretmenler.Remove(ogretmen);
                await ogretmenDbContext.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }
    }
}
