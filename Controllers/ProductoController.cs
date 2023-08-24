using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiProducto.Controllers;
using ApiProducto.Models;
using ApiProducto.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiProducto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
      private readonly DataContext _context;
        public ProductoController(DataContext context)
        {
          _context = context;
        }
        //HttpPost

        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
              var productoNew = new Producto{
                 Nombre = producto.Nombre,
                 Descripcion= producto.Descripcion,
                 Precio = producto.Precio,
                 FechaAlta = DateTime.Now,
                 Activo = true
              };

              _context.Productos.Add(productoNew);
              await _context.SaveChangesAsync();
              return Ok();
        }


        //HttpGet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Producto>> GetProducto(int Id)
        {
            var producto = await _context.Productos.FindAsync(Id);
            if(producto == null)
                 {
                  return NotFound();
                 }
            return producto;
        }


        //HttpPut


        [HttpPut("{id}")] 
        public async Task <ActionResult>PutProducto(int id, Producto producto) 
          {
      
           var productoEdit = await _context.Productos.FindAsync(id);
           if(productoEdit == null)
            {
             return NotFound();
            }



           productoEdit.Nombre = producto.Nombre;
           productoEdit.Descripcion= producto.Descripcion;
           productoEdit.Precio=producto.Precio;
           productoEdit.FechaAlta= DateTime.Now;
           productoEdit.Activo=producto.Activo;
           

           await _context.SaveChangesAsync();
           return Ok();
        }

        //HttpDelete

        [HttpDelete] 
        public async Task<ActionResult> DeleteProducto(int id)
         { 
       
             var productoDelete = await _context.Productos.FindAsync(id);
                if(productoDelete == null)
                    {
                     return NotFound();
                    }
            _context.Productos.Remove(productoDelete);
            await _context.SaveChangesAsync();
            return Ok();
         }

    }
    
}