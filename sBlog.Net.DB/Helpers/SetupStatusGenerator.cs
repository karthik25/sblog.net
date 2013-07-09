using System;
using System.Linq;
using sBlog.Net.DB.Services;
using sBlog.Net.DB.Comparers;
using sBlog.Net.DB.Enumerations;
using System.Collections.Generic;
using sBlog.Net.Domain.Interfaces;

namespace sBlog.Net.DB.Helpers
{
    public class SetupStatusGenerator
    {
        private readonly ISchema _schemaRepository;
        private readonly IPathMapper _pathMapper;

        public SetupStatusGenerator(ISchema schemaRepository, IPathMapper pathMapper)
        {
            _schemaRepository = schemaRepository;
            _pathMapper = pathMapper;
        }

        public SetupStatus GetSetupStatus()
        {
            var setupStatus = new SetupStatus();

            try
            {
                var allEntries = GetSchemaVersions();
                var sortedList = _pathMapper.GetAvailableScripts();

                var schemaInstance = allEntries.LastOrDefault();
                var lastInstance = sortedList.LastOrDefault();

                if (lastInstance != null && lastInstance.Equals(schemaInstance))
                {
                    setupStatus.StatusCode = SetupStatusCode.NoUpdates;
                    setupStatus.Message = "Your instance is up to date!";
                }
                else
                {
                    setupStatus.StatusCode = SetupStatusCode.HasUpdates;
                    setupStatus.Message = "Your instance has some updates";
                }
            }
            catch (Exception exception)
            {
                if (exception.Message == "Invalid object name 'Schema'.")
                {
                    setupStatus.StatusCode = SetupStatusCode.DatabaseNotSetup;
                    setupStatus.Message = "Database has not been setup";
                }
                else
                {
                    setupStatus.StatusCode = SetupStatusCode.DatabaseError;
                    setupStatus.Message = exception.Message;
                }
            }

            return setupStatus;
        }

        private IEnumerable<SchemaVersion> GetSchemaVersions()
        {
            var allEntries = _schemaRepository.GetSchemaEntries()
                                              .Select(
                                                  e =>
                                                  new SchemaVersion
                                                      {
                                                          MajorVersion = e.MajorVersion,
                                                          MinorVersion = e.MinorVersion,
                                                          ScriptVersion = e.ScriptVersion
                                                      })
                                              .ToList();
            allEntries.Sort(new SchemaVersionComparer());
            return allEntries;
        }
    }
}
