using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using sBlog.Net.Areas.Prepare.Models;
using sBlog.Net.DB.Core;
using sBlog.Net.DB.Enumerations;
using sBlog.Net.DB.Helpers;
using sBlog.Net.DB.Services;
using sBlog.Net.Domain.Concrete;
using sBlog.Net.Domain.Entities;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Infrastructure;

namespace sBlog.Net.Areas.Prepare.Controllers
{
    public class DatabaseController : Controller
    {
        private readonly ISchema _schemaRepository;
        private readonly ISettings _settingsRepository;
        private readonly IPathMapper _pathMapper;
        private readonly DbContext _dbContext;

        public DatabaseController(ISchema schemaRepository, ISettings settingsRepository, IPathMapper pathMapper)
        {
            _schemaRepository = schemaRepository;
            _settingsRepository = settingsRepository;
            _pathMapper = pathMapper;
            _dbContext = new DbContext();
        }

        public ActionResult Index()
        {
            var databaseStatusGenerator = new SetupStatusGenerator(_schemaRepository, _pathMapper);
            var databaseStatus = databaseStatusGenerator.GetSetupStatus();

            if (databaseStatus.StatusCode == SetupStatusCode.DatabaseError)
            {
                return RedirectToRoute("SetupError");
            }

            if (databaseStatus.StatusCode == SetupStatusCode.HasUpdates)
            {
                return RedirectToRoute("UpdateDatabase");
            }

            if (databaseStatus.StatusCode == SetupStatusCode.NoUpdates && !_settingsRepository.InstallationComplete)
            {
                return RedirectToRoute("SetupIndex");
            }

            if (databaseStatus.StatusCode == SetupStatusCode.NoUpdates && _settingsRepository.InstallationComplete)
            {
                return RedirectToAction("Index", "Home", new {Area = ""});
            }

            var databaseSetupModel = new DatabaseSetupModel { Scripts = _pathMapper.GetAvailableScripts().ToList().Select(s => Path.GetFileName(s.ScriptPath)).ToList() };
            return View(databaseSetupModel);
        }

        [HttpPost]
        public ActionResult Index(DatabaseSetupModel databaseSetupModel)
        {
            var isCredentialsValid = _dbContext.IsCredentialsValid(databaseSetupModel.ConnectionString);
            if (ModelState.IsValid && isCredentialsValid)
            {
                var files = _pathMapper.GetAvailableScripts().ToList();
                try
                {
                    var runner = new SqlRunner(ApplicationConfiguration.ConnectionString);
                    var results = runner.RunScripts(files);
                    UpdateScriptsRan(results);

                    return RedirectToRoute("SetupIndex");
                }
                catch (Exception exception)
                {
                    databaseSetupModel.Message = "Unable to complete the installation, scroll below...";
                    databaseSetupModel.MessageCss = "error";

                    databaseSetupModel.CompleteException = exception.ToString();
                }
            }

            if (!string.IsNullOrEmpty(databaseSetupModel.ConnectionString) && !isCredentialsValid)
            {
                databaseSetupModel.Message = "An invalid connection string was entered";
                databaseSetupModel.MessageCss = "error";
            }
            databaseSetupModel.Scripts = _pathMapper.GetAvailableScripts().ToList().Select(s => Path.GetFileName(s.ScriptPath)).ToList();
            return View(databaseSetupModel);
        }

        public ActionResult Update()
        {
            var databaseStatusGenerator = new SetupStatusGenerator(_schemaRepository, _pathMapper);
            var databaseStatus = databaseStatusGenerator.GetSetupStatus();

            if (databaseStatus.StatusCode == SetupStatusCode.DatabaseError)
            {
                return RedirectToRoute("SetupError");
            }

            if (databaseStatus.StatusCode == SetupStatusCode.DatabaseNotSetup)
            {
                return RedirectToRoute("InitializeDatabase");
            }

            if (databaseStatus.StatusCode == SetupStatusCode.NoUpdates && !_settingsRepository.InstallationComplete)
            {
                return RedirectToRoute("SetupIndex");
            }

            if (databaseStatus.StatusCode == SetupStatusCode.NoUpdates && _settingsRepository.InstallationComplete)
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            var scripts = GetRunnableScripts().ToList().Select(r => Path.GetFileName(r.ScriptPath)).ToList();
            var databaseSetupModel = new DatabaseSetupModel { Scripts = scripts };
            return View(databaseSetupModel);
        }

        [HttpPost]
        public ActionResult Update(DatabaseSetupModel databaseSetupModel)
        {
            var isCredentialsValid = _dbContext.IsCredentialsValid(databaseSetupModel.ConnectionString);
            var runnableScripts = GetRunnableScripts();

            if (ModelState.IsValid && isCredentialsValid)
            {
                try
                {
                    var runner = new SqlRunner(ApplicationConfiguration.ConnectionString);
                    var results = runner.RunScripts(runnableScripts);
                    UpdateScriptsRan(results);

                    return RedirectToRoute("SetupIndex");
                }
                catch (Exception exception)
                {
                    databaseSetupModel.Message = "Unable to complete the update, scroll below...";
                    databaseSetupModel.MessageCss = "error";

                    databaseSetupModel.CompleteException = exception.ToString();
                }
            }

            if (!string.IsNullOrEmpty(databaseSetupModel.ConnectionString) && !isCredentialsValid)
            {
                databaseSetupModel.Message = "An invalid connection string was entered";
                databaseSetupModel.MessageCss = "error";
            }

            var scripts = runnableScripts.Select(r => Path.GetFileName(r.ScriptPath)).ToList();
            databaseSetupModel.Scripts = scripts;
            return View(databaseSetupModel);
        }

        private List<SchemaVersion> GetRunnableScripts()
        {
            var availableScripts = _pathMapper.GetAvailableScripts().ToList();
            var scriptsToBeRan = _schemaRepository.GetScriptsToExecute(availableScripts);
            return scriptsToBeRan;
        }

        private void UpdateScriptsRan(List<SchemaVersion> results)
        {
            results.ForEach(result =>
                {
                    var schemaEntity = new SchemaEntity { ScriptName = Path.GetFileName(result.ScriptPath), MajorVersion = result.MajorVersion, MinorVersion = result.MinorVersion, ScriptVersion = result.ScriptVersion, ScriptRunDateTime = DateTime.Now };
                    _schemaRepository.AddSchemaEntity(schemaEntity);
                });
        }
    }
}
