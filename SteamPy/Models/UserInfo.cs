using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace steamPy.Models
{
    public class UserInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [DisplayName("用户名")]
        public string? UserName { get; set; }

        [DisplayName("密码")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DisplayName("邮箱")]
        [EmailAddress]
        public string? Email { get; set; }

        [DisplayName("盐")]
        public string? Salt { get; set; }
    }
}
