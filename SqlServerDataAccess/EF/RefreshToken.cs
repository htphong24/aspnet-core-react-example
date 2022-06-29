using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace SqlServerDataAccess.EF
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public string Token { get; set; }
        
        public DateTime ExpiresTime { get; set; }
        
        public bool IsExpired => DateTime.UtcNow >= ExpiresTime;
        
        public DateTime CreatedTime { get; set; }
        
        public string CreatedByIp { get; set; }
        
        public DateTime? RevokedTime { get; set; }
        
        public string RevokedByIp { get; set; }
        
        public string ReplacedByToken { get; set; }
        
        public bool IsActive => RevokedTime is null && !IsExpired;

        public string UserId { get; set; }

        //[ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    
    }
}
