using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApiApp.Models;

namespace MyWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        public static List<HangHoa> HangHoas = new List<HangHoa>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(HangHoas);
        }
        [HttpGet("Get")]
        public IActionResult GetAllAllPrice()
        {
            var hangHoa = HangHoas.Where(x => x.DonGia > 400);
            return Ok(hangHoa);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {

            try
            {
                var hangHoa = HangHoas.Where(x => x.DonGia > 400 && x.TenHangHoa == "Phuong").SingleOrDefault(x => x.MaHangHoa == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound("không đúng điều kiện ");
                }

                return Ok(hangHoa);
            }
            catch
            {
                return BadRequest("khong tim thay");
            }
        }
        [HttpPost]
        public IActionResult Create(HangHoaVm hangHoaVm)
        {
            var hangHoa = new HangHoa
            {
                MaHangHoa = Guid.NewGuid(),
                TenHangHoa = hangHoaVm.TenHangHoa,
                DonGia = hangHoaVm.DonGia
            };
            HangHoas.Add(hangHoa);
            return Ok(
                new
                {
                    Success = true,
                    Data = hangHoa
                }
                );
        }

        [HttpPut("{id}")]
        public IActionResult Edit(string id, HangHoa hangHoaEdit)
        {
            try
            {
                var hangHoa = HangHoas.SingleOrDefault(x => x.MaHangHoa == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound("không đúng điều kiện ");
                }

                if (id != hangHoa.MaHangHoa.ToString())
                {
                    return BadRequest();
                }
                //Update
                hangHoa.TenHangHoa = hangHoaEdit.TenHangHoa;
                hangHoa.DonGia = hangHoaEdit.DonGia;
                return Ok();
            }
            catch
            {
                return BadRequest("khong tim thay");
            }
        }

        [HttpDelete]
        public IActionResult Remove(string id)
        {
            try
            {
                var hangHoa = HangHoas.SingleOrDefault(x => x.MaHangHoa == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound("không đúng điều kiện ");
                }

                HangHoas.Remove(hangHoa);
                return Ok();
            }
            catch
            {
                return BadRequest("khong tim thay");
            }
        }
    }
}
