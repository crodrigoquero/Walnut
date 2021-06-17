using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Walnut.Entities;

namespace Walnut.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //Entities names are in SINGULAR, cos they represent individual elements
        //But set the property names in PLURAL!!! cos these props represent collections
        public DbSet<Gender> Genders { get; set; }
        public DbSet<WorkType> WorkTypes { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateFile> CandidateFiles { get; set; }
        public DbSet<CandidateFileType> CandidateFileTypes { get; set; }
        public DbSet<EntityFlag> EntityFlags { get; set; }
        public DbSet<EntityFlagEntity> EntityFlagEntities { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleType> ArticleTypes { get; set; }
        public DbSet<CompanyContact> CompanyContacts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ControlPanel> ControlPanels { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<DashboardControlPanel> DashboardControlPanels { get; set; }
        public DbSet<WorkFlow> WorkFlows { get; set; }
        public DbSet<ProcessTemplate> ProcessTemplates { get; set; }
        public DbSet<ProcessType> ProcessTypes { get; set; }
        public DbSet<ProcessStageTemplate> ProcessStageTemplates { get; set; }
        public DbSet<ProcessTaskTemplate> ProcessTasksTemplates { get; set; }
        public DbSet<WorkFlowSubscriptor> WorkFlowSubscriptors { get; set; }
        public DbSet<ControlPanelItem> ControlPanelItems { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }
        public DbSet<Periodicity> Periodicities { get; set; }
        public DbSet<CompanyContactRole> CompanyContactRoles { get; set; }
        public DbSet<CompanyDepartment> CompanyDepartments { get; set; }
        public DbSet<RelationshipType> RelationshipTypes { get; set; }
        public DbSet<SkillSet> SkillSets { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SkillSetDomain> SkillSetDomains { get; set; }
        public DbSet<JobVacancy> JobVacancies { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<JobVacancySkillSet> JobVacancySkillSets { get; set; }
        public DbSet<SkillType> SkillTypes { get; set; }
        public DbSet<CompanyAPI> CompanyAPIs { get; set; }
        public DbSet<CompanyAPICommand> CompanyAPICommands { get; set; }
        public DbSet<CompanyAPIcommandMAP> CompanyAPIcommandMAPs { get; set; }

        public System.Data.Entity.DbSet<Walnut.Entities.Process> Processes { get; set; }

        public System.Data.Entity.DbSet<Walnut.Entities.ProcessTask> ProcessTasks { get; set; }

        public System.Data.Entity.DbSet<Walnut.Entities.ProcessStage> ProcessStages { get; set; }
    }
}