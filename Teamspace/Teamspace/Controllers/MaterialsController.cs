﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using System.Collections.Specialized;
using System.Security.Claims;
using System.Security.Cryptography;
using Teamspace.Configurations;
using Teamspace.DTO;
using Teamspace.Models;
using Teamspace.Repositories;

namespace Teamspace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        public MaterialsRepo _materialsRepo;

        public MaterialsController(MaterialsRepo materialsRepo)
        {
            _materialsRepo = materialsRepo;
        }

        [HttpPost]
        public async Task<IActionResult> addMaterials([FromForm] DtoMaterials dtoMaterials)
        {
            bool ok = await _materialsRepo.addMaterials(dtoMaterials);
            //Console.WriteLine(ok + "*************\n");
            if (ok == false) return BadRequest("No files provided");
            return Ok();
        }
        [HttpGet("getAllMaterials/{courseId:int}")]

        public async Task<IActionResult> getAllMaterials(int courseId)
        {
            var materials = await _materialsRepo.getAllMaterials(courseId);
            return Ok(materials);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> getMaterialById(int id)
        {
            //var temp = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            //Console.WriteLine(temp + "********\n");
            var material = await _materialsRepo.getMaterialById(id);
            if (material == null) return NotFound("This material does not exist.");
            return Ok(material);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteMaterialById(int id)
        {
            bool ok = await _materialsRepo.DeleteMaterialById(id);
            if (ok == true) return Ok();
            return NotFound("This material does not exist to delete");
        }

        // هل محتاجينها؟
        /*
        [HttpPut]

        public async Task<IActionResult> UpdateMaterial([FromForm] DtoMaterials dtoMaterials)
        {
            bool ok = _materialsRepo.UpdateMaterial(dtoMaterials);
            if (ok == true) return Ok();
            return NotFound();
        }
        */
    }
}






