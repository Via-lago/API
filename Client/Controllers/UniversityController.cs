﻿using Client.Models;
using Client.Repositories.Data;
using Client.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    //[Authorize]
    public class UniversityController : Controller
    {
        private readonly IUniversityRepository repository;

        public UniversityController(IUniversityRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var result = await repository.Get();
            var universities = new List<University>();

            if (result.Data != null) 
            {
                universities = result.Data?.Select(e => new University
                {
                    Guid = e.Guid,
                    Code = e.Code,
                    Name = e.Name
                }).ToList();
            }

            return View(universities);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        public async Task<IActionResult> Create(University university)
        {
           /* if (ModelState.IsValid)
            {*/
                var result = await repository.Post(university);
                if (result.StatusCode == "200")
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (result.StatusCode == "409")
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
            /*}*/
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid Guid)
        {
            var result = await repository.Get(Guid);
            var university = new University();
            if (result.Data?.Guid is null)
            {
                return View(university);
            }
            else
            {
                university.Guid = result.Data.Guid;
                university.Code = result.Data.Code;
                university.Name = result.Data.Name;
            }

            return View(university);
        }

        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        public async Task<IActionResult> Edit(University university)
        {
          /*  if (ModelState.IsValid)
            {*/
                var result = await repository.Put(university);
                if (result.StatusCode == "200")
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (result.StatusCode == "409")
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
            /*   }*/
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete1(Guid Guid)
        {
            var result = await repository.Get(Guid);
            var university = new University();
            if (result.Data?.Guid is null)
            {
                return View(university);
            }
            else
            {
                university.Guid = result.Data.Guid;
                university.Code = result.Data.Code;
                university.Name = result.Data.Name;
                university.CreatedDate = result.Data.CreatedDate;
            }
            return View(university);
        }

        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        public async Task<IActionResult> Remove(Guid Guid)
        {
            var result = await repository.Delete1(Guid);
            if (result.StatusCode == "200")
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}