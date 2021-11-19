using Archi.API.Model;
using Archi.Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archi.Library.Controllers
{
    public abstract class BaseController<TContext, TModel> : ControllerBase where TContext : DbContext where TModel : ModelBase
    {
        protected readonly TContext _context;

        public BaseController(TContext context)
        {
            _context = context;
        }

        // GET: api/{model}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TModel>>> GetAll([FromQuery] Paginator filter)
        {
            var paginator = new Paginator(filter.range, filter.current_page);
            var source = _context.Set<TModel>();
            var result1 = source.Skip((paginator.current_page - 1) * paginator.range)
            .Take(paginator.range);

            return await result1.Where(x => x.Active == true).ToListAsync();
            //return await _context.Pizzas.Where(x => x.Active == true).ToListAsync();
        }

    }
}
