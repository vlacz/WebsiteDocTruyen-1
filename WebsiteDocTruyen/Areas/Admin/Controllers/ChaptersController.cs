﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteDocTruyen.Models;
using WebsiteDocTruyen.ViewModels;

namespace WebsiteDocTruyen.Areas.Admin.Controllers
{
    public class ChaptersController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public ChaptersController()
        {
            _dbContext = new ApplicationDbContext();
        }

        // GET: Admin/Chapters
        // Thêm chapter cho truyện
        public ActionResult Create(int id, ChapterViewModel model)
        {
            var story = _dbContext.Stories.Find(id);
            ViewBag.ChapterTitle = story.Title;

            if (ModelState.IsValid)
            {
                var chapter = new Chapter{
                   Title = model.Title,
                   Content = model.Content,
                   StoryID = story.StoryID,
                };
                _dbContext.Chapters.Add(chapter);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Stories");
            }

            model.Story = _dbContext.Stories.ToList();
            return View(model);
        }

        // Sửa lại chapter
        public ActionResult Edit(int id, ChapterViewModel model)
        {
            var story = _dbContext.Stories.Find(id);

            List<Chapter> Chapter = _dbContext.Chapters.Where(s => s.StoryID == id).ToList();
            // Tạo SelectList
            SelectList ListChapter = new SelectList(Chapter, "ChapterID", "Title");
            // Set vào ViewBag
            ViewBag.ChapterList = ListChapter;

            ViewBag.ChapterTittle = story.Title;
            if (ModelState.IsValid)
            {
                var chapter = new Chapter
                {
                    ChapterID = model.ChapterID,
                    Title = model.Title,
                    Content = model.Content,
                    StoryID = story.StoryID,
                };
                _dbContext.Chapters.AddOrUpdate(chapter);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Stories");
            }

            model.Story = _dbContext.Stories.ToList();
            return View(model);
        }
    }
}