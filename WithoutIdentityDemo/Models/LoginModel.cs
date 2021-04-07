using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WithoutIdentityDemo.Models
{
    public class LoginModel
    {
        /// <summary>
        /// 識別ID(不使用User、Name、ID等，減少被弱掃或攻擊到的機會)
        /// </summary>
        [Required]
        [StringLength(30, ErrorMessage = "長度不符合規定", MinimumLength = 3)]
        [Display(Name = "帳號")]
        public string Badge { get; set; }
        /// <summary>
        /// 密碼(不使用Password，減少被弱掃或攻擊到的機會)
        /// </summary>
        [Required]
        [StringLength(30, ErrorMessage = "長度不符合規定", MinimumLength = 10)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string CipherCode { get; set; }
    }
}
