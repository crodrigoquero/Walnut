using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Walnut.Entities;
using Walnut.Models;

namespace Walnut.Extensions
{
    public static class EntitiesExtensions
    {
        public static string GeEntitytTypeDescription(this Article article)
        {
            var db = ApplicationDbContext.Create();

            ArticleType _articleType =  db.ArticleTypes.Find(article.ArticleTypeId);
            if (_articleType != null)
            {
                return _articleType.Description;

            } else
            {
                return "N/A";
            }


        }

        public static string GetFlagDescription(this EntityFlagEntity EntityFlagEntity)
        {
            var db = ApplicationDbContext.Create();

            EntityFlag _entityFlag = db.EntityFlags.Find(EntityFlagEntity.EntityFlagId);
            if (_entityFlag != null)
            {
                return _entityFlag.Description;

            }
            else
            {
                return "N/A";
            }


        }

        public static string GeEntitytTypeDescription(this Company company)
        {
            var db = ApplicationDbContext.Create();

            CompanyType _comanyType = db.CompanyTypes.Find(company.CompanyTypeId);
            if (_comanyType != null)
            {
                return _comanyType.Description;

            }
            else
            {
                return "N/A";
            }


        }

        public static string GetCompanyDescription(this CompanyContact companyContact)
        {
            var db = ApplicationDbContext.Create();

            Company _company = db.Companies.Find(companyContact.CompanyId);
            if (_company != null)
            {
                return _company.Description;

            }
            else
            {
                return "N/A";
            }


        }

        public static string GetControlPanelDescription(this ControlPanelItem controlPanelItem)
        {
            var db = ApplicationDbContext.Create();

            ControlPanel _controlPanel = db.ControlPanels.Find(controlPanelItem.ControlPanelId);
            if (_controlPanel != null)
            {
                return _controlPanel.Description;

            }
            else
            {
                return "N/A";
            }


        }

        public static string GetProcessTypeDescription(this ProcessTemplate process)
        {
            var db = ApplicationDbContext.Create();

            ProcessType _processType = db.ProcessTypes.Find(process.ProcessTypeId);
            if (_processType != null)
            {
                return _processType.Description;

            }
            else
            {
                return "N/A";
            }


        }

        public static string GetProcessStageTemplateDescription(this ProcessTaskTemplate processTask)
        {
            var db = ApplicationDbContext.Create();

            ProcessStageTemplate _ProcessStageTemplate = db.ProcessStageTemplates.Find(processTask.ProcessStageId);
            if (_ProcessStageTemplate != null)
            {
                return _ProcessStageTemplate.Description;

            }
            else
            {
                return "N/A";
            }


        }

        public static string GetProcessDescription(this ProcessTaskTemplate processTask)
        {
            var db = ApplicationDbContext.Create();

            ProcessTemplate _Process = db.ProcessTemplates.Find(processTask.ProcessId);
            if (_Process != null)
            {
                return _Process.Description;

            }
            else
            {
                return "N/A";
            }


        }

        public static string GetCandidateDescription(this Interview interview)
        {
            var db = ApplicationDbContext.Create();

            Candidate _candidate = db.Candidates.Find(interview.CandidateId);
            if (_candidate != null)
            {
                return _candidate.Description;

            }
            else
            {
                return "N/A";
            }


        }

        public static string GetCompanyTypeDescription(this SkillSetDomain skillDomain)
        {
            var db = ApplicationDbContext.Create();

            CompanyType _companyType = db.CompanyTypes.Find(skillDomain.CompanyTypeId);
            if (_companyType != null)
            {
                return _companyType.Description;

            }
            else
            {
                return "N/A";
            }


        }

        public static string GetCompanyDepartmentDescription(this SkillSetDomain skillDomain)
        {
            var db = ApplicationDbContext.Create();

            CompanyDepartment _companyDepartment = db.CompanyDepartments.Find(skillDomain.CompanyDepartmentId);
            if (_companyDepartment != null)
            {
                return _companyDepartment.Description;

            }
            else
            {
                return "N/A";
            }


        }

        public static string GetSkillSetDescription(this Skill skill)
        {
            var db = ApplicationDbContext.Create();

            SkillSet _skillSet = db.SkillSets.Find(skill.SkillSetId);
            if (_skillSet != null)
            {
                return _skillSet.Description;

            }
            else
            {
                return "N/A";
            }


        }

        public static string GetSkillSetDomainDescription(this Skill skill)
        {
            var db = ApplicationDbContext.Create();

            SkillSetDomain _skillSetDomain = db.SkillSetDomains.Find(skill.SkillDomainId);
            if (_skillSetDomain != null)
            {
                return _skillSetDomain.Description;

            }
            else
            {
                return "N/A";
            }


        }

        public static string GetCandidateDescription(this JobApplication jobApplication)
        {
            var db = ApplicationDbContext.Create();

            Candidate _candidate = db.Candidates.Find(jobApplication.CandidateId);
            if (_candidate != null)
            {
                return _candidate.Description;

            }
            else
            {
                return "N/A";
            }


        }

        public static string GetCompanyDepartmentDescription(this JobVacancy jobVacancy)
        {
            var db = ApplicationDbContext.Create();

            CompanyDepartment _companyDepartment = db.CompanyDepartments.Find(jobVacancy.CompanyDepartmentId);
            if (_companyDepartment != null)
            {
                return _companyDepartment.Description;

            }
            else
            {
                return "N/A";
            }


        }

        public static string GetCompanyContactRoleDescription(this JobVacancy jobVacancy)
        {
            var db = ApplicationDbContext.Create();

            CompanyContactRole _CompanyContactRole = db.CompanyContactRoles.Find(jobVacancy.CompanyContactRoleId);
            if (_CompanyContactRole != null)
            {
                return _CompanyContactRole.Description;

            }
            else
            {
                return "N/A";
            }


        }

        public static string GetSkillSetDescription(this JobVacancySkillSet jobVacancySkillSet)
        {
            var db = ApplicationDbContext.Create();

            SkillSet _skillSet = db.SkillSets.Find(jobVacancySkillSet.SkillSetId);
            if (_skillSet != null)
            {
                return _skillSet.Description;

            }
            else
            {
                return "N/A";
            }


        }

        public static string GetJobVacancyDescription(this JobVacancySkillSet jobVacancySkillSet)
        {
            var db = ApplicationDbContext.Create();

            JobVacancy _jobVacancy = db.JobVacancies.Find(jobVacancySkillSet.JobVacancyId);
            if (_jobVacancy != null)
            {
                return _jobVacancy.Description;

            }
            else
            {
                return "N/A";
            }


        }

        public static string GetJobVacancyDescription(this JobApplication jobApplication)
        {
            var db = ApplicationDbContext.Create();

            JobVacancy _jobVacancy = db.JobVacancies.Find(jobApplication.JobVacancyId);
            if (_jobVacancy != null)
            {
                return _jobVacancy.Description;

            }
            else
            {
                return "N/A";
            }


        }

        public static string GetSkillTypeDescription(this Skill skill)
        {
            var db = ApplicationDbContext.Create();

            SkillType _skillType = db.SkillTypes.Find(skill.SkillTypeId);
            if (_skillType != null)
            {
                return _skillType.Description;
            }
            else
            {
                return "N/A";
            }


        }

        public static string GetProcessDescription(this ProcessStageTemplate processStageTemplate)
        {
            var db = ApplicationDbContext.Create();

            ProcessTemplate _Process = db.ProcessTemplates.Find(processStageTemplate.ProcessId);
            if (_Process != null)
            {
                return _Process.Description;

            }
            else
            {
                return "N/A";
            }


        }

    }
}