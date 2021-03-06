﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Rauthor.Models
{
    [Table("competition")]
    public class Competition
    {
        [Key]
        [Column("GUID")]
        public Guid Guid { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("creator_user_guid")]
        public Guid CreatorUserGuid { get; set; }

        /// <summary>
        /// Дата окончания приёма заявок
        /// </summary>
        [Column("start_date")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Дата публикации результата
        /// </summary>
        [Column("end_date")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Описание соревнования
        /// </summary>
        [Column("description")]
        public string Description { get; set; }

        /// <summary>
        /// Короткое описание соревнования
        /// </summary>
        [Column("short_description")]
        public string ShortDescription { get; set; }

        /// <summary>
        /// Дополнительные данные о конкурсе (в базе данных хранится в виде JSON).
        /// </summary>
        [Column("extra")]
        public string Extra { get; set; }

        [ForeignKey("CompetitionGuid")]
        public List<Prize> Prizes { get; set; }

        [ForeignKey("CreatorUserGuid")]
        public virtual User Creator { get; set; }

        public virtual List<Participant> Participants { get; set; }

        public virtual List<CompetitionRelCategory> Categories { get; set; }

        public virtual List<CompetitionConstraint> Constraints { get; set; }

        public virtual List<CompetitionRelJury> Jury { get; set; }

        public Competition()
        {
            Extra = "{}";
            Participants = new List<Participant>();
            Categories = new List<CompetitionRelCategory>();
            Jury = new List<CompetitionRelJury>();
            Constraints = new List<CompetitionConstraint>();
            Prizes = new List<Prize>();
        }

        /// <summary>
        /// Возвращает экземпляр Competition с заполненными полями и навигационными свойствами, на основе
        /// модели представления.
        /// </summary>
        public static Competition FromApiViewModel(ViewModels.Api.Competition viewModel)
        {
            var competition = new Competition()
            {
                Guid = viewModel.Guid ?? default,
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate,
                Description = viewModel.Description,
                ShortDescription = viewModel.ShortDescription,
                Title = viewModel.Title,
            };
            var juryRefernces = viewModel.JuryGuids.Select(juryGuid => new CompetitionRelJury()
            {
                CompetitionGuid = competition.Guid,
                JuryUserGuid = juryGuid
            });
            var categoryReferences = viewModel.Categories.Select(categoryGuid => new CompetitionRelCategory()
            {
                CompetitionGuid = competition.Guid,
                CategoryGuid = categoryGuid
            });
            var constrains = viewModel.Constraints.Select(x =>
            {
                x.CompetitionGuid = competition.Guid;
                return x;
            });
            var prizes = viewModel.Prizes.Select(x =>
            {
                x.CompetitionGuid = competition.Guid;
                return x;
            });
            competition.Jury = juryRefernces.ToList();
            competition.Categories = categoryReferences.ToList();
            competition.Constraints = constrains.ToList();
            competition.Prizes = prizes.ToList();
            return competition;
        }
    }
}
