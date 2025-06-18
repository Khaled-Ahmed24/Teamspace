using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public bool addMaterials([FromForm] DtoMaterials dtoMaterials)
        {
            using var stream = new MemoryStream();
            dtoMaterials.File.CopyTo(stream);
            if (dtoMaterials == null) return false;
            Material Material = new Material
            {
                Name = dtoMaterials.Name,
                File = stream.ToArray(),
                CourseId = dtoMaterials.CourseId,
                UploadedAt = DateTime.Now,
                StaffId = dtoMaterials.StaffId
            };
            _db.Materials.Add(Material);
            _db.SaveChanges();
            return true;
        }

        public List<Material> getAllMaterials(int courseId)
        {

            return _db.Materials.Where(s => s.CourseId == courseId).ToList();

        }
        //updatedate here
        public Material getMaterialById(int id)
        {

            var material = _db.Materials.FirstOrDefault(p => p.Id == id);
            return material;
        }
        // updatedate here
        public bool DeleteMaterialById(int materialId)
        {
            var material = _db.Materials.FirstOrDefault(n => n.Id == materialId);
            if (material != null)
            {
                _db.Materials.Remove(material);
                _db.SaveChanges();
                return true;
            }
            return false;
        }
        //updatedate here
        public bool UpdateMaterial([FromForm] DtoMaterials dtoMaterials)
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

    }

}











