namespace HealthyRecipes.Web.Areas.Administration.Controllers
{
    using HealthyRecipes.Common;
    using HealthyRecipes.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
