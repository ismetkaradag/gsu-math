using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using gsu_math.Models;
using gsu_math.ViewModels;

namespace gsu_math.Controllers;

public class EtkinlikController : Controller
{
    private readonly MyDbContext _context;
    private readonly IWebHostEnvironment hostingEnvironment;
    public EtkinlikController( MyDbContext context,IWebHostEnvironment hostingEnvironment)
    {
        _context = context;    
        this.hostingEnvironment = hostingEnvironment;
    }

    public IActionResult Index()
    {
        return View(_context.Etkinlik.ToList());
    }

    public IActionResult Create()
    {
        
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(EtkinlikCreateViewModel data)
    {

        if (ModelState.IsValid)
            {
                string uniquefilename = null;
                if(data.foto!=null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath,"photos");
                    uniquefilename = Guid.NewGuid().ToString() + "_" + data.foto.FileName;
                    string filePath = Path.Combine(uploadsFolder,uniquefilename);
                    data.foto.CopyTo(new FileStream(filePath,FileMode.Create));
                    System.Console.WriteLine(filePath);
                }
                Etkinlik etkinlik = new Etkinlik
                {
                    Baslik = data.Baslik,
                    Metin = data.Metin,
                    atCreated = DateTime.Now,
                    startDate = data.startDate,
                    endDate = data.endDate, 
                    Foto = uniquefilename
                };
                _context.Add(etkinlik);
                await _context.SaveChangesAsync();

                return RedirectToAction("index","etkinlik");
            }
            ModelState.AddModelError(nameof(data),"Bir hata oluştu.");
            return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult Liste()
    {
        return View(_context.Etkinlik.ToList());
    }
    public IActionResult Delete(int id)
    {
        return View(_context.Etkinlik.FirstOrDefault(p => p.EtkinlikId == id));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirme(int id)
    {
        if (ModelState.IsValid)
        {
            var er = _context.Etkinlik.FirstOrDefault(p => p.EtkinlikId == id);
            _context.Remove(er);
            _context.SaveChanges();
            return RedirectToAction("liste","etkinlik");
        }
        return RedirectToAction("error");
    }
}
