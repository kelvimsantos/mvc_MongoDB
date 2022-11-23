using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using mvc_MongoDB.Data;
using mvc_MongoDB.Models;

namespace mvc_MongoDB.Controllers
{
    public class UsuariosController : Controller
    {
        //usado apenas em api
        //private readonly mvc_MongoDBContext _context;

        //public UsuariosController(mvc_MongoDBContext context)
        //{
        //    _context = context;
        //}

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            ContextMongoDB dbContexty = new ContextMongoDB();
            //procura usuario, se = encontrar.lista
              return View(await dbContexty.Usuario.Find(u=> true).ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {

            if (id == null )
            {
                return NotFound();
            }

            ContextMongoDB dbContexty = new ContextMongoDB();

            var usuario = await dbContexty.Usuario.Find(u => u.Id == id).FirstOrDefaultAsync();

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                ContextMongoDB dbContexty = new ContextMongoDB();

                usuario.Id = Guid.NewGuid();
                await dbContexty.Usuario.InsertOneAsync(usuario);

                return RedirectToAction(nameof(Index));
               
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            ContextMongoDB dbContexty = new ContextMongoDB();

            var usuario = await dbContexty.Usuario.Find(u => u.Id == id).FirstOrDefaultAsync();


            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ContextMongoDB dbContexty = new ContextMongoDB();
                   await dbContexty.Usuario.ReplaceOneAsync(m => m.Id == usuario.Id, usuario);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            ContextMongoDB dbContexty = new ContextMongoDB();

            var usuario = await dbContexty.Usuario.Find(u => u.Id == id).FirstOrDefaultAsync();


            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            ContextMongoDB dbContexty = new ContextMongoDB();
            await dbContexty.Usuario.DeleteOneAsync(u => u.Id == id);
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(Guid id)
        {
            ContextMongoDB dbContexty = new ContextMongoDB();

            var usuario = dbContexty.Usuario.Find(u => u.Id == id).Any();
            return usuario;


        }
    }
}
