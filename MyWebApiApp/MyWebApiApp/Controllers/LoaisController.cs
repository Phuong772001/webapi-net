using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApiApp.Data;
using MyWebApiApp.Models;

namespace MyWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaisController : ControllerBase
    {
        private readonly MyDbContext _context;

        public LoaisController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var dsLoai = _context.Loais.ToList();
            return Ok(dsLoai);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var Loai = _context.Loais.SingleOrDefault(x => x.MaLoai == id);
            if (Loai!=null)
            {
                return Ok(Loai);
            }
            else
            {
                return NotFound("not found");
            }
        }

        [HttpPost]
        public IActionResult CreateNew(LoaiModel model)
        {
            try
            {
                var loai = new Loai
                {
                    TenLoai = model.TenLoai
                };
                _context.Add(loai);
                _context.SaveChanges();
                return Ok(loai);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateLoaiById(int id,LoaiModel model)
        {
            var Loai = _context.Loais.SingleOrDefault(x => x.MaLoai == id);
            if (Loai != null)
            {
                Loai.TenLoai = model.TenLoai;
                _context.SaveChanges();
                return Ok(Loai);
            } 
            else
            {
                return NotFound("not found");
            }
        }
    }
}
