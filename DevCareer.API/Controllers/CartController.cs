using System;
using DevCareer.Data.Data;
using Microsoft.AspNetCore.Mvc;

namespace DevCareer.API.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly DevCareerDbContext _context;
        public CartController(DevCareerDbContext devCareerDbContext)
        {
            _context = devCareerDbContext;
        }


    }

}
