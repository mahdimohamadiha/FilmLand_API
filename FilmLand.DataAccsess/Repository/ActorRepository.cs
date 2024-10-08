﻿using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Database;
using FilmLand.Logs;
using FilmLand.Models;
using FilmLand.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.DataAccsess.Repository
{
    public class ActorRepository : IActorRepository
    {
        private readonly ICustomLogger _customLogger;
        public ActorRepository(ICustomLogger customLogger)
        {
            _customLogger = customLogger;
        }
        public string AddActor(ActorAndUploadFileDTO actorAndUploadFileDTO)
        {
            Guid idUploadFile = Guid.NewGuid();
            string message = DapperEntities.ExecuteDatabase("INSERT INTO UploadFile (UploadFileId, UploadFileTitle, UploadFilePath, UploadFileCreateDate, UploadFileIsStatus, UploadFileIsDelete) VALUES (@UploadFileId, @UploadFileTitle, @UploadFilePath, GETDATE(), 1, 0);", Connection.FilmLand(), new { UploadFileId = idUploadFile, UploadFileTitle = "ActorPicture", UploadFilePath = actorAndUploadFileDTO.ActorPath });
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
                Guid idActor = Guid.NewGuid();
                string message2 = DapperEntities.ExecuteDatabase("INSERT INTO Actor (ActorId, ActorName, ActorBirthDay, ActorProfession, ActorBio, Actor_UploadFileRef, ActorCreateDate, ActorIsStatus, ActorIsDelete) VALUES (@ActorId, @ActorName, @ActorBirthDay, @ActorProfession, @ActorBio, @Actor_UploadFileRef, GETDATE(), 1, 0)", Connection.FilmLand(), new { ActorId = idActor, ActorName = actorAndUploadFileDTO.ActorName, ActorBirthDay = actorAndUploadFileDTO.ActorBirthDay, ActorProfession = actorAndUploadFileDTO.ActorProfession, ActorBio = actorAndUploadFileDTO.ActorBio, Actor_UploadFileRef = idUploadFile });
                if (message2 == "Success")
                {
                    _customLogger.SuccessDatabase(message2);
                }
                else
                {
                    _customLogger.ErrorDatabase(message2);
                }
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return message;
        }
        public IEnumerable<ActorSummary> GetAllActorSummary()
        {
            (IEnumerable<ActorSummary> allActorSummaryList, string message) = DapperEntities.QueryDatabase<ActorSummary>("SELECT [ActorId]\r\n      ,[ActorName]\r\nFROM [Actor] WHERE ActorIsDelete = 0", Connection.FilmLand());
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return allActorSummaryList;
        }

        public (Actor, string) GetActor(Guid actorId)
        {
            (IEnumerable<Actor> actor, string message) = DapperEntities.QueryDatabase<Actor>("SELECT ActorName, ActorBirthDay, ActorProfession, ActorBio, UploadFilePath FROM Actor JOIN UploadFile on Actor_UploadFileRef = UploadFileId WHERE ActorId = @ActorId", Connection.FilmLand(), new { ActorId = actorId });
            if (message == "Success")
            {
                if (actor.Count() == 0)
                {
                    _customLogger.CustomDatabaseError("Id was not found in the database");
                    return (null, "Not found");
                }
                else
                {
                    _customLogger.SuccessDatabase(message);
                    return (actor.FirstOrDefault(), "Success");
                }
            }
            else
            {
                _customLogger.ErrorDatabase(message);
                return (null, "Error");
            }
        }
        public string RemoveActor(Guid actorId)
        {
            string message = DapperEntities.ExecuteDatabase("UPDATE Actor SET ActorIsDelete = 1 WHERE ActorId = @ActorId", Connection.FilmLand(), new { ActorId = actorId });
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return message;
        }
    }
}
