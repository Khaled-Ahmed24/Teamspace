using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Security.Claims;
using Teamspace.Configurations;
using Teamspace.DTO;
using Teamspace.Models;

namespace Teamspace.Repositories
{
    [Authorize]
    public class MaterialsRepo
    {
        private AppDbContext _db;
        public MaterialsRepo(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> addMaterials(DtoMaterials dtoMaterials)
        {

            if (dtoMaterials == null) return false;
            foreach (var file in dtoMaterials.Files)
            {
                Material Material = new Material
                {
                    Name = dtoMaterials.Name,
                    CourseId = dtoMaterials.CourseId,
                    UploadedAt = DateTime.Now,
                    StaffId = dtoMaterials.StaffId
                };
                using (var stream = new MemoryStream())
                {
                    if(file != null && file.Length > 0)
                    {
                        await file.CopyToAsync(stream);
                        Material.File = stream.ToArray();
                    }
                }
                await _db.Materials.AddAsync(Material);
                await _db.SaveChangesAsync();
            }
            return true;
        }

        public async Task<List<Material>> getAllMaterials(int courseId)
        {
            return await _db.Materials.Where(s => s.CourseId == courseId).ToListAsync();
        }
        //updatedate here
        public async Task<Material?> getMaterialById(int id)
        {
            var material = await _db.Materials.FirstOrDefaultAsync(p => p.Id == id);
            return material;
        }
        // updatedate here
        public async Task<bool> DeleteMaterialById(int materialId)
        {
            var material = _db.Materials.FirstOrDefault(n => n.Id == materialId);
            if (material != null)
            {
                _db.Materials.Remove(material);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // هل مستاهلة نعملها اصلا (احذف وارفع من جديد
        //updatedate here

        /*public bool UpdateMaterial([FromForm] DtoMaterials dtoMaterials)
        {
            Material material = _db.Materials.First(s => s.Id == dtoMaterials.Id);
            if (material == null) return false;
            using var stream = new MemoryStream();
            dtoMaterials.File.CopyTo(stream);
            material.Name = dtoMaterials.Name;
            material.CourseId = dtoMaterials.CourseId;
            material.File = stream.ToArray();
            _db.SaveChanges();
            return true;
        }
        */
    }

}











