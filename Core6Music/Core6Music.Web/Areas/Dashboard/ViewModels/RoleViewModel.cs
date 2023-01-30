using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Core6Music.Web.Areas.Dashboard.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        [DisplayName("名字")]
        public string RoleName { get; set; }

    }
}
